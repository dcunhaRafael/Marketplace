using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IPersonApplication {
        Task<IList<PersonEntity>> ListAsync(string name, long? document, TipoPessoaEnum? personType, StatusTypeEnum? statusType);
    }
}
