using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface IAppParameterDao {

        Task<IList<ParametroEntity>> ListAsync();
        Task<ParametroEntity> GetAsync(AppParameterEnum appParameter);        
    }
}
