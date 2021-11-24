using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum AppParameterEnum {


        //--------------------------------------------------------------------------
        //-- Códigos gerais do portal (Faixa de 0 a ....)
        //--------------------------------------------------------------------------
        [Description("Código do Product Padrão Utilizado no Portal")]
        CodigoProdutoPadrao = 0,

        [Description("Código do Product Recursal Utilizado no Portal")]
        CodigoProdutoRecursal = 5,

        [Description("Modalidade para tipo de seguro 'Licitação'")]
        ModalidadeTipoSeguroLicitacao = 1,

        [Description("Modalidade para tipo de seguro 'Contrato'")]
        ModalidadeTipoSeguroContrato = 2,

        [Description("Modalidade para tipo de seguro 'Recursal'")]
        ModalidadeTipoSeguroRecursal = 3,

        [Description("Modalidade para tipo de seguro 'Recursal SME'")]
        ModalidadeTipoSeguroRecursalSME = 11,

        [Description("Motivo de cancelamento de proposta padrão")]
        PropostaMotivoCancelamentoPadrao = 4,

        [Description("Inclusão manual de tomador exige dados do representante legal")]
        InclusaoManualTomadorExigeRepresentante = 6,

        [Description("Codigo identificação da empresa assinatura digital")]
        OrigemEmpresaAssinatura = 8,

        [Description("Codigo identificação da empresa assinatura digital")]
        OrigemEmpresaPropostaAssinadaAssinatura = 9,
       
        [Description(" Tipo da assinatura automatica, utilizado quando existe mais de uma forma de assinatura para a empresa informada.")]
        TipoAssinaturaAutomatica = 10,

        //--------------------------------------------------------------------------
        //-- Códigos para uso de Licitação (Faixa de 1000 a ....)
        //--------------------------------------------------------------------------
        //...

        //--------------------------------------------------------------------------
        //-- Códigos para uso de Contrato (Faixa de 2000 a ....)
        //--------------------------------------------------------------------------
        //...

        //--------------------------------------------------------------------------
        //-- Códigos para uso do Recursal (Faixa de 3000 a ....)
        //--------------------------------------------------------------------------

        [Description("Recursal - Texto do Objeto Insured (Sem agravo)")]
        RecursalTextoObjetoSeguradoSemAgravo = 3000,
        [Description("Recursal - Texto do Objeto Insured (Com agravo)")]
        RecursalTextoObjetoSeguradoComAgravo = 3001,

        [Description("Recursal - TermType Padrão")]
        RecursalPrazoPadrao = 3002,

        [Description("Recursal - Periodicidade de Pagamento Padrão")]
        RecursalPeridiocidadePagamentoPadrao = 3003,

        [Description("Recursal - Forma de Pagamento Padrão")]
        RecursalFormaPagamentoPadrao = 3004,

        [Description("Recursal - Parcelamento Padrão")]
        RecursalParcelamentoPadrao = 3005,

        [Description("Recursal - CEP Padrão do Insured")]
        RecursalCepPadraoSegurado = 3010,

        //--------------------------------------------------------------------------
        //-- Códigos para uso no Crivo (Faixa de ???? a ....)
        //--------------------------------------------------------------------------

        [Description("Crivo sistema que permite a automação do processo de tomada de decisão de análise de crédito/risco de pessoas físicas e jurídicas")]
        EndpointCrivo = 3006,

        [Description("Campo defini se havera consulta ao crivo no momento do cadastro do tomador")]
        ConsultaSerasa = 3007,

        [Description("Usuario de acesso ao serviço crivo")]
        UsuarioCrivo = 3008,

        [Description("Senha de acesso ao serviço crivo")]
        SenhaCrivo = 3009,

        //--------------------------------------------------------------------------
        //-- Códigos para uso nos serviços e batches (Faixa de 9000 a ....)
        //--------------------------------------------------------------------------

        [Description("Integration Services - Código de usuário padrão para acesso ao legado")]
        IntegrationServicesLegacyUserId = 9000,

    }

    public static class AppParameterEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(AppParameterEnum)).Cast<AppParameterEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }
    }
}