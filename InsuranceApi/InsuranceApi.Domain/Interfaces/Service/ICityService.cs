using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface ICityService {
        Task<IList<CidadeConsultarEntity>> ListAsync(int ufCode, string cityName);
    }
}
