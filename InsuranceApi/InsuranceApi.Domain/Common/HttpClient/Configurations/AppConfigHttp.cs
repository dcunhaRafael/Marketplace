using InsuranceApi.Domain.BusinessObjects.AppSettings;
using Microsoft.Extensions.Options;
using System;

namespace InsuranceApi.Domain.Common.HttpClient.Configurations {
    public abstract class AppConfigHttp {

        private static IOptions<ServiceSettings> _serviceSettings;

        public AppConfigHttp(IOptions<ServiceSettings> serviceSettings) {
            _serviceSettings = serviceSettings;
        }

        public static int CodigoUsuarioPadraoEbix { get { return IntFromAppSettings("CodigoUsuarioPadraoEbix"); } }
        public static int CodigoUsuarioPadraoMarketplace { get { return IntFromAppSettings("CodigoUsuarioPadraoMarketplace"); } }
        public static string CargaTomadorAssinaturaDigitalTemplate { get { return StringFromAppSettings("CargaTomadorAssinaturaDigitalTemplate"); } }
        public static int DefaultUserId { get { return IntFromAppSettings("CodigoUsuarioPadraoServico"); } }
        public static string DefaultSusepCode { get { return StringFromAppSettings("CodigoUsuarioSusep"); } }
        public static string RotaTomadorBuscar { get { return StringFromAppSettings("RotaTomadorBuscar"); } }
        public static string RotaTomadorIncluir { get { return StringFromAppSettings("RotaTomadorIncluir"); } }
        public static string RotaProdutoBuscar { get { return StringFromAppSettings("RotaProdutoBuscar"); } }
        public static string RotaSeguradoPesquisar { get { return StringFromAppSettings("RotaSeguradoPesquisar"); } }
        public static string RotaSeguradoIncluir { get { return StringFromAppSettings("RotaSeguradoIncluir"); } }
        public static string RotaCepBuscar { get { return StringFromAppSettings("RotaCepBuscar"); } }
        public static string RotaConsultaCidade { get { return StringFromAppSettings("RotaConsultaCidade"); } }
        public static string RotaCorretorBuscar { get { return StringFromAppSettings("RotaCorretorBuscar"); } }
        public static string RotaPropostaGravar { get { return StringFromAppSettings("RotaPropostaGravar"); } }
        public static string RotaConsultaCorretor { get { return StringFromAppSettings("RotaConsultaCorretor"); } }
        public static string RotaTomadorBuscarParametrosCalculo { get { return StringFromAppSettings("RotaTomadorBuscarParametrosCalculo"); } }
        public static string RotaPropostaImprimirMinuta { get { return StringFromAppSettings("RotaPropostaImprimirMinuta"); } }
        public static string RotaCoberturaBuscar { get { return StringFromAppSettings("RotaCoberturaBuscar"); } }
        public static string RotaPropostaVerificarAprovacao { get { return StringFromAppSettings("RotaPropostaVerificarAprovacao"); } }
        public static string RotaPropostaAprovar { get { return StringFromAppSettings("RotaPropostaAprovar"); } }
        public static string RotaPropostaPesquisar { get { return StringFromAppSettings("RotaPropostaPesquisar"); } }
        public static string ProposalSignatureDigitalClientServiceEndpoint { get { return StringFromAppSettings("ProposalSignatureDigitalClientServiceEndpoint"); } }
        public static string InsuredClientServiceEndpoint { get { return StringFromAppSettings("InsuredClientServiceEndpoint"); } }
        public static string SearchClientServiceEndpoint { get { return StringFromAppSettings("SearchClientServiceEndpoint"); } }
        public static string CityClientServiceEndpoint { get { return StringFromAppSettings("CityClientServiceEndpoint"); } }
        public static string PolicyClientServiceEndpoint { get { return StringFromAppSettings("PolicyClientServiceEndpoint"); } }
        public static string CoverageClientServiceEndpoint { get { return StringFromAppSettings("CoverageClientServiceEndpoint"); } }
        public static string ProductClientServiceEndpoint { get { return StringFromAppSettings("ProductClientServiceEndpoint"); } }
        public static string TakerClientServiceEndpoint { get { return StringFromAppSettings("TakerClientServiceEndpoint"); } }
        public static string ProposalClientServiceEndpoint { get { return StringFromAppSettings("ProposalClientServiceEndpoint"); } }
        public static string BrokerClientServiceEndpoint { get { return StringFromAppSettings("BrokerClientServiceEndpoint"); } }
        public static string SignatureDigitalClientServiceEndpoint { get { return StringFromAppSettings("SignatureDigitalClientServiceEndpoint"); } }
        public static string ProposalTransmissionClientServiceEndpoint { get { return StringFromAppSettings("ProposalTransmissionClientServiceEndpoint"); } }
        public static string RotaAssinaturaGravar { get { return StringFromAppSettings("RotaAssinaturaGravar"); } }
        public static string RotaAssinaturaConsultar { get { return StringFromAppSettings("RotaAssinaturaConsultar"); } }
        public static string RotaImpressaoAssinatura { get { return StringFromAppSettings("RotaImpressaoAssinatura"); } }
        public static string RotaApoliceEmitir { get { return StringFromAppSettings("RotaApoliceEmitir"); } }
        public static string RotaApolicePesquisar { get { return StringFromAppSettings("RotaApolicePesquisar"); } }
        public static string RotaApoliceImprimirBoleto { get { return StringFromAppSettings("RotaApoliceImprimirBoleto"); } }
        public static string RotaApoliceImprimir { get { return StringFromAppSettings("RotaApoliceImprimir"); } }
        public static string BCDataClientServiceEndpoint { get { return StringFromAppSettings("BCDataClientServiceEndpoint"); } }
        public static string RotaConsultarSeries { get { return StringFromAppSettings("RotaConsultarSeries"); } }
        

        public static string StringFromAppSettings(string appSettingsName) {

            string value;
            _serviceSettings.Value.TryGetValue(appSettingsName, out value);

            if (String.IsNullOrEmpty(value))
                throw new Exception("Parâmetro '" + appSettingsName + "' não configurado.");
            else
                return value;
        }
        public static int IntFromAppSettings(string appSettingsName) {

            string value;
            _serviceSettings.Value.TryGetValue(appSettingsName, out value);

            if (String.IsNullOrEmpty(value)) {
                throw new Exception("Parâmetro '" + appSettingsName + "' não configurado.");
            } else {
                if (!int.TryParse(value, out int intValue)) {
                    throw new Exception("Parâmetro '" + appSettingsName + "' inválido.");
                } else {
                    return intValue;
                }
            }
        }
    }
}
