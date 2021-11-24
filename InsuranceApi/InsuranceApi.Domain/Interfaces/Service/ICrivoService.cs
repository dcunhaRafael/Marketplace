using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface ICrivoService {
        Task<CreditPolicyEntity> GetAsync(GetCrivoEntity filtros);
    }
}