using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IInsuredApplication {
        Task<InsuredEntity> GetAsync(string cpfCnpj);

        Task<InsuredEntity> IsRegisteredAsync(long cpfCnpj);
    }
}
