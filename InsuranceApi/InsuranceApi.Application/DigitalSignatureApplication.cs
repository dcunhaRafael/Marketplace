using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Service;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class DigitalSignatureApplication : IDigitalSignatureApplication {
        private readonly ISignatureDigitalService digitalSignatureService;
        private readonly IAppParameterApplication appParameterApplication;

        public DigitalSignatureApplication(ISignatureDigitalService digitalSignatureService, IAppParameterApplication appParameterApplication) {
            this.digitalSignatureService = digitalSignatureService;
            this.appParameterApplication = appParameterApplication;
        }

        public async Task<AssinaturaConsultarEntity> GetAsync(int transactionId) {
            var origemEmpresa = await appParameterApplication.GetAsync(AppParameterEnum.OrigemEmpresaAssinatura);
            return await digitalSignatureService.GetAsync(transactionId, origemEmpresa.Value);
        }

        public async Task<string> GetAsync(int transactionId, string brokercpfCnpj) {
            var origemEmpresa = await appParameterApplication.GetAsync(AppParameterEnum.OrigemEmpresaPropostaAssinadaAssinatura);
            var tipoAsssinatura = await appParameterApplication.GetAsync(AppParameterEnum.TipoAssinaturaAutomatica);
            return await digitalSignatureService.GetAsync(transactionId, origemEmpresa.Value, brokercpfCnpj, tipoAsssinatura.Value);
        }

        public async Task<AssinaturaRetornoGravarEntity> AddAsync(AssinaturaGravarEntity entity) {
            return await digitalSignatureService.AddAsync(entity);
        }

        public async Task<AssinaturaImpressoEntity> PrintAsync(string url) {
            return await digitalSignatureService.PrintAsync(url);
        }
    }
}
