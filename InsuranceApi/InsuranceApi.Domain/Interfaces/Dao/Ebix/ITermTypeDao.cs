using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ITermTypeDao {
        Task<TermTypeEntity> GetAsync(int termTypeId, int coverageExternalCode);
        Task<IList<TermTypeEntity>> ListAsync(int coverageExternalCode);
    }
}
