using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Services.Rest.Messages.Legacy;
using InsuranceApi.Domain.Common.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class InsuredMapper {
        
        public static SeguradoPesquisarRequest MapSeguradoBuscarEntityToRequest(SeguradoBuscarEntity entity) {
            try {
                SeguradoPesquisarRequest request = new SeguradoPesquisarRequest {
                    id_pessoa = entity.IdPessoa,
                    nr_cnpj_cpf = entity.CpfCnpj,
                    nm_pessoa = entity.NomePessoa,
                    dv_ativo = entity.Ativo,
                    dv_endereco_padrao = entity.EnderecoPadrao
                };
                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Segurado_Pesquisar).", e);
            }
        }        
        public static List<InsuredEntity> MapSeguradoBuscarResponseToEntity(SeguradoPesquisarResponse response) {
            try {
                var mappedList = new List<InsuredEntity>();
                foreach (var item in response.Segurado_Pesquisar) {
                    mappedList.Add(new InsuredEntity() {
                        IdPessoa = int.Parse(item.id_pessoa),
                        Nome = item.nm_pessoa,
                        CpfCnpj = BaseMapper.StringToLong(item.nr_cnpj_cpf),
                        TipoPessoa = (TipoPessoaEnum)int.Parse(item.cd_tp_pessoa),
                        DescricaoTipoPessoa = item.nm_tipo_pessoa,

                        Endereco = new EnderecoEntity {
                            IdEndereco = BaseMapper.StringToInt(item.id_endereco),
                            TipoEndereco = new TipoEnderecoEntity {
                                IdTipoEndereco = BaseMapper.StringToInt(item.id_tp_endereco),
                                NomeTipoEndereco = item.nm_tp_endereco
                            },
                            Logradouro = item.nm_logradouro,
                            Numero = item.nr_rua_endereco,
                            Complemento = item.nm_complemento,
                            Bairro = item.nm_bairro,
                            Cidade = item.nm_cidade,
                            Cep = item.nm_cep,
                            UF = item.nm_uf
                        },
                        Contato = new ContatoEntity {
                            Nome = item.Contato?.Contato?.FirstOrDefault()?.nome,
                            Cpf = BaseMapper.StringToLong(item.Contato?.Contato?.FirstOrDefault()?.cpf_cnpj),
                            Email = item.Contato?.Contato?.FirstOrDefault(x => x.meio_comunicacao.Equals("4"))?.valor_meio_comunicacao
                        }
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Segurado_Pesquisar).", e);
            }
        }

        public static SeguradoIncluirRequest MapSeguradoIncluirEntityToRequest(InsuredEntity entity) {
            try {
                var request = new SeguradoIncluirRequest {
                    nm_pessoa = entity.Nome,
                    cd_tipo_pessoa = (int)entity.TipoPessoa,
                    nr_cnpj_cpf = entity.CpfCnpj?.FormatLongToCpfCnpj().ApenasNumericos(),
                };
                if (entity.Endereco != null) {
                    request.SeguradoInserir_Endereco = new EnderecoItem {
                        id_tp_endereco = entity.Endereco.TipoEndereco.IdTipoEndereco.Value,
                        nm_endereco = entity.Endereco.Logradouro,
                        nr_rua_endereco = entity.Endereco.Numero,
                        nm_complemento = entity.Endereco.Complemento,
                        nm_bairro = entity.Endereco.Bairro,
                        nm_cidade = entity.Endereco.Cidade,
                        cd_uf = entity.Endereco.IdUf.Value,
                        nm_cep = entity.Endereco.Cep,
                        id_local = entity.Endereco.IdCidade,
                        dv_endereco_padrao = true
                    };
                }
                //if (entity.Contato != null) {
                //    request.Contato = new ContatoItem {
                //        nome = entity.Contato.Nome,
                //        cpf_cnpj = entity.Contato.CpfCnpj?.FormatLongToCpfCnpj().ApenasNumericos(),
                //        meio_comunicacao = "4",  //-- Fixo: E-MAIL
                //        valor_meio_comunicacao = entity.Contato.Email
                //    };
                //}

                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Segurado_Incluir).", e);
            }
        }
        public static SeguradoRetornoIncluirEntity MapSeguradoIncluirResponseToEntity(SeguradoIncluirResponse response) {
            try {
                var mapped = new SeguradoRetornoIncluirEntity {
                    Codigo = response.cd_retorno,
                    Descricao = response.nm_retorno,
                    IdPessoa = int.Parse(response.id_pessoa)
                };
                if (response.SeguradoInserir_Endereco != null) {
                    mapped.IdEndereco = int.Parse(response.SeguradoInserir_Endereco.id_endereco);
                }
                return mapped;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Segurado_Incluir).", e);
            }
        }

    }
}
