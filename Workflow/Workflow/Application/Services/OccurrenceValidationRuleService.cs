using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Extensions;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class OccurrenceValidationRuleService : BaseLogger, IOccurrenceValidationRuleService {
        private readonly IPendingInstallmentRepository pendingInstallmentRepository;

        public OccurrenceValidationRuleService(ILogger<UserService> logger,
            IPendingInstallmentRepository pendingInstallmentRepository) : base(logger) {
            this.pendingInstallmentRepository = pendingInstallmentRepository;
        }

        public Task<bool> CheckAlwaysGenerate(Domain.Entities.Proposal proposal) {
            var methodParameters = new { proposal };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                return Task.FromResult(true);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na checagem da regra de validação ('{ValidationRuleEnum.AlwaysGenerate.GetDescription()}'): {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public Task<bool> CheckIsInsuredBlocked(Domain.Entities.Proposal proposal) {
            var methodParameters = new { proposal };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                return Task.FromResult(true);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na checagem da regra de validação ('{ValidationRuleEnum.IsInsuredBlocked.GetDescription()}'): {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public Task<bool> CheckHasCoverageCreditSubLimit(Domain.Entities.Proposal proposal, TakerCreditLimit takerCreditLimit) {
            var methodParameters = new { proposal };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                if (takerCreditLimit.Sublimits.Count == 0) {
                    return Task.FromResult(true);   // Tomador não tem sublimites parametrizados
                }

                var coverageSubLimit = takerCreditLimit.Sublimits.FirstOrDefault(x=> x.Coverages.FirstOrDefault(y=> y.Coverage.CoverageId == proposal.Coverage.CoverageId) != null);
                if (coverageSubLimit == null) {
                    return Task.FromResult(false);  // Não possui sublimite para cobertura
                }

                return Task.FromResult((coverageSubLimit.AvailableSubLimitValue ?? 0) >= (proposal.InsuredAmount ?? 0));

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na checagem da regra de validação ('{ValidationRuleEnum.HasCoverageCreditSubLimit.GetDescription()}'): {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<bool> CheckHasFinancialPending(Domain.Entities.Proposal proposal) {
            var methodParameters = new { proposal };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var pendingCount = await pendingInstallmentRepository.GetPendingCount(proposal.Taker.CpfCnpjNumber);
                return pendingCount > 0;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na checagem da regra de validação ('{ValidationRuleEnum.HasFinancialPending.GetDescription()}'): {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public Task<bool> CheckHasCreditLimit(Domain.Entities.Proposal proposal, TakerCreditLimit takerCreditLimit) {
            var methodParameters = new { proposal };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                return Task.FromResult((takerCreditLimit.AvailableCreditLimit ?? 0) >= (proposal.InsuredAmount ?? 0));

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na checagem da regra de validação ('{ValidationRuleEnum.HasCreditLimit.GetDescription()}'): {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public Task<bool> CheckIsCCGSigned(TakerData takerData) {
            var methodParameters = new { takerData };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                return Task.FromResult(takerData.IsCcgSigned);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na checagem da regra de validação ('{ValidationRuleEnum.IsCCGSigned.GetDescription()}'): {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }
    }
}
