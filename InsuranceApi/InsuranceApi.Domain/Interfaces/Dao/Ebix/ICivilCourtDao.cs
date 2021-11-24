using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ICivilCourtDao {
        Task<IList<CivilCourtEntity>> ListAsync(int laborCourtId, string name, RecordStatusEnum status);
        Task<CivilCourtEntity> GetAsync(int civilCourtId, int laborCourtId);
        Task UpdateExternalCodeAsync(CivilCourtEntity item);
    }
}
