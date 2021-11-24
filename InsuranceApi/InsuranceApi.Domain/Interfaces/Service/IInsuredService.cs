using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IInsuredService {
        Task<IList<InsuredEntity>> ListAsync(SeguradoBuscarEntity filters);
        Task<SeguradoRetornoIncluirEntity> AddAsync(InsuredEntity insured);
    }
}
