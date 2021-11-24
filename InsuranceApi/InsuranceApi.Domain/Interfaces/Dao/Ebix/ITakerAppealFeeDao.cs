using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ITakerAppealFeeDao {
        Task<TakerAppealFeeEntity> GetAsync(int takerExternalCode, int productId, int coverageId, decimal insuredAmountValue, int termTypeId);
    }
}
