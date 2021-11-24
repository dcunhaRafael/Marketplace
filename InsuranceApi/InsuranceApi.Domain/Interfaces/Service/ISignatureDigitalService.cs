using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface ISignatureDigitalService {
        Task<AssinaturaRetornoGravarEntity> AddAsync(AssinaturaGravarEntity signature);
        Task<AssinaturaConsultarEntity> GetAsync(int transactionId, string companyOrigin);
        Task<string> GetAsync(int transactionId, string insurerCpfCnpj, string brokerCpfcnpj, string signatureType);
        Task<AssinaturaImpressoEntity> PrintAsync(string url);
        Task<AssinaturaTomadorRetornoEntity> AddAsync(AssinaturaTomadorEntity signature);
    }
}
