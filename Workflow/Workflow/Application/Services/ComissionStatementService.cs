using Application.Interfaces.Services;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Domain.Util.Extensions;
using Infrastructure.Interfaces.Repositories;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class ComissionStatementService : BaseLogger, IComissionStatementService {
        private readonly IComissionStatementStatusRepository comissionStatementStatusRepository;
        private readonly ILegacyBrokerService legacyBrokerService;

        public ComissionStatementService(ILogger<ComissionStatementService> logger,
            IComissionStatementStatusRepository comissionStatementStatusRepository,
            ILegacyBrokerService legacyBrokerService) : base(logger) {
            this.comissionStatementStatusRepository = comissionStatementStatusRepository;
            this.legacyBrokerService = legacyBrokerService;
        }

        public async Task<IList<ComissionStatementStatus>> ListAsync() {
            var methodParameters = new { };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await comissionStatementStatusRepository.ListAsync();
                var payloads = from a in items select new ComissionStatementStatus() { Id = a.Id, Name = a.Name, LegacyCode = a.LegacyCode, BackgroundColor = a.BackgroundColor, TextColor = a.TextColor };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos status do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public IList<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, fromDate, toDate, status, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatement(statementNumber, fromDate, toDate, status, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<ComissionStatementDetail> GetComissionStatementDetail(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatementDetails(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                var detail = items.FirstOrDefault(); ;

                var status = await comissionStatementStatusRepository.GetAsync(detail.StatusName);
                if (!string.IsNullOrWhiteSpace(status?.ImportantWarningText)) {
                    detail.ImportantWarningText = FillText(status.ImportantWarningText, detail);

                }
                return detail;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos detalhes do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        private string FillText(string bodyText, ComissionStatementDetail detail) {
            bodyText = bodyText.Replace("@{NUMERO_EXTRATO}", detail.StatementNumber.ToString());
            bodyText = bodyText.Replace("@{NOME_CORRETOR}", detail.Broker.Name);
            bodyText = bodyText.Replace("@{COMPETENCIA}", detail.Competency);
            bodyText = bodyText.Replace("@{NUMERO_RECIBO}", detail.ReceiptNumber.ToString());
            bodyText = bodyText.Replace("@{VALOR_PAGAMENTO}", detail.ComissionNetValue?.FormatCurrency());
            bodyText = bodyText.Replace("@{BANCO_PAGAMENTO}", detail.PaymentBank);
            bodyText = bodyText.Replace("@{AGENCIA_PAGAMENTO}", detail.PaymentBranch);
            bodyText = bodyText.Replace("@{CONTA_PAGAMENTO}", detail.PaymentAccount);
            return bodyText.ToString();
        }


        public IList<ComissionStatementType> ListComissionStatementTypes(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatementTypes(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem do extrato de comissão por tipo: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public IList<ComissionStatementBusiness> ListComissionStatementBusiness(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatementBusiness(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem do extrato de comissão por ramo: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public IList<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatementEntries(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos lançamentos do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
