using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public  interface ILaborCourtApplication {
        Task<IList<LaborCourtEntity>> ListAsync(string name);
        Task<LaborCourtEntity> GetAsync(int laborCourtId);
        Task UpdateAsync(LaborCourtEntity item);
    }
}
