using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IBrokerDao {
        Task<BrokerEntity> GetAsync(int brokerExternalId);
    }
}
