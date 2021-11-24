using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IRateDao {
        Task<RateEntity> GetAsync(decimal insuredAmountValue, int termTypeId);
    }
}
