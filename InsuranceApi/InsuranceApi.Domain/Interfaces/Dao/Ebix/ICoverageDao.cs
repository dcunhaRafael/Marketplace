using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ICoverageDao {
        Task<CoverageEntity> GetAsync(string productExternalCode, string coverageExternalCode);
        Task<CoberturaAgravoEntity> GetGrievanceAsync(int externalCode);
        Task<IList<CoverageEntity>> ListAsync(int? productId, RecordStatusEnum status);
    }
}
