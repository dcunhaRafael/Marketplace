using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class CrivoApplication : ICrivoApplication {
        private readonly ICrivoService crivoService;
        private readonly IAppParameterApplication appParameterApplication;

        public CrivoApplication(ICrivoService crivoService, IAppParameterApplication appParameterApplication) {
            this.crivoService = crivoService;
            this.appParameterApplication = appParameterApplication;
        }

        public async Task<CreditPolicyEntity> GetCreditPolicyAsync(string cnpj) {
            var crivoData = new CreditPolicyEntity();
            var isConsultaSerasa = await appParameterApplication.GetAsync(AppParameterEnum.ConsultaSerasa);

            if (Convert.ToBoolean(isConsultaSerasa.Value)) {

                var user = await appParameterApplication.GetAsync(AppParameterEnum.UsuarioCrivo);
                var password = await appParameterApplication.GetAsync(AppParameterEnum.SenhaCrivo);
                var endpoint = await appParameterApplication.GetAsync(AppParameterEnum.EndpointCrivo);

                var dadosChamada = new GetCrivoEntity() {
                    Usuario = user.Value,
                    Senha = password.Value,
                    Politica = EnumExtension.GetEnumDescription(TypePoliticsEnum.PoliticaDeCredito),
                    Parametros = String.Format("E{0}CNPJ;{1}", Environment.NewLine, cnpj),
                    Endpoint = endpoint.Value,
                };
                crivoData = await crivoService.GetAsync(dadosChamada);
            }
            return crivoData;
        }

        public async Task<CreditPolicyEntity> BuscarPoliticaDeCredito(GetCrivoEntity crivoFiltros) {
            return await crivoService.GetAsync(crivoFiltros);
        }
    }
}
