using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IAppParameterApplication {
        Task<IList<ParametroEntity>> ListAsync();
        Task<ParametroEntity> GetAsync(AppParameterEnum parameter);
    }
}
