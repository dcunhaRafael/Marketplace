using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IPersonDao {
        Task<IList<PersonEntity>> ListAsync(string name, string document, TipoPessoaEnum? personType, StatusTypeEnum? statusType);
    }
}
