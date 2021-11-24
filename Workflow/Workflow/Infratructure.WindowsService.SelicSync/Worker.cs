using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Payload;
using Domain.Util.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Infratructure.WindowsService.SelicSync {
    public class Worker : BackgroundService {
        private const string SERVICE_NAME = "SelicSync";
        private const int DEFAULT_DELAY = 60000;
        private DateTime LastExecution = DateTime.MinValue;
        private readonly ILogger<Worker> logger;
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ISelicService selicService;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) {
            this.logger = logger;
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            using var scope = serviceScopeFactory.CreateScope();
            this.selicService = scope.ServiceProvider.GetRequiredService<ISelicService>();
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
                        throw new Exception("Não foi possível obter os parâmetros para utilização do serviço.");
                    }

                    DateTime startDate = new DateTime(2000, 1, 1);
                    if (!string.IsNullOrWhiteSpace(service.ExecutionData)) {
                        startDate = DateTime.ParseExact(service.ExecutionData, "yyyy-MM-dd", new CultureInfo("en-US"));
                    }
                    DateTime finishDate = DateTime.Now;

                    // Envia sinal de keep alive para que seja possível monitorar o funcionamento do serviço
                    logger.LogInformation($"{SERVICE_NAME} sending keep alive at: {DateTimeOffset.Now}");
                    await appServiceService.SendKeepAliveAsync(service.AppServiceId.Value);

                    // Verifica se já deu o prazo de processamento (em segundos)
                    var difference = (DateTime.Now - LastExecution).TotalMilliseconds / 1000;
                    if (difference >= (service.Timeout)) {

                        logger.LogInformation($"{SERVICE_NAME} calling SyncDailyAsync: {DateTimeOffset.Now}");
                        var newFinishDateDaily = await selicService.SyncDailyAsync(startDate, finishDate);

                        logger.LogInformation($"{SERVICE_NAME} calling SyncMonthlyAsync: {DateTimeOffset.Now}");
                        await selicService.SyncMonthlyAsync(startDate.ToFirstDayOfMonth(), finishDate);

                        // Sinaliza o final da execução
                        await appServiceService.UpdateExecutionAsync(service.AppServiceId.Value, ExecutionStatusEnum.FinishedSuccess,
                            $"Processamento finalizado. Período de {startDate.FormatDate()} a {finishDate.FormatDate()}",
                            newFinishDateDaily.ToString("yyyy-MM-dd"));

                        LastExecution = DateTime.Now;
                        logger.LogInformation($"{SERVICE_NAME} running finished at: {DateTimeOffset.Now}");
                    }

                } catch (Exception e) {
                    logger.LogError(e, $"{SERVICE_NAME} failed at: {DateTimeOffset.Now}");
                    if (appServiceService != null) {
                        await appServiceService.UpdateExecutionAsync(service.AppServiceId.Value, ExecutionStatusEnum.FinishedError,
                            $"Erro no processo de sincronização: {e.Message}", service.ExecutionData);
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
