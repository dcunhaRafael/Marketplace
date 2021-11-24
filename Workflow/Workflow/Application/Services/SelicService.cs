using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class SelicService : BaseLogger, ISelicService {
        private readonly ISelicDailyRepository selicDailyRepository;
        private readonly ISelicMonthlyRepository selicMonthlyRepository;
        private readonly IBacenService bacenService;

        public SelicService(ILogger<ProfileService> logger,
            ISelicDailyRepository selicDailyRepository, ISelicMonthlyRepository selicMonthlyRepository, IBacenService bacenService) : base(logger) {
            this.selicDailyRepository = selicDailyRepository;
            this.selicMonthlyRepository = selicMonthlyRepository;
            this.bacenService = bacenService;
        }

        public async Task<DateTime> SyncDailyAsync(DateTime fromDate, DateTime toDate) {
            DateTime lastSync = fromDate;
            var taxes = bacenService.ListDaily(fromDate, toDate);
            foreach (var tax in taxes) {
                await selicDailyRepository.SaveAsync(new Domain.Entities.SelicDaily() {
                    Date = tax.Date,
                    Value = tax.Value
                });
                lastSync = tax.Date;
            }
            return lastSync;
        }

        public async Task<DateTime> SyncMonthlyAsync(DateTime fromDate, DateTime toDate) {
            DateTime lastSync = fromDate;
            var taxes = bacenService.ListMonthly(fromDate, toDate);
            foreach (var tax in taxes) {
                await selicMonthlyRepository.SaveAsync(new Domain.Entities.SelicMonthly() {
                    Date = tax.Date,
                    Value = tax.Value
                });
                lastSync = tax.Date;
            }
            return lastSync;
        }

        public async Task<decimal> ApplyCorrectionAsync(decimal initialValue, DateTime fromDate, DateTime toDate) {
            var methodParameters = new { initialValue, fromDate, toDate };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var newValue = initialValue;
                var corrections = await selicMonthlyRepository.ListCorrectionAsync(fromDate, toDate);
                foreach (var correction in corrections) {
                    newValue = newValue + (newValue * (correction.ValueCorrection / 100M));
                }
                return newValue;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na correção do valor: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
