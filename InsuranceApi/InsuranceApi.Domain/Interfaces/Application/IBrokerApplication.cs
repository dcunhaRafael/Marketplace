using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
   public interface IBrokerApplication {
        Task<BrokerEntity> GetAsync(int externalCode);
        Task<CorretorConsultaEntity> GetAsync(CorretorConsultaEntity loggedBroker, int? proposalUserId);
        Task<CorretorConsultaEntity> GetAsync(long documentNumber);
    }
}
