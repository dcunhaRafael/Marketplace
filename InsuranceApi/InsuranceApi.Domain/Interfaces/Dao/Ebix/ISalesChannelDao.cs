using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ISalesChannelDao {
        Task<SalesChannelEntity> GetAsync(int productId, long personId);
    }
}
