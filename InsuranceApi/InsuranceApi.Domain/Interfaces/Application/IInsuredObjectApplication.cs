using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IInsuredObjectApplication {
        Task<string> GetTextAsync(ProposalEntity proposal);
    }
}
