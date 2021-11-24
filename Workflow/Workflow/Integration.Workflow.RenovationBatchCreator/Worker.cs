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

namespace Infrastructure.WindowsService.RenovationBatchCreator {
    public class Worker : BackgroundService {
        private const string SERVICE_NAME = "RenovationBatchCreator";
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

                    // Busca os dados b�sicos de processamento do servi�o
                    service = await appServiceService.GetAsync(SERVICE_NAME);
                    if (service == null) {
                        throw new Exception("N�o foi poss�vel obter os par�metros para utiliza��o do servi�o");
                    }

                    // Envia sinal de keep alive para que seja poss�vel monitorar o funcionamento do servi�o
                    logger.LogInformation($"{SERVICE_NAME} sending keep alive at: {DateTimeOffset.Now}");
                    await appServiceService.SendKeepAliveAsync(service.AppServiceId.Value);

                    // Verifica se j� deu o prazo de processamento (em segundos)
                    var difference = (DateTime.Now - LastExecution).TotalMilliseconds / 1000;
                    if (difference >= (service.Timeout)) {

                        var batchSuccess = new List<Domain.Payload.PolicyBatch>();
                        var batchError = new List<Domain.Payload.PolicyBatch>();

                        // Realiza a gera��o dos lotes
                        logger.LogInformation($"{SERVICE_NAME} creating renovation batchs: {DateTimeOffset.Now}");
                        var configs = await this.policyBatchConfigurationService.ListAsync(new Domain.Payload.PolicyBatchConfigurationFilters() { Status = Domain.Enumerators.RecordStatusEnum.Active });
                        foreach (var config in configs) {
                            var batches = await this.policyBatchService.ListNewAsync(config);
                            foreach (var batch in batches) {
                                try {

                                    batch.BatchType = config.BatchType;
                                    batch.LoggedUserId = 999;
                                    await this.policyBatchService.SaveAsync(batch, config.ProcessDays.Value);

                                    batchSuccess.Add(batch);

                                } catch (Exception b) {
                                    batch.Exception = b;
                                    batchError.Add(batch);
                                }
                            }
                        }

                        // Envia e-mail de sucesso
                        if (batchSuccess.Count > 0) {
                            logger.LogInformation($"{SERVICE_NAME} calling SendBatchSuccessMailAsync: {DateTimeOffset.Now}");
                            await this.policyBatchService.SendBatchSuccessMailAsync(batchSuccess);
                        }

                        // Envia e-mail de falhas
                        if (batchError.Count > 0) {
                            logger.LogInformation($"{SERVICE_NAME} calling SendBatchErrorMailAsync: {DateTimeOffset.Now}");
                            await this.policyBatchService.SendBatchErrorMailAsync(batchError);
                        }

                        // Busca novas ap�lices que possuam indice de atualiza��o para que sejam criadas as propostas na i4pro
                        logger.LogInformation($"{SERVICE_NAME} creating pending proposals: {DateTimeOffset.Now}");
                        var pendings = await this.policyBatchService.ListProposalCreationPendingAsync();
                        var proposalSuccess = new List<Domain.Payload.PolicyRenovation>();
                        var proposalError = new List<Domain.Payload.PolicyRenovation>();
                        foreach (var pending in pendings) {
                            try {

                                await this.policyBatchService.SavePolicyRenovationAsync(pending);
                                proposalSuccess.Add(pending);

                            } catch (Exception b) {
                                pending.Exception = b;
                                proposalError.Add(pending);
                            }
                        }

                        // Sinaliza o final da execu��o
                        await appServiceService.UpdateExecutionAsync(service.AppServiceId.Value, ExecutionStatusEnum.FinishedSuccess,
                            string.Format("Processamento finalizado. Lote criados com sucesso: {0}, erros na cria��o de lotes: {1}, propostas criadas com sucesso: {2}, erros na cria��o de propostas: {3}", 
                                batchSuccess.Count, batchError.Count, proposalSuccess.Count, proposalError.Count), null);

                        LastExecution = DateTime.Now;
                        logger.LogInformation($"{SERVICE_NAME} running finished at: {DateTimeOffset.Now}");
                    }

                } catch (Exception e) {
                    logger.LogError(e, $"{SERVICE_NAME} failed at: {DateTimeOffset.Now}");
                    if (appServiceService != null) {
                        await appServiceService.UpdateExecutionAsync(service.AppServiceId.Value, ExecutionStatusEnum.FinishedError,
                            $"Erro no processo de sincroniza��o: {e.Message}", service.ExecutionData);
                    }
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
