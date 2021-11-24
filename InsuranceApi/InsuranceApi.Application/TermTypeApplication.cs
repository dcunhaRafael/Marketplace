using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class TermTypeApplication : ITermTypeApplication {
        private readonly ITermTypeDao termTypeDao;

        public TermTypeApplication(
          ITermTypeDao termTypeDao) {
            this.termTypeDao = termTypeDao;
        }

        public async Task<IList<TermTypeEntity>> ListAsync(int coverageExternalCode) {
            return await this.termTypeDao.ListAsync(coverageExternalCode);
        }

        public async Task<TermTypeEntity> GetAsync(int termTypeId, int coverageExternalCode) {
            var item = await this.termTypeDao.GetAsync(termTypeId, coverageExternalCode);
            return item;
        }
    }
}
