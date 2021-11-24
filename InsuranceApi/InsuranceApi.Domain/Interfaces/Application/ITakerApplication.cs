using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ITakerApplication {
        Task<IList<TakerModel>> ListAsync(string name, string cpfCnpj, int? brokerUserId, bool listAll = false);
        Task<TakerModel> GetAsync(string cpfCnpj, int? brokerUserId);
        Task<TakerCalculationParameters> GetParameterAsync(int takerExternalCode);
        Task<TomadorRetornoIncluirEntity> AddAsync(TakerModel taker, int brokerUserId, string ipAddress, string userAgent, int loggedUserId);
    }
}
