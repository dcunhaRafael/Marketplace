using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ITakerDao {
        Task<long> AddAsync(TakerModel item);
        Task UpdateAsync(int takerExternalCode, long personId);
    }
}
