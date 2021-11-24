using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface ILegalRecourseTypeParameterDao {
        Task<LegalRecourseTypeParameterEntity> GetAsync(int externalCode);
    }
}
