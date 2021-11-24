using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ICoverageApplication {
        Task<IList<CoverageEntity>> ListAsync(int? productId = null);
        Task<CoverageEntity> GetAsync(string productExternalCode, string coverageExternalCode);
        Task<CoberturaAgravoEntity> GetParametersAsync(int coverageExternalCode);
    }
}
