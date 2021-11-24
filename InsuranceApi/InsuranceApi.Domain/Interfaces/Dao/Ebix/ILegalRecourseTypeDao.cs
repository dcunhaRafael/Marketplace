using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ILegalRecourseTypeDao {
        Task<IList<LegalRecourseTypeEntity>> ListAsync();
        Task<LegalRecourseTypeEntity> GetAsync(int legalRecourseTypeId);
        Task<RecursoParametroEntity> GetParameterAsync(int legalRecourseTypeId);
    }
}

