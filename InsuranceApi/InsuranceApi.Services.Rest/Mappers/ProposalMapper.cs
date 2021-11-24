using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class ProposalMapper {

        public static PropostaGravarRequest MapPropostaGravarEntityToRequest(ProposalEntity entity, int idUsuarioCorretor, string susepCorretor) {
            try {
                var mappedObj = new PropostaGravarRequest()
                {
                    dt_inclusao = BaseMapper.DateToString(entity.DataProposta),
                    dt_inicio_vigencia = BaseMapper.DateToString(entity.DataInicioVigencia),
                    dt_final_vigencia = BaseMapper.DateToString(entity.DataFimVigencia),
                    cd_susep = susepCorretor, //BaseMapper.DefaultSusepCode, //entity.CodigoSusep,
                    id_pessoa_tomador = BaseMapper.IntToString(entity.Taker.IdPessoa),
                    id_endereco_tomador = BaseMapper.IntToString(entity.Taker.Endereco.IdEndereco),
                    id_pessoa_cliente = BaseMapper.IntToString(entity.Insured.IdPessoa),
                    id_endereco_cliente = BaseMapper.IntToString(entity.Insured.Endereco.IdEndereco),
                    id_usuario = idUsuarioCorretor.ToString(), //BaseMapper.DefaultUserId.ToString(), //BaseMapper.IntToString(entity.UsuarioInclusao.CodigoUsuario),
                    cd_produto = BaseMapper.IntToString(entity.Product.CodigoExterno),
                    cd_status = BaseMapper.IntToString((int)entity.StatusProposta),
                    dados_cobranca_dt_vencimento_pparcela = BaseMapper.DateToString(entity.DadosCobranca.DataVencimentoPrimeiraParcela),
                    dados_cobranca_dias_demais_pparcela = BaseMapper.IntToString(entity.DadosCobranca.DiaCobranca),
                    dados_cobranca_cd_forma_pagamento_pparcela = BaseMapper.IntToString(entity.DadosCobranca.FormaPagamentoPrimeiraParcela.CodigoFormaPagamento),
                    dados_cobranca_cd_forma_pagamento = BaseMapper.IntToString(entity.DadosCobranca.FormaPagamentoDemaisParcelas?.CodigoFormaPagamento),
                    dados_cobranca_id_produto_parc_premio = BaseMapper.IntToString(entity.DadosCobranca.Parcelamento.IdParcelamento),
                    dados_cobranca_id_periodo_pagamento = BaseMapper.IntToString(entity.DadosCobranca.PeridiocidadePagamento.IdPeridiocidadePagamento),
                    dados_garantia_id_produto_cobertura = entity.DadosGarantia.Coverage.ExternalCode.ToString(),
                    dados_garantia_vl_premio_tarifario = BaseMapper.DecimalToString(entity.DadosGarantia.ValorPremioTarifario),
                    dados_garantia_pe_comissao = BaseMapper.DecimalToString(entity.DadosGarantia.PercentualComissao),
                    dados_garantia_vl_adicional = BaseMapper.DecimalToString(entity.DadosGarantia.ValorAdicionalFracionamento),
                    dados_garantia_vl_is = BaseMapper.DecimalToString(entity.DadosGarantia.ValorImportanciaSegurada),
                    dados_garantia_vl_taxa_risco = BaseMapper.DecimalToString(entity.DadosGarantia.ValorTaxaRisco),
                    dados_garantia_vl_premio_total = BaseMapper.DecimalToString(entity.DadosGarantia.ValorPremioTotal),
                    dados_garantia_vl_iof = BaseMapper.DecimalToString(entity.DadosGarantia.ValorIOF),
                    objeto_segurado_nm_objeto_segurado = entity.InsuredObject.Contents,
                    dv_calculo_manual = entity.IsPremioInformado ? "true" : "false"

                };

                if (entity.Clausulas != null && entity.Clausulas.Count > 0) {

                    StringBuilder sbClausulas = new StringBuilder();
                    foreach (var item in entity.Clausulas) {
                        if (sbClausulas.Length > 0) {
                            sbClausulas.AppendLine("");
                        }
                        sbClausulas.AppendLine(item.Title);
                        sbClausulas.AppendLine(item.Text);
                    }

                    mappedObj.texto_trabalhista_anexo = sbClausulas.ToString();
                }

                if (entity.TipoSeguro == TipoSeguroEnum.Recursal) {
                    mappedObj.dados_garantia_vl_is = BaseMapper.DecimalToString(entity.DadosGarantia.ValorImportanciaSeguradaRecursal);
                }

                return mappedObj;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Proposta_Gravar).", e);
            }
        }
        public static ProposalReturnGravarEntity MapPropostaGravarResponseToEntity(PropostaGravarResponse response, ProposalEntity request) {
            try {
                var mappedObj = new ProposalReturnGravarEntity()
                {
                    Proposta = MapPropostaPesquisarItemToEntity(response.Proposta),
                    Success = response.cd_retorno == 0,
                    ReturnCode = response.cd_retorno,
                    Message = response.nm_retorno
                };
                mappedObj.Proposta.DadosGarantia.NumeroLicitacao = request.DadosGarantia.NumeroLicitacao;
                mappedObj.Proposta.InsuredObject.ExternalCode = request.InsuredObject.ExternalCode;
                mappedObj.Proposta.TipoSeguro = request.TipoSeguro;
                return mappedObj;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Proposta_Gravar).", e);
            }
        }
        public static PropostaRetornoVerificarAprovacaoEntity MapPropostaVerificarAprovacaoResponseToEntity(PropostaVerificarAprovacaoResponse response) {
            try {
                var mappedObj = new PropostaRetornoVerificarAprovacaoEntity()
                {
                    Success = response.cd_retorno == 0,
                    ReturnCode = response.cd_retorno,
                    Message = response.nm_retorno
                };
                if (!string.IsNullOrWhiteSpace(response.cd_status)) {
                    mappedObj.StatusProposta = (StatusPropostaEnum)int.Parse(response.cd_status);
                }
                return mappedObj;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Proposta_VerificarAprovacao).", e);
            }
        }
        public static ProposalReturnAprovarEntity MapPropostaAprovarResponseToEntity(PropostaVerificarAprovacaoResponse response) {
            try {
                var mappedObj = new ProposalReturnAprovarEntity()
                {
                    StatusProposta = (StatusPropostaEnum)response.cd_status.ToInt()
                };
                return mappedObj;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Proposta_Aprovar).", e);
            }
        }
        public static PropostaPesquisarRequest MapPropostaBuscarEntityToRequest(PropostaFiltrosEntity entity, int idUsuarioCorretor, string susepCorretor) {
            try {

                int? statusProposta = null;
                if (entity.StatusProposta != null)
                    statusProposta = (int)entity.StatusProposta;

                var request = new PropostaPesquisarRequest()
                {
                    id_apolice = entity.IdApolice,
                    cd_proposta = entity.CodigoProposta,
                    cd_susep = susepCorretor, //entity.CodigoSusepUsuario,
                    id_pessoa_tomador = entity.IdTomador,
                    id_pessoa_cliente = entity.IdSegurado,
                    cd_produto = entity.CodigoProduto,
                    cd_status = statusProposta,
                    id_usuario = idUsuarioCorretor, //entity.IdUsuario,
                    data_inicial = entity.DataInicial,   //TODO: não tem no serviço...
                    data_final = entity.DataFinal      //TODO: não tem no serviço...
                };

                // Se receber dados específicos de corretor faz a troca
                if (entity.Corretor.IdUsuarioCorretor != null) {
                    request.id_usuario = entity.Corretor.IdUsuarioCorretor;
                    request.cd_susep = entity.Corretor.CodigoSusep;
                }

                return request;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Proposta_Buscar).", e);
            }
        }
        public static ProposalEntity MapPropostaObterResponseToEntity(PropostaPesquisarResponse response) {
            try {
                var mappedItem = new ProposalEntity();
                foreach (var item in response.Proposta_Pesquisar) {
                    mappedItem = MapPropostaPesquisarItemToEntity(item);
                    break;  
                }
                return mappedItem;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Proposta_Buscar).", e);
            }
        }
        private static ProposalEntity MapPropostaPesquisarItemToEntity(PropostaPesquisarItem item) {
            ProposalEntity entity = new ProposalEntity()
            {
                IdApolice = BaseMapper.StringToInt(item.id_apolice, 0).Value,
                IdEndosso = BaseMapper.StringToInt(item.id_endosso, 0).Value,
                CodigoApolice = BaseMapper.StringToLong(item.cd_apolice),
                DataProposta = BaseMapper.StringToDate(item.dt_proposta, new DateTime(1753, 1, 1)).Value,
                DataValidade = BaseMapper.StringToDate(item.dt_validade, new DateTime(1753, 1, 1)).Value,
                DataInicioVigencia = BaseMapper.StringToDate(item.dt_inicio_vigencia),
                DataFimVigencia = BaseMapper.StringToDate(item.dt_fim_vigencia),
                DiasPrazoVigencia = (BaseMapper.StringToDate(item.dt_fim_vigencia, DateTime.MinValue).Value - BaseMapper.StringToDate(item.dt_inicio_vigencia, DateTime.MinValue).Value).Days,
                StatusProposta = (StatusPropostaEnum)BaseMapper.StringToInt(item.cd_status),
                CodigoStatus = BaseMapper.StringToInt(item.cd_status).Value,
                DescricaoStatus = EnumExtension.ParseEnum<StatusPropostaEnum>(item.cd_status).ToString(),
                CodigoProposta = BaseMapper.StringToInt(item.cd_proposta, 0).Value,
                Product = new ProductEntity
                {
                    CodigoExterno = BaseMapper.StringToInt(item.cd_produto),
                    NomeProduto = item.nm_produto
                },
                Broker = new CorretorConsultaEntity
                {
                    IdPessoa = BaseMapper.StringToInt(item.id_pessoa_corretor, 0).Value,
                    NomePessoa = item.nm_pessoa_corretor
                },
                Taker = new TakerModel
                {
                    IdPessoa = BaseMapper.StringToInt(item.id_pessoa_tomador),
                    NomePessoa = item.nm_pessoa_tomador,
                    CpfCnpj = BaseMapper.StringToLong(item.nr_cnpj_cpf_tomador),
                    TipoPessoa = (TipoPessoaEnum)BaseMapper.StringToInt(item.cd_tp_pessoa_tomador),
                    DescricaoTipoPessoa = item.nm_tp_pessoa_tomador,
                    Endereco = new EnderecoEntity
                    {
                        IdEndereco = BaseMapper.StringToInt(item.id_endereco_tomador),
                        TipoEndereco = new TipoEnderecoEntity
                        {
                            IdTipoEndereco = BaseMapper.StringToInt(item.id_tp_endereco_tomador),
                            NomeTipoEndereco = item.nm_tp_endereco_tomador
                        },
                        Logradouro = item.nm_logradouro_tomador,
                        Numero = item.nr_rua_endereco_tomador,
                        Complemento = item.nm_complemento_tomador,
                        Bairro = item.nm_bairro_tomador,
                        Cidade = item.nm_cidade_tomador,
                        IdUf = BaseMapper.StringToInt(item.cd_uf_tomador),
                        UF = item.nm_uf_tomador,
                        Cep = item.nm_cep_tomador
                    },
                },
                Insured = new InsuredEntity
                {
                    IdPessoa = BaseMapper.StringToInt(item.id_pessoa_segurado),
                    Nome = item.nm_pessoa_segurado,
                    TipoPessoa = (TipoPessoaEnum)BaseMapper.StringToInt(item.cd_tp_pessoa_segurado),
                    DescricaoTipoPessoa = item.nm_tp_pessoa_segurado,
                    CpfCnpj = BaseMapper.StringToLong(item.nr_cnpj_cpf_segurado),
                    Endereco = new EnderecoEntity
                    {
                        IdEndereco = BaseMapper.StringToInt(item.id_endereco_segurado),
                        TipoEndereco = new TipoEnderecoEntity
                        {
                            IdTipoEndereco = BaseMapper.StringToInt(item.id_tp_endereco_segurado),
                            NomeTipoEndereco = item.nm_tp_endereco_segurado
                        },
                        Logradouro = item.nm_logradouro_segurado,
                        Numero = item.nr_rua_endereco_segurado,
                        Complemento = item.nm_complemento_segurado,
                        Bairro = item.nm_bairro_segurado,
                        Cidade = item.nm_cidade_segurado,
                        IdUf = BaseMapper.StringToInt(item.cd_uf_segurado),
                        UF = item.nm_uf_segurado,
                        Cep = item.nm_cep_segurado
                    },
                },
                DadosCobranca = new DadosCobrancaEntity
                {
                    DataVencimentoPrimeiraParcela = BaseMapper.StringToDate(item.dados_cobranca_dt_vencimento_pparcela),
                    DiaCobranca = BaseMapper.StringToInt(item.dados_cobranca_nr_dia_cobranca),
                    FormaPagamentoPrimeiraParcela = new FormaPagamentoEntity
                    {
                        CodigoFormaPagamento = BaseMapper.StringToInt(item.dados_cobranca_cd_forma_pagamento_pparcela),
                        NomeFormaPagamento = item.dados_cobranca_nm_forma_pagamento_pparcela
                    },
                    FormaPagamentoDemaisParcelas = new FormaPagamentoEntity
                    {
                        CodigoFormaPagamento = BaseMapper.StringToInt(item.dados_cobranca_cd_forma_pagamento_demais_parc),
                        NomeFormaPagamento = item.dados_cobranca_nm_forma_pagamento_demais_parcela
                    },
                    Parcelamento = new ParcelamentoEntity
                    {
                        IdParcelamento = BaseMapper.StringToInt(item.dados_cobranca_id_produto_parc_premio),
                        DescricaoParcelamento = item.dados_cobranca_nm_parcelamento,
                        QuantidadeParcelas = item.ParcelaProposta.ParcelaProposta.Count
                    },
                    PeridiocidadePagamento = new PeridiocidadePagamentoEntity
                    {
                        IdPeridiocidadePagamento = BaseMapper.StringToInt(item.dados_cobranca_id_periodo_pagamento),
                        NomePeridiocidadePagamento = item.dados_cobranca_nm_periodo,
                    }
                },
                DadosGarantia = new DadosGarantiaEntity
                {
                    Coverage = new CoverageEntity
                    {
                        ExternalCode = BaseMapper.StringToInt(item.dados_garantia_id_produto_cobertura),
                        IdCobertura = BaseMapper.StringToInt(item.dados_garantia_id_produto_cobertura),
                        NomeCobertura = item.dados_garantia_nm_cobertura
                    },
                    ValorPremioTarifario = BaseMapper.StringToDecimal(item.dados_garantia_vl_premio_tarifario),
                    PercentualComissao = BaseMapper.StringToDecimal(item.dados_garantia_Pe_comissao),
                    ValorTaxaMoeda = BaseMapper.StringToDecimal(item.dados_garantia_vl_taxa_moeda),
                    ValorAdicionalFracionamento = BaseMapper.StringToDecimal(item.dados_garantia_vl_adicional),
                    ValorComissao = BaseMapper.StringToDecimal(item.dados_garantia_Vl_comissao),
                    ValorImportanciaSegurada = BaseMapper.StringToDecimal(item.dados_garantia_vl_is),
                    ValorImportanciaSeguradaRecursal = BaseMapper.StringToDecimal(item.dados_garantia_vl_is),
                    ValorTaxaRisco = BaseMapper.StringToDecimal(item.dados_garantia_vl_taxa_risco),
                    CodigoMoeda = BaseMapper.StringToInt(item.dados_garantia_cd_moeda),
                    ValorIOF = BaseMapper.StringToDecimal(item.dados_garantia_vl_iof, 0),
                    PercentualIOF = BaseMapper.StringToDecimal(item.dados_garantia_pe_iof),
                    ValorPremioTotal = item.ParcelaProposta.ParcelaProposta.Sum(x => BaseMapper.StringToDecimal(x.vl_premio_total, 0).Value) //TODO: Na BTG prêmio total se obtém assim...
                },

                InsuredObject = new InsuredObjectEntity
                {
                    InsuredObjectId = null,     //TODO: O dia que a i4pro retornar, deve obter corretamente aqui, por enquanto busca em DB
                    Name = null,   //TODO: O dia que a i4pro retornar, deve obter corretamente aqui, por enquanto busca em DB
                    Contents = item.objeto_segurado_nm_objeto_segurado
                },

                UsuarioInclusao = new UsuarioEntity
                {
                    CodigoUsuario = BaseMapper.StringToInt(item.cd_usuario_inclusao, 0).Value,
                    NomeUsuario = item.nm_usuario_inclusao_login
                },
                UsuarioAlteracao = new UsuarioEntity
                {
                    CodigoUsuario = BaseMapper.StringToInt(item.cd_usuario_altracao, 0).Value,
                    NomeUsuario = item.nm_usuario_alteracao_login
                },
                Parcelas = MapParcelaResponseToEntity(item.ParcelaProposta)
            };
            return entity;
        }
        private static List<ParcelaEntity> MapParcelaResponseToEntity(ParcelaPropostaItem response) {
            List<ParcelaEntity> mappedList = new List<ParcelaEntity>();
            foreach (var item in response.ParcelaProposta) {
                ParcelaEntity entity = new ParcelaEntity
                {
                    NumeroParcela = BaseMapper.StringToInt(item.nr_parcela, 0).Value,
                    ValorAdicionalFracionamento = BaseMapper.StringToDecimal(item.vl_adicional, 0).Value,
                    DataVencimento = BaseMapper.StringToDate(item.dt_vencimento).Value,
                    ValorIof = BaseMapper.StringToDecimal(item.vl_iof, 0).Value,
                    ValorPremioTarifario = BaseMapper.StringToDecimal(item.vl_premio_tarifario, 0).Value,
                    ValorPremioTotal = BaseMapper.StringToDecimal(item.vl_premio_total, 0).Value,
                    ValorCusto = BaseMapper.StringToDecimal(item.vl_custo, 0).Value,
                    StatusParcela = EnumExtension.ParseEnum<StatusParcelaEnum>(item.dv_situacao)
                };
                mappedList.Add(entity);
            }
            return mappedList;
        }
        public static PropostaRetornoImprimirEntity MapPropostaImprimirResponseToEntity(PropostaImprimirMinutaResponse response) {
            try {
                var mappedObj = new PropostaRetornoImprimirEntity()
                {
                    NomeArquivo = "PropostaMinuta.pdf",
                    Base64 = response.Proposta_ImprimirMinuta
                };
                return mappedObj;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Proposta_ImprimirMinuta).", e);
            }
        }
    }
}
