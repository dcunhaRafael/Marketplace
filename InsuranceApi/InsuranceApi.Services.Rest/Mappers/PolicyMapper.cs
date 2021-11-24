using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class PolicyMapper {
        
        public static ApolicePesquisarRequest MapApolicePesquisarEntityToRequest(PolicySearchEntity entity) {
            try {

                int? statusApolice = null;
                if (entity.StatusApolice != null)
                    statusApolice = (int)entity.StatusApolice;


                ApolicePesquisarRequest request = new ApolicePesquisarRequest
                {
                    id_apolice = null,
                    nr_apolice = entity.NumeroApolice.ToNullableDecimal(),
                    id_pessoa_cliente = entity.IdSegurado,
                    dt_inicio = entity.DataInicial?.ToString("yyyy-MM-dd"),
                    dt_fim = entity.DataFinal?.ToString("yyyy-MM-dd"),
                    cd_produto = entity.CodigoProduto,
                    id_pessoa_tomador = entity.IdTomador,
                    cd_proposta = entity.NumeroProposta.ToNullableDecimal(),
                    cd_susep = entity.CodigoSusepUsuario,
                    id_usuario = entity.IdUsuario.Value, 
                    cd_status = statusApolice

                };             

                return request;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Apolice_Pesquisar).", e);
            }
        }        
        public static List<PolicyReturnEntity> MapApolicePesquisarResponseToEntity(ApolicePesquisarResponse response) {
            try {

                var mappedList = new List<PolicyReturnEntity>();
                foreach (var item in response.Apolice_Pesquisar) {
                    mappedList.Add(new PolicyReturnEntity()
                    {
                        NumeroApolice = item.cd_apolice.ToString(),
                        CodigoApolice = item.id_apolice,
                        NumeroProposta = item.cd_proposta.ToString(),
                        NomeSegurado = item.nm_pessoa_segurado,
                        NomeTomador = item.nm_pessoa_tomador,
                        NomeCorretor = item.nm_pessoa_corretor,
                        DescricaoProduto = item.nm_produto,
                        Data = item.dt_proposta,
                        StatusApolice = (StatusApoliceEnum)item.cd_status,
                        id_endosso = item.id_endosso ?? 0,
                        IdUsuarioInclusao = item.cd_usuario_inclusao,

                        DataInicioVigencia = item.dt_inicio_vigencia,
                        DataFinalVigencia = item.dt_fim_vigencia,
                        DescricaoModalidade = item.dados_garantia_nm_cobertura,
                        CnpjTomador = item.Nr_cnpj_cpf_tomador ?? 0,
                        ValorIS = item.dados_garantia_vl_is,
                        ValorPremio = item.dados_garantia_vl_premio_tarifario

                    });
                }
                return mappedList;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Apolice_Pesquisar).", e);
            }
        }        
        public static BilletPrintEntity MapApoliceImprimirBoletoResponseToEntity(ApoliceImprimirBoletoResponse response) {
            try {

                var mapped = new BilletPrintEntity()
                {
                    Base64 = response.Apolice_ImprimirBoleto                  
                };
                return mapped;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Apolice_ImprimirBoleto).", e);
            }
        }        
        public static ApoliceEmitirRequest MapApoliceEmitirEntityToRequest(ApoliceAssinadaEmitirEntity entity, int idUsuarioCorretor) {
            try {
                var request = new ApoliceEmitirRequest
                {
                    id_apolice = BaseMapper.IntToString(entity.IdApolice),
                    id_usuario = idUsuarioCorretor.ToString(), //BaseMapper.DefaultUserId.ToString(), //BaseMapper.IntToString(entity.IdUsuario),
                    id_endosso = BaseMapper.IntToString(entity.IdEndosso),
                    dv_assinatura_proposta = BaseMapper.BooleanToString(entity.IndicadorPropostaAssinada),
                    dt_assinatura_proposta = BaseMapper.DateToString(entity.DataAssinaturaProposta),
                    nm_observacao_assinatura = entity.ObservacoesAssinatura
                };
                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Apolice_Emitir).", e);
            }
        }        
        public static ApoliceRetornoEmitirEntity MapApoliceEmitirResponseToEntity(ApoliceEmitirResponse response) {
            try {

                var mapped = new ApoliceRetornoEmitirEntity
                {
                    NumeroApolice = BaseMapper.StringToLong(response.Apolice.cd_apolice, 0).Value,
                    StatusProposta = (StatusPropostaEnum)response.Apolice.cd_status
                };
                return mapped;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Apolice_Emitir).", e);
            }
        }
        public static PolicyPrintEntity MapApoliceImprimirResponseToEntity(ApoliceImprimirResponse response) {
            try {

                var mapped = new PolicyPrintEntity()
                {
                    Base64 = response.Apolice_Imprimir
                };
                return mapped;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Apolice_ImprimirBoleto).", e);
            }
        }
    }
}
