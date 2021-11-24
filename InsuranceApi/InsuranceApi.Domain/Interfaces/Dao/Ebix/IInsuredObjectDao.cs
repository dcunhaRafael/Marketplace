using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IInsuredObjectDao {
        Task<InsuredObjectEntity> GetAsync(int coverageId);
        Task<IList<InsuredObjectBlockEntity>> ListBlockAsync(int insuredObjectId, RecordStatusEnum? status);
    }
}
