using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class CoverageApplication : ICoverageApplication {
        private readonly ICoverageDao coverageDao;

        public CoverageApplication(ICoverageDao coverageDao) {
            this.coverageDao = coverageDao;
        }

        public async Task<IList<CoverageEntity>> ListAsync(int? productId = null) {
            var list = await this.coverageDao.ListAsync(productId, RecordStatusEnum.Ativo);
            return list;
        }

        public async Task<CoverageEntity> GetAsync(string productExternalCode, string coverageExternalCode) {
            var item = await this.coverageDao.GetAsync(productExternalCode, coverageExternalCode);
            return item;
        }

        public async Task<CoberturaAgravoEntity> GetParametersAsync(int coverageExternalCode) {
            var item = await this.coverageDao.GetGrievanceAsync(coverageExternalCode);
            return item;
        }
    }
}
