using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ILaborCourtDao {
        Task<LaborCourtEntity> GetAsync(int laborCourtId);
        Task<IList<LaborCourtEntity>> ListAsync(string name, RecordStatusEnum status);
        Task UpdateExternalCodeAsync(LaborCourtEntity item);
    }
}
