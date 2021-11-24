using InsuranceApi.Domain.Common.Attributes;
using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum PlaceholderEnum {

        [Description("Nº Licitação/Contrato")]
        [DefaultValue("@@NUMERO_LICITACAO@@")]
        [Placeholder("[Informe o Nº Licitação/Contrato]")]
        NumeroLicitacao,

        [Description("Tipo de Recurso")]
        [DefaultValue("@@TIPO_RECURSO@@")]
        [Placeholder("[Selecione o Tipo de Recurso]")]
        TipoRecurso,

        [Description("Nº do Processo")]
        [DefaultValue("@@NUMERO_PROCESSO@@")]
        [Placeholder("[Informe o Número do Processo]")]
        NumeroProcesso,

        [Description("Percentual de Agravo")]
        [DefaultValue("@@PERCENTUAL_AGRAVO@@")]
        [Placeholder("0,00")]
        PercentualAgravo,

        [Description("Nome do LaborCourt")]
        [DefaultValue("@@NOME_TRIBUNAL@@")]
        [Placeholder("[Selecione o LaborCourt]")]
        NomeTribunal,

        [Description("Nome da CivilCourt")]
        [DefaultValue("@@NOME_VARA@@")]
        [Placeholder("[Selecione a CivilCourt]")]
        NomeVara,

        [Description("Nome do Tipo de Ação")]
        [DefaultValue("@@NOME_TIPO_ACAO@@")]
        [Placeholder("[Selecione o Tipo de Ação]")]
        NomeTipoAcao,

        [Description("CPF/CNPJ do Proponente")]
        [DefaultValue("@@CPF_CNPJ_PROPONENTE@@")]
        [Placeholder("[Informe o CPF/CNPJ do Proponente]")]
        CpfCnpjProponente,

        [Description("Nome do Proponente")]
        [DefaultValue("@@NOME_PROPONENTE@@")]
        [Placeholder("[Informe o Nome do Proponente]")]
        NomeProponente,

        [Description("Nº CDA")]
        [DefaultValue("@@NUMERO_CDA@@")]
        [Placeholder("[Informe o Número de CDA]")]
        NumeroCDA,

        [Description("Nº do Processo Administrativo")]
        [DefaultValue("@@NUMERO_PROCESSO_ADMINISTRATIVO@@")]
        [Placeholder("[Informe o Número Processo Administrativo]")]
        NumeroProcessoAdministrativo,

        [Description("Nº do Auto de Infração")]
        [DefaultValue("@@NUMERO_AUTO_INFRACAO@@")]
        [Placeholder("[Informe o Número do Auto de Infração]")]
        NumeroAutoInfracao,

        [Description("Nº da Notificação de Lançamento")]
        [DefaultValue("@@NUMERO_NOTIFIACAO_LANCAMENTO@@")]
        [Placeholder("[Informe o Número da Notificação de Lançamento]")]
        NumeroNotificacaoLancamento,

        [Description("Nome do Tipo de Tributo")]
        [DefaultValue("@@NOME_TIPO_TRIBUTO@@")]
        [Placeholder("[Selecione o Tipo de Tributo]")]
        NomeTipoTributo,

        [Description("Complemento do Tipo de Tributo")]
        [DefaultValue("@@COMPLEMENTO_TIPO_TRIBUTO@@")]
        [Placeholder("[Informe o Complemento do Tipo de Tributo]")]
        ComplementoTipoTributo,

        [Description("E-mail do Solicitante")]
        [DefaultValue("@@EMAIL_SOLICITANTE@@")]
        [Placeholder("[Informe o E-mail do Solicitante]")]
        EmailSolicitante,

        [Description("Nome do Solicitante")]
        [DefaultValue("@@NOME_SOLICITANTE@@")]
        [Placeholder("[Informe o Nome do Solicitante]")]
        NomeSolicitante,

        [Description("Telefone do Solicitante")]
        [DefaultValue("@@TELEFONE_SOLICITANTE@@")]
        [Placeholder("[Informe o Telefone do Solicitante]")]
        TelefoneSolicitante,

        [Description("Lista de Reclamantes (Nome e CPF/CNPJ)")]
        [DefaultValue("@@LISTA_RECLAMANTES_NOME_CPF_CNPJ@@")]
        [Placeholder("[Informe os dados dos reclamantes]")]
        ListaReclamantesNomeCpfCnpj,

        [Description("Lista de Reclamantes (CPF/CNPJ e Nome)")]
        [DefaultValue("@@LISTA_RECLAMANTES_CPF_CNPJ_NOME@@")]
        [Placeholder("[Informe os dados dos reclamantes]")]
        ListaReclamantesCpfCnpjNome,

        [Description("Lista de Reclamantes (CPF/CNPJ apenas)")]
        [DefaultValue("@@LISTA_RECLAMANTES_CPF_CNPJ@@")]
        [Placeholder("[Informe os dados dos reclamantes]")]
        ListaReclamantesCpfCnpj,

        [Description("Lista de Reclamantes (Nome apenas)")]
        [DefaultValue("@@LISTA_RECLAMANTES_NOME@@")]
        [Placeholder("[Informe os dados dos reclamantes]")]
        ListaReclamantesNome,

        [Description("CPF/CNPJ do Reclamante Principal")]
        [DefaultValue("@@RECLAMANTE_PRINCIPAL_CPF_CNPJ@@")]
        [Placeholder("[Informe os dados do reclamante principal]")]
        ReclamantePrincipalCpfCnpj,

        [Description("Nome do Reclamante Principal")]
        [DefaultValue("@@RECLAMANTE_PRINCIPAL_NOME@@")]
        [Placeholder("[Informe os dados do reclamante principal]")]
        ReclamantePrincipalNome,

        [Description("Valor da Importância Segurada")]
        [DefaultValue("@@VALOR_IMPORTANCIA_SEGURADA@@")]
        [Placeholder("[Informe o valor da importância segurada]")]
        ValorImportanciaSegurada,

        [Description("Valor da Discussão")]
        [DefaultValue("@@VALOR_DISCUSSAO@@")]
        [Placeholder("[Informe o valor da discussão]")]
        ValorDiscussao,

        [Description("Início de Vigência")]
        [DefaultValue("@@INICIO_VIGENCIA@@")]
        [Placeholder("[Informe o início de vigência]")]
        InicioVigencia,

        [Description("Final de Vigência")]
        [DefaultValue("@@FINAL_VIGENCIA@@")]
        [Placeholder("[Informe o final de vigência]")]
        FinalVigencia,

        [Description("TermType de Vigência")]
        [DefaultValue("@@PRAZO_VIGENCIA@@")]
        [Placeholder("[Selecione o prazo de vigência]")]
        PrazoVigencia,

        [Description("Índice de Atualização")]
        [DefaultValue("@@INDICE_ATUALIZACAO@@")]
        [Placeholder("[Selecione o índice de atualização]")]
        IndiceAtualizacao,

        [Description("Número da Proposta")]
        [DefaultValue("@@NUMERO_PROPOSTA@@")]
        [Placeholder("[Número da proposta]")]
        NumeroProposta,

        [Description("Número da Apólice")]
        [DefaultValue("@@NUMERO_APOLICE@@")]
        [Placeholder("[Número da apólice]")]
        NumeroApolice,
    }

    public static class PlaceholderEnumUtil {
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from tipoEnti in System.Enum.GetValues(typeof(PlaceholderEnum)).Cast<PlaceholderEnum>()
                   select new KeyValuePair<string, string>(tipoEnti.ToDefaultValue(),
                                                            tipoEnti.ToDescription());
        }
    }
}
