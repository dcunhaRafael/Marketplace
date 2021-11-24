using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class PersonApplication : IPersonApplication {
        private readonly IPersonDao personDao;

        public PersonApplication(
        IPersonDao personDao) {
            this.personDao = personDao;
        }

        public async Task<IList<PersonEntity>> ListAsync(string name, long? document, TipoPessoaEnum? personType, StatusTypeEnum? statusType) {
            if (string.IsNullOrWhiteSpace(name)) {
                name = null;
            }
            string documentStr = null;
            if (document != null) {
                documentStr = document.FormatCpfCnpjLongToString();
            }
            return await personDao.ListAsync(name, documentStr, personType, statusType);
        }
    }
}
