using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ITermTypeApplication {
        Task<IList<TermTypeEntity>> ListAsync(int coverageExternalCode);
        Task<TermTypeEntity> GetAsync(int termTypeId, int coverageExternalCode);
    }
}
