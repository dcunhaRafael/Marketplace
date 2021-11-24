using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IDigitalSignatureApplication {
        Task<AssinaturaConsultarEntity> GetAsync(int transactonId);
        Task<string> GetAsync(int transactonId, string brokerCpfCnpj);
        Task<AssinaturaRetornoGravarEntity> AddAsync(AssinaturaGravarEntity entity);
        Task<AssinaturaImpressoEntity> PrintAsync(string url);
    }
}
