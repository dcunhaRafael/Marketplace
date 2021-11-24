using System.ComponentModel;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum PoliticsCreditEnum {

        [Description("RECUSA_PRINCIPAL")]
        RECUSAPRINCIPAL,

        [Description("EXECUTA")]
        EXECUTA,

        [Description("DEFINE_RATING")]
        DEFINE_RATING,

        [Description("UPPER MIDDLE")]
        UpperMiddle,

        [Description("RECUSA")]
        RECUSA,

        [Description("MV_PONTUACAO_BMG")]
        MV_PONTUACAO_BMG,

        [Description("MV_RATING")]
        MV_RATING,

        [Description("MV_LIMITE")]
        MV_LIMITE,

        [Description("MV_TAXA")]
        MV_TAXA,

        [Description("MV_RAZAO_SOCIAL")]
        MV_RAZAO_SOCIAL,

        [Description("MV_LOGRADOURO")]
        MV_LOGRADOURO,

        [Description("MV_NR_LOGRADOURO")]
        MV_NR_LOGRADOURO,

        [Description("MV_UF")]
        MV_UF,

        [Description("EXECUTA_MV_INTEGRACAO")]
        EXECUTA_MV_INTEGRACAO,

        [Description("MV_COMPLEMENTO")]
        MV_COMPLEMENTO,

        [Description("MV_CIDADE")]
        MV_CIDADE,

        [Description("MV_CEP")]
        MV_CEP,

        [Description("MV_BAIRRO")]
        MV_BAIRRO,

        [Description("MV_NR_TELEFONE")]
        MV_NR_TELEFONE,

        [Description("MV_DT_CONSULTA")]
        MV_DT_CONSULTA,

        [Description("Discreta - Razão social")]
        DiscretaRazaoSocial,
        [Description("Discreta - Logradouro - endereço - situação SRF")]
        DiscretaLogradouroEnderecoSituacaoSRF,
        [Description("Discreta - Número do logradouro - endereço - situação SRF")]
        DiscretaNumeroLogradouroEnderecoSituacaoSRF,
        [Description("Discreta - CEP - endereço - situação SRF")]
        DiscretaCepEnderecoSituacaoSRF,
        [Description("Discreta - Bairro - endereço - situação SRF")]
        DiscretaBairroEnderecoSituacaoSRF,
        [Description("Discreta - Cidade - endereço - situação SRF")]
        DiscretaCidadeEnderecoSituacaoSRF,
        [Description("Discreta - Complemento do logradouro - endereço - situação SRF")]
        DiscretaComplementoEnderecoSituacaoSRF,
        [Description("Discreta - UF - endereço - situação SRF")]
        DiscretaUfEnderecoSituacaoSRF,
    }
}
