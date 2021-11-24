using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IOccurrenceValidationRuleService {
        Task<bool> CheckAlwaysGenerate(Proposal proposal);
        Task<bool> CheckIsInsuredBlocked(Proposal proposal);
        Task<bool> CheckHasCoverageCreditSubLimit(Proposal proposal, Domain.Payload.TakerCreditLimit takerCreditLimit);
        Task<bool> CheckHasFinancialPending(Proposal proposal);
        Task<bool> CheckHasCreditLimit(Proposal proposal, Domain.Payload.TakerCreditLimit takerCreditLimit);
        Task<bool> CheckIsCCGSigned(Domain.Payload.TakerData takerData);
    }
}
