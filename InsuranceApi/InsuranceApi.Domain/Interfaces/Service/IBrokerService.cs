using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IBrokerService {
        Task<BrokerEntity> GetAsync(int proposalBrokerUserId);
        Task<IList<CorretorConsultaEntity>> ListAsync(CorretorConsultaEntity filters);
    }
}
