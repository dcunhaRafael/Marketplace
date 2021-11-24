using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface ITakerService {
        Task<IList<TakerModel>> ListAsync(string name, string cpfCnpj, int? brokerUserId, bool listAll = false);
        Task<TomadorRetornoIncluirEntity> AddAsync(TakerModel taker, int? brokerUserId);
        Task<TakerCalculationParameters> GetParametersAsync(int takerExternalCode);
    }
}
