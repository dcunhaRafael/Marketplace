using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class TakerMapper {

        public static TomadorBuscarRequest MapTomadorBuscarEntityToRequest(TomadorBuscarEntity entity, int? idUsuarioCorretor) {
            try {
                TomadorBuscarRequest request = new TomadorBuscarRequest
                {
                    id_pessoa_tomador = entity.IdPessoaTomador,
                    dv_ativo = entity.Ativo,
                    dv_endereco_padrao = entity.EnderecoPadrao,
                    dv_situacao = entity.Situacao,
                    dv_ccg_formalizado = entity.CcgFormalizado,
                    dv_dominio_tomador = entity.DominioTomador,
                    dv_dominio_corretor = entity.DominioCorretor,
                    dv_lista_corretor = entity.ListaCorretor,
                    nm_pessoa = entity.NomePessoa,
                    nr_cnpj_cpf = entity.CpfCnpj,
                    // id_usuario = BaseMapper.DefaultUserId, //entity.IdUsuario
                    dv_lista_tomador = entity.ListarTodos
                };

                // Se receber dados específicos de corretor faz a troca
                if (idUsuarioCorretor != null) {
                    request.id_usuario = idUsuarioCorretor;
                }

                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Tomador_Buscar).", e);
            }
        }
        public static List<TakerModel> MapTomadorBuscarResponseToEntity(TomadorBuscarResponse response) {
            try {
                var mappedList = new List<TakerModel>();
                if (response.Tomador_Buscar != null) {
                    foreach (var item in response.Tomador_Buscar) {
                        mappedList.Add(new TakerModel()
                        {
                            IdPessoa = BaseMapper.StringToInt(item.id_pessoa),
                            NomePessoa = item.nm_pessoa,
                            CpfCnpj = BaseMapper.StringToLong(item.nr_cnpj_cpf),
                            TipoPessoa = (TipoPessoaEnum)BaseMapper.StringToInt(item.cd_tp_pessoa),
                            DescricaoTipoPessoa = item.nm_tipo_pessoa,
                            ClasseRisco = item.cd_classe_risco,
                            Endereco = new EnderecoEntity
                            {
                                IdEndereco = BaseMapper.StringToInt(item.id_endereco),
                                TipoEndereco = new TipoEnderecoEntity
                                {
                                    IdTipoEndereco = BaseMapper.StringToInt(item.id_tp_endereco),
                                    NomeTipoEndereco = item.nm_tp_endereco
                                },
                                Logradouro = item.nm_logradouro,
                                Numero = item.nr_rua_endereco,
                                Complemento = item.nm_complemento,
                                Bairro = item.nm_bairro,
                                Cidade = item.nm_cidade,
                                Cep = item.nm_cep,
                                IdUf = BaseMapper.StringToInt(item.cd_uf),
                                UF = item.nm_uf,
                            },
                            NomeCorretor = item.Corretor_Tomador.FirstOrDefault()?.nm_pessoa,
                            CpfCnpjCorretor = item.Corretor_Tomador.FirstOrDefault()?.nr_cnpj_cpf,
                            IdUsuarioCorretor = BaseMapper.StringToInt(item.Corretor_Tomador.FirstOrDefault()?.id_usuario_corretor),
                            CodigoSusepCorretor = item.Corretor_Tomador.FirstOrDefault()?.cd_susep_corretor,
                            Limite = BaseMapper.StringToDecimal(item.vl_lim_credito),
                            Taxa = BaseMapper.StringToDecimal(item.vl_taxa),
                            LimiteDisponivel = BaseMapper.StringToDecimal(item.vl_saldo_disponivel)
                        });
                    }
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Tomador_Buscar).", e);
            }
        }
        public static TakerCalculationParameters MapTomadorBuscarParametrosCalculoResponseToEntity(TomadorBuscarParametrosCalculoResponse response) {
            try {
                var mappedList = new List<TakerCalculationParameters>();
                foreach (var item in response.Tomador_BuscarParametrosCalculo) {
                    mappedList.Add(new TakerCalculationParameters()
                    {
                        ValorPremioMinimo = BaseMapper.StringToDecimal(item.vl_premio_minimo, 0).Value,
                        ValorTaxaRisco = BaseMapper.StringToDecimal(item.vl_taxa_risco, 0).Value,
                        PercentualComissao = BaseMapper.StringToDecimal(item.pe_comissao, 0).Value
                    });
                }
                return mappedList.FirstOrDefault();
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Tomador_BuscarParametrosCalculo).", e);
            }
        }
        public static TomadorIncluirRequest MapTomadorIncluirEntityToRequest(TakerModel tomador, int? idUsuarioCorretor) {
            try {
                var request = new TomadorIncluirRequest
                {
                    nm_pessoa = tomador.NomePessoa,
                    cd_tipo_pessoa = (int)tomador.TipoPessoa,
                    nr_cnpj_cpf = string.Format("{0:D14}", tomador.CpfCnpj),
                    id_usuario = idUsuarioCorretor.ToString(),
                    TomadorInserir_Endereco = new EnderecoItem
                    {
                        id_tp_endereco = tomador.Endereco.TipoEndereco.IdTipoEndereco.Value,
                        nm_endereco = tomador.Endereco.Logradouro,
                        nr_rua_endereco = tomador.Endereco.Numero,
                        nm_complemento = tomador.Endereco.Complemento,
                        nm_bairro = tomador.Endereco.Bairro,
                        id_local = tomador.Endereco.IdCidade,  //TODO
                        nm_cidade = tomador.Endereco.Cidade,
                        cd_uf = tomador.Endereco.IdUf.Value,
                        nm_cep = tomador.Endereco.Cep,
                        dv_endereco_padrao = true
                    },
                    TomadorInserir_Contato = null,
                    vl_lim_credito = BaseMapper.DecimalToString(tomador.Limite),
                    vl_taxa = BaseMapper.DecimalToString(tomador.Taxa),
                    cd_classe_risco = tomador.Rating
                };

                if (tomador.Contato != null) {
                    request.TomadorInserir_Contato = new ContatoItem
                    {
                        nome = tomador.Contato.Nome,
                        cpf_cnpj = string.Format("{0:D14}", tomador.Contato.Cpf),
                        meio_comunicacao = "4",  //-- Fixo: E-MAIL
                        valor_meio_comunicacao = tomador.Contato.Email

                    };
                }

                if (idUsuarioCorretor != null) {
                    request.id_usuario = idUsuarioCorretor.Value.ToString();
                }

                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Tomador_Incluir).", e);
            }
        }
        public static TomadorRetornoIncluirEntity MapTomadorIncluirResponseToEntity(TomadorIncluirResponse response) {
            try {
                var mapped = new TomadorRetornoIncluirEntity
                {
                    Codigo = response.cd_retorno,
                    Descricao = response.nm_retorno,
                    IdPessoa = response.id_pessoa
                };
                return mapped;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Tomador_Incluir).", e);
            }
        }

    }
}
