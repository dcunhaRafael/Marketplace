using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ICrivoApplication {
        Task<CreditPolicyEntity> GetCreditPolicyAsync(string cnpj);
    }
}
