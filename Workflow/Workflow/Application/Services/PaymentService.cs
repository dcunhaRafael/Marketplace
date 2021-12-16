using Application.Interfaces.Services;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Application.Services {
    public class PaymentService : BaseLogger, IPaymentService {
        private readonly ILatePaymentRepository latePaymentRepository;

        public PaymentService(ILogger<PaymentService> logger, ILatePaymentRepository latePaymentRepository) : base(logger) {
            this.latePaymentRepository = latePaymentRepository;
        }

        public IList<LatePaymentSlip> ListLatePaymentSlip(string brokerLegacyCode, string takerLegacyCode, string insuredLegacyCode, string productLegacyCode,
            long? policyNumber, int? endorsementNumber, int? installmentNumber, decimal? premiumValue, string ourNumber,
            DateTime? fromDate, DateTime? toDate) {
            var methodParameters = new {
                brokerLegacyCode,
                takerLegacyCode,
                insuredLegacyCode,
                productLegacyCode,
                policyNumber,
                endorsementNumber,
                installmentNumber,
                premiumValue,
                ourNumber,
                fromDate,
                toDate
            };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = latePaymentRepository.ListLatePaymentSlip(brokerLegacyCode, takerLegacyCode, insuredLegacyCode, productLegacyCode, policyNumber,
                                         endorsementNumber, installmentNumber, premiumValue, ourNumber, fromDate, toDate);
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos boletos de pagamento em atraso: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public LatePaymentSlip GetLatePaymentSlip(string ourNumber) {
            var methodParameters = new { ourNumber };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = latePaymentRepository.GetLatePaymentSlip(ourNumber);
                return item;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo boleto de pagamento em atraso: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
