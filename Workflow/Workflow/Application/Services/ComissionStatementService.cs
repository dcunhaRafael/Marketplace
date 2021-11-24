using Application.Interfaces.Services;
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
                var payloads = from a in items select new ComissionStatementStatus() { Id = a.Id, Name = a.Name, LegacyCode = a.LegacyCode };
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

        public IList<ComissionStatementDetail> ListComissionStatementDetais(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId) {
            var methodParameters = new { statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = legacyBrokerService.ListComissionStatementDetais(statementNumber, competency, brokerLegacyCode, brokerSusepCode, brokerUserId, new LoggerComplement() { UserId = loggedUserId });
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos detalhes do extrato de comissão: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
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
