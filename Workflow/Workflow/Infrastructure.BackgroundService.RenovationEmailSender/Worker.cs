using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Payload;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.WindowsService.RenovationEmailSender {
    public class Worker : BackgroundService {
        private const string SERVICE_NAME = "RenovationEmailSender";
        private const int DEFAULT_DELAY = 60000;
        private DateTime LastExecution = DateTime.MinValue;
        private readonly ILogger<Worker> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly IPolicyBatchConfigurationService policyBatchConfigurationService;
        private readonly IPolicyBatchService policyBatchService;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));

            using var scope = serviceScopeFactory.CreateScope();
            this.policyBatchConfigurationService = scope.ServiceProvider.GetRequiredService<IPolicyBatchConfigurationService>();
            this.policyBatchService = scope.ServiceProvider.GetRequiredService<IPolicyBatchService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            while (!stoppingToken.IsCancellationRequested) {

                logger.LogInformation($"{SERVICE_NAME} running started at: {DateTimeOffset.Now}");

                IAppServiceService appServiceService = null;
                AppService service = null;
                using var scope = serviceScopeFactory.CreateScope();

                try {

                    appServiceService = scope.ServiceProvider.GetRequiredService<IAppServiceService>();

                    // Busca os dados básicos de processamento do serviço
                    service = await appServiceService.GetAsync(SERVICE_NAME);
                    if (service == null) {
                        throw new Exception("Não foi possível obter os parâmetros para utilização do serviço");
                    }

                    // Envia sinal de keep alive para que seja possível monitorar o funcionamento do serviço
                    logger.LogInformation($"{SERVICE_NAME} sending keep alive at: {DateTimeOffset.Now}");
                    await appServiceService.SendKeepAliveAsync(service.AppServiceId.Value);

                    // Verifica se já deu o prazo de processamento (em segundos)
                    var difference = (DateTime.Now - LastExecution).TotalMilliseconds / 1000;
                    if (difference >= (service.Timeout)) {

                        var mailSuccess = new List<Domain.Payload.PolicyBatchItem>();
                        var mailError = new List<Domain.Payload.PolicyBatchItem>();

                        var configs = await this.policyBatchConfigurationService.ListAsync(new Domain.Payload.PolicyBatchConfigurationFilters() { Status = RecordStatusEnum.Active });
                        var batches = await this.policyBatchService.ListAsync(new Domain.Payload.PolicyBatch() { BatchStatus = PolicyBatchStatusEnum.Pending, Status = RecordStatusEnum.Active });
                        foreach (var config in configs) {
                            foreach (var mail in config.Mails) {
                                foreach (var batch in batches) {
                                    var items = await this.policyBatchService.ListItemsAsync(batch.PolicyBatchId.Value, mail.DaysBeforeExpiration);
                                    foreach (var item in items) {

                                        try {

                                            logger.LogInformation($"{SERVICE_NAME} calling SendAlertMailAsync: {DateTimeOffset.Now}");
                                            await this.policyBatchService.SendAlertMailAsync(mail, item);

                                            mailSuccess.Add(item);

                                        } catch (Exception b) {
                                            batch.Exception = b;
                                            mailError.Add(item);
                                        }

                                    }
                                }
                            }
                        }

                        // Envia e-mail de final de processamento
                        if (mailSuccess.Count > 0 || mailError.Count > 0) {
                            logger.LogInformation($"{SERVICE_NAME} sending finish process report at: {DateTimeOffset.Now}");
                            await this.policyBatchService.SendAlertMailFinishedAsync(mailSuccess.Count, mailError.Count);
                        }

                        // Sinaliza o final da execução
                        await appServiceService.UpdateExecutionAsync(service.AppServiceId.Value, ExecutionStatusEnum.FinishedSuccess,
                            $"Processamento finalizado. Enviados com sucesso: {mailSuccess.Count}, erros no envio: {batches.Count}", null);

                        LastExecution = DateTime.Now;
                        logger.LogInformation($"{SERVICE_NAME} running finished at: {DateTimeOffset.Now}");
                    }

                } catch (Exception e) {
                    logger.LogError(e, $"{SERVICE_NAME} failed at: {DateTimeOffset.Now}");
                    await appServiceService.UpdateExecutionAsync(service.AppServiceId.Value, ExecutionStatusEnum.FinishedError,
                        $"Erro no processo de envios de e-mail: {e.Message}", service.ExecutionData);
                }

                logger.LogInformation($"{SERVICE_NAME} sleeping at: {DateTimeOffset.Now}");
                await Task.Delay(DEFAULT_DELAY, stoppingToken);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken) {
            logger.LogInformation($"{SERVICE_NAME} started at: {DateTimeOffset.Now}");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken) {
            logger.LogInformation($"{SERVICE_NAME} stopped at: {DateTimeOffset.Now}");
            return base.StopAsync(cancellationToken);
        }
    }
}
