using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IProductDao {
        Task<ProductEntity> GetAsync(string externalCode);
    }
}
