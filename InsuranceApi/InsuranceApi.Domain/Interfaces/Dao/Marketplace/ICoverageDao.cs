using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface ICoverageDao {
        Task<CoverageEntity> GetAsync(int externalCode);
        //Task<CoberturaAgravoEntity> GetGrievanceAsync(int externalCode);
        //Task<IList<CoverageEntity>> ListAsync(int? productId, RecordStatusEnum status);
    }
}
