using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum TypePoliticsEnum {

        [Description("(BMG) Definição Limite/Rating")]
        DefiniçãoLimiteRating = 1,
        [Description("(BMG) Política de Crédito")]
        PoliticaDeCredito = 2,
        [Description("(BMG) Politica Principal Receita")]
        PoliticaPrincipalReceita = 3,

        [Description("(BMG) ZipOnline 3.0 - Coleta Razão Social")]
        ZipOnline30_ColetaRazaoSocial = 4,
        [Description("(BMG) ZipOnline 3.0 - Coleta Endereço")]
        ZipOnline30_ColetaEndereco = 5,
        [Description("(BMG) ZipOnline 3.0 - Verifica Status SRF")]
        ZipOnline30_VerificaStatusSRF = 6,

        [Description("ZipOnline 3.0 - Consulta por CNPJ - PJ")]
        ZipOnline30_ConsultaPorCnpjPj = 7,
    }
}
