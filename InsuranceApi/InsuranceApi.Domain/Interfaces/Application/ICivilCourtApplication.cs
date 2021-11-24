using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ICivilCourtApplication {
        Task<IList<CivilCourtEntity>> ListAsync(int laborCourtId, string name);
        Task<CivilCourtEntity> GetAsync(int civilCourtId, int laborCourtId);
        Task UpdateAsync(CivilCourtEntity entity);
    }
}
