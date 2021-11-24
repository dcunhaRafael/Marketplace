using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class InsuredObjectApplication : IInsuredObjectApplication {
        private readonly IInsuredObjectDao insuredObjectDao;

        public InsuredObjectApplication(IInsuredObjectDao insuredObjectDao) {
            this.insuredObjectDao = insuredObjectDao;
        }

        public async Task<string> GetTextAsync(ProposalEntity proposal) {
            string textoObjetoSegurado = "";

            var objetoSegurado = await insuredObjectDao.GetAsync(proposal.DadosGarantia.Coverage.IdCobertura.Value);

            IList<InsuredObjectBlockEntity> insuredObjectBlocks = new List<InsuredObjectBlockEntity>();
            if (objetoSegurado != null) {
                insuredObjectBlocks = await insuredObjectDao.ListBlockAsync(objetoSegurado.InsuredObjectId.Value, RecordStatusEnum.Ativo);
            }
            if (insuredObjectBlocks != null && insuredObjectBlocks.Count > 0) {
                StringBuilder insuredOjectFilledText = new StringBuilder();
                foreach (var block in insuredObjectBlocks) {
                    var variableCount = block.Contents.PatternCount("@@") / 2;
                    var filledBlock = FillTextBlock(block.Contents, proposal);
                    var notFilledCount = filledBlock.PatternCount("@@") / 2;

                    if (block.StartInNewLine) {
                        insuredOjectFilledText.Append("\n");
                    }

                    switch (block.PrintMode) {
                        case InsuredObjectPrintModeEnum.Always:
                            insuredOjectFilledText.Append(filledBlock);
                            break;
                        case InsuredObjectPrintModeEnum.IfAtLeastOneVariableIsFilled:
                            if (variableCount > notFilledCount) {
                                insuredOjectFilledText.Append(filledBlock);
                            }
                            break;
                        case InsuredObjectPrintModeEnum.IfAllVariablesAreFilled:
                            if (notFilledCount == 0) {
                                insuredOjectFilledText.Append(filledBlock);
                            }
                            break;
                    }
                }

                textoObjetoSegurado = insuredOjectFilledText.ToString();
            }

            return textoObjetoSegurado;
        }

        public string FillTextBlock(string textoPadrao, ProposalEntity model) {
            StringBuilder htmlDescricaoSegurado = new StringBuilder(textoPadrao);

            //-- Nº Licitação/Contrato
            var numeroLicitacao = model.DadosGarantia.NumeroLicitacao;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NumeroLicitacao.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(numeroLicitacao) ? PlaceholderEnum.NumeroLicitacao.ToPlaceholder() : numeroLicitacao);

            //-- Tipo de recurso
            var tipoRecurso = model.DadosGarantia.LegalRecourseType?.Name;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.TipoRecurso.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(tipoRecurso) ? PlaceholderEnum.TipoRecurso.ToPlaceholder() : tipoRecurso);

            //-- Número do processo
            var numeroProcesso = model.DadosGarantia.NumeroProcesso;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NumeroProcesso.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(numeroProcesso) ? PlaceholderEnum.NumeroProcesso.ToPlaceholder() : numeroProcesso);

            //-- Percentual de agravo
            var percentualAgravo = model.DadosGarantia.PercentualAgravo;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.PercentualAgravo.ToDefaultValue(),
                                                                  percentualAgravo == null ? PlaceholderEnum.PercentualAgravo.ToPlaceholder() : percentualAgravo?.FormatDecimal());

            //-- LaborCourt
            var tribunal = model.DadosGarantia.LaborCourt?.Name;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NomeTribunal.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(tribunal) ? PlaceholderEnum.NomeTribunal.ToPlaceholder() : tribunal);

            //-- CivilCourt
            var vara = model.DadosGarantia.CivilCourt?.Name;
            if (!string.IsNullOrEmpty(vara)) {
                htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NomeVara.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(vara) ? PlaceholderEnum.NomeVara.ToPlaceholder() : vara);
            } else {                
                htmlDescricaoSegurado =  htmlDescricaoSegurado.Replace(PlaceholderEnum.NomeVara.ToDefaultValue(), "");
            }
            //-- Tipo de Ação
            var tipoAcao = "";
            if (model.DadosGarantia.TipoAcao?.IsComplementRequired == true) {
                tipoAcao = model.DadosGarantia.DescricaoOutroTipoAcao;
            } else {
                tipoAcao = model.DadosGarantia.TipoAcao?.Name;
            }
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NomeTipoAcao.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(tipoAcao) ? PlaceholderEnum.NomeTipoAcao.ToPlaceholder() : tipoAcao);
            //-- Número de CDA
            var numeroCDA = model.DadosGarantia.NumeroCDA;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NumeroCDA.ToDefaultValue(),
                                                                  numeroCDA == null ? PlaceholderEnum.NumeroCDA.ToPlaceholder() : numeroCDA.ToString());


            //-- Número Processo Administrativo
            var numeroProcAdm = model.DadosGarantia.NumeroProcessoAdministrativo;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NumeroProcessoAdministrativo.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(numeroProcAdm) ? PlaceholderEnum.NumeroProcessoAdministrativo.ToPlaceholder() : numeroProcAdm);

            //-- Tipo de tributo
            var tipoTributo = model.DadosGarantia.TipoTributo?.Name;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NomeTipoTributo.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(tipoTributo) ? PlaceholderEnum.NomeTipoTributo.ToPlaceholder() : tipoTributo);

            //-- Complemento do tipo de tributo
            var complementoTipoTributo = model.DadosGarantia.NumeroProcessoAdministrativo;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ComplementoTipoTributo.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(complementoTipoTributo) ? PlaceholderEnum.ComplementoTipoTributo.ToPlaceholder() : complementoTipoTributo);

            //-- Solicitante
            var emailSolicitante = model.DadosGarantia.EmailSolicitante;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.EmailSolicitante.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(emailSolicitante) ? PlaceholderEnum.EmailSolicitante.ToPlaceholder() : emailSolicitante);
            var nomeSolicitante = model.DadosGarantia.NomeSolicitante;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NomeSolicitante.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(nomeSolicitante) ? PlaceholderEnum.NomeSolicitante.ToPlaceholder() : nomeSolicitante);
            var telefoneSolicitante = model.DadosGarantia.TelefoneSolicitante;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.TelefoneSolicitante.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(telefoneSolicitante) ? PlaceholderEnum.TelefoneSolicitante.ToPlaceholder() : telefoneSolicitante);

            //-- Nome ou CPF/CNPJ do Reclamante Principal
            var reclamantePrincipal = model.DadosGarantia.Reclamantes.FirstOrDefault(x => x.IsPrincipal == true);
            if (reclamantePrincipal != null) {

                //-- Nome
                htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ReclamantePrincipalNome.ToDefaultValue(),
                                                                      string.IsNullOrWhiteSpace(reclamantePrincipal.NomeReclamante) ? PlaceholderEnum.ReclamantePrincipalNome.ToPlaceholder() :
                                                                      reclamantePrincipal.NomeReclamante);

                //-- CPF/CNPJ
                htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ReclamantePrincipalCpfCnpj.ToDefaultValue(),
                                                                      reclamantePrincipal.CpfCnpjReclamante == null ? PlaceholderEnum.ReclamantePrincipalCpfCnpj.ToPlaceholder() :
                                                                      reclamantePrincipal.CpfCnpjReclamante?.FormatLongToCpfCnpj());
            }

            //-- Lista de reclamantes (Apenas CPF/CNPJ)
            StringBuilder reclamantes = new StringBuilder();
            for (int i = 0; i < model.DadosGarantia.Reclamantes.Count; i++) {
                if (i > 0) {
                    if (i == model.DadosGarantia.Reclamantes.Count - 1) {
                        reclamantes.Append(" e ");
                    } else {
                        reclamantes.Append(", ");
                    }
                }
                reclamantes.Append("CPF/CNPJ n°: ").Append(model.DadosGarantia.Reclamantes[i].CpfCnpjReclamante?.FormatLongToCpfCnpj());
            }
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ListaReclamantesCpfCnpj.ToDefaultValue(),
                                                                  reclamantes.Length == 0 ? PlaceholderEnum.ListaReclamantesCpfCnpj.ToPlaceholder() : reclamantes.ToString());

            //-- Lista de reclamantes (Apenas nome)
            reclamantes = new StringBuilder();
            for (int i = 0; i < model.DadosGarantia.Reclamantes.Count; i++) {
                if (i > 0) {
                    if (i == model.DadosGarantia.Reclamantes.Count - 1) {
                        reclamantes.Append(" e ");
                    } else {
                        reclamantes.Append(", ");
                    }
                }
                reclamantes.Append(model.DadosGarantia.Reclamantes[i].NomeReclamante);
            }
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ListaReclamantesNome.ToDefaultValue(),
                                                                  reclamantes.Length == 0 ? PlaceholderEnum.ListaReclamantesNome.ToPlaceholder() : reclamantes.ToString());

            //-- Lista de reclamantes (Nome e CPF/CNPJ)
            reclamantes = new StringBuilder();
            for (int i = 0; i < model.DadosGarantia.Reclamantes.Count; i++) {
                if (i > 0) {
                    if (i == model.DadosGarantia.Reclamantes.Count - 1) {
                        reclamantes.Append(" e ");
                    } else {
                        reclamantes.Append(", ");
                    }
                }
                var item = model.DadosGarantia.Reclamantes[i];
                reclamantes.Append(item.NomeReclamante);
                reclamantes.Append(" - CPF/CNPJ n°: ").Append(item.CpfCnpjReclamante?.FormatLongToCpfCnpj());
            }
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ListaReclamantesNomeCpfCnpj.ToDefaultValue(),
                                                                  reclamantes.Length == 0 ? PlaceholderEnum.ListaReclamantesNomeCpfCnpj.ToPlaceholder() : reclamantes.ToString());

            //-- Lista de reclamantes (CPF/CNPJ e Nome)
            reclamantes = new StringBuilder();
            for (int i = 0; i < model.DadosGarantia.Reclamantes.Count; i++) {
                if (i > 0) {
                    if (i == model.DadosGarantia.Reclamantes.Count - 1) {
                        reclamantes.Append(" e ");
                    } else {
                        reclamantes.Append(", ");
                    }
                }
                var item = model.DadosGarantia.Reclamantes[i];
                reclamantes.Append("CPF/CNPJ n°: ").Append(item.CpfCnpjReclamante?.FormatLongToCpfCnpj());
                reclamantes.Append(" - ").Append(item.NomeReclamante);
            }
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ListaReclamantesCpfCnpjNome.ToDefaultValue(),
                                                                  reclamantes.Length == 0 ? PlaceholderEnum.ListaReclamantesCpfCnpjNome.ToPlaceholder() : reclamantes.ToString());

            //-- Valor da importância segurada
            var valorImportanciaSegurada = model.DadosGarantia.ValorImportanciaSegurada;
            if (model.TipoSeguro == TipoSeguroEnum.Recursal) {
                valorImportanciaSegurada = model.DadosGarantia.ValorImportanciaSeguradaRecursal;
            } else if (model.TipoSeguro == TipoSeguroEnum.JudicialCivel || model.TipoSeguro == TipoSeguroEnum.JudicialFiscal || model.TipoSeguro == TipoSeguroEnum.JudicialTrabalhista) {
                valorImportanciaSegurada = model.DadosGarantia.ValorImportanciaSeguradaJudicial;
            }
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ValorImportanciaSegurada.ToDefaultValue(),
                                                                  valorImportanciaSegurada == null ? PlaceholderEnum.ValorImportanciaSegurada.ToPlaceholder() : valorImportanciaSegurada?.FormatDecimal());

            //-- Valor da discussão
            var valorDiscussao = model.DadosGarantia.ValorDiscussaoJudicial;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.ValorDiscussao.ToDefaultValue(),
                                                                  valorDiscussao == null ? PlaceholderEnum.ValorDiscussao.ToPlaceholder() : valorDiscussao?.FormatDecimal());

            //-- Início de Vigência
            var inicioVigencia = model.DataInicioVigencia;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.InicioVigencia.ToDefaultValue(),
                                                                  inicioVigencia == null ? PlaceholderEnum.InicioVigencia.ToPlaceholder() : inicioVigencia?.FormatDate());

            //-- Final de Vigência
            var finalVigencia = model.DataFimVigencia;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.FinalVigencia.ToDefaultValue(),
                                                                  finalVigencia == null ? PlaceholderEnum.FinalVigencia.ToPlaceholder() : finalVigencia?.FormatDate());

            //-- TermType de Vigência
            var prazoVigencia = model.DadosGarantia.TermType?.Name;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.PrazoVigencia.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(prazoVigencia) ? PlaceholderEnum.PrazoVigencia.ToPlaceholder() : prazoVigencia);

            //-- Número da Proposta
            var numeroProposta = model.CodigoProposta == 0 ? "" : model.CodigoProposta.ToString();
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NumeroProposta.ToDefaultValue(),
                                                                  string.IsNullOrWhiteSpace(numeroProposta) ? PlaceholderEnum.NumeroProposta.ToPlaceholder() : numeroProposta);

            //-- Número da Apólice
            var numeroApolice = model.CodigoApolice;
            htmlDescricaoSegurado = htmlDescricaoSegurado.Replace(PlaceholderEnum.NumeroApolice.ToDefaultValue(),
                                                                  numeroApolice == null ? PlaceholderEnum.NumeroApolice.ToPlaceholder() : numeroApolice.ToString());

            return htmlDescricaoSegurado.ToString();
        }
    }
}
