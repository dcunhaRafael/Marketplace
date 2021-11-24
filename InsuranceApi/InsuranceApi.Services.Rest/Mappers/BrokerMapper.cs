using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class BrokerMapper {

        public static BrokerEntity MapCorretorBuscarResponseToEntity(CorretorBuscarResponse response) {
            try {
                
                var mappedList = new List<BrokerEntity>();
                if(response.Corretor_Buscar.Count > 0) {
                    foreach (var item in response.Corretor_Buscar) {
                        mappedList.Add(new BrokerEntity()
                        {
                            IdCorretor = int.Parse(item.id_pessoa),
                            NomeCorretor = item.nm_pessoa,
                            CpfCnpj = long.Parse(item.nr_cnpj_cpf),
                            TipoPessoa = int.Parse(item.cd_tipo_pessoa),
                            DescricaoTipoPessoa = item.nm_tipo_pessoa,
                            Endereco = new EnderecoEntity()
                            {
                                IdEndereco = BaseMapper.StringToInt(item.id_endereco),
                                TipoEndereco = new TipoEnderecoEntity()
                                {
                                    IdTipoEndereco = BaseMapper.StringToInt(item.id_tp_endereco),
                                    NomeTipoEndereco = item.nm_tp_endereco,
                                },
                                Logradouro = item.nm_logradouro,
                                Numero = item.nr_rua_endereco,
                                Complemento = item.nm_complemento,
                                Bairro = item.nm_bairro,
                                Cidade = item.nm_cidade,
                                Cep = item.nm_cep,
                                IdUf = BaseMapper.StringToInt(item.cd_uf),
                                UF = item.nm_uf
                            }
                        });
                    }
                }
                return mappedList?.FirstOrDefault();
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Corretor_Buscar).", e);
            }
        }


        public static CorretorConsultaRequest MapCorretorConsultaEntityToRequest(CorretorConsultaEntity entity) {
            try {
                CorretorConsultaRequest request = new CorretorConsultaRequest() {
                    nm_pessoa = entity.NomePessoa,
                    nr_cnpj_cpf = entity.CpfCnpj.ToNullableDecimal(),
                    cd_susep_corretor = entity.CodigoSusep,
                    dv_ativo = entity.IsAtivo,
                    id_usuario = AppConfigHttp.DefaultUserId,
                    cd_susep = AppConfigHttp.DefaultSusepCode,
                };

                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Corretor_Consulta).", e);
            }
        }

        public static List<CorretorConsultaEntity> MapCorretorConsultaEntityToRequest(CorretorConsultaResponse response) {
            try {
                var mappedList = new List<CorretorConsultaEntity>();
                foreach (var item in response.Consulta_Corretor) {
                    mappedList.Add(new CorretorConsultaEntity() {
                        IdPessoa = BaseMapper.StringToInt(item.id_pessoa_corretor).Value,
                        TipoPessoa = BaseMapper.StringToInt(item.cd_tp_pessoa).Value,
                        NomePessoa = item.nm_pessoa,
                        CpfCnpj = item.nr_cnpj_cpf,
                        CodigoSusep = item.cd_susep_corretor,
                        IdUsuarioCorretor = BaseMapper.StringToInt(item.id_usuario_corretor),
                        IsAtivo = item.dv_ativo,
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Corretor_Consulta).", e);
            }
        }
    }
}
