﻿using Domain.Payload;
using Domain.Util.Extensions;
using Integration.BMG.Schemas;
using System;
using System.Collections.Generic;

namespace Integration.BMG.Mappers {
    public static class BrokerMapper {

        public static List<ComissionStatement> Map(CorretorExtratoComissaoResponse response) {
            try {
                var mappedList = new List<ComissionStatement>();
                if (response.Corretor_Extrato_Comissao_table_1 != null) {
                    foreach (var item in response.Corretor_Extrato_Comissao_table_1) {
                        mappedList.Add(new ComissionStatement() {
                            StatementNumber = item.cd_extrato,

                            Broker = new Broker() {
                                LegacyCode = item.id_pessoa_corretor,
                                SusepCode = item.cd_susep,
                                LegacyUserId = item.cd_usuario_autenticacao,
                                Name = item.nm_corretor,
                            },
                            Competency = item.dt_competencia,
                            EntryCount = item.nr_qtd_lancamentos,
                            OpeningDate = item.dt_abertura?.ToDateTime(),
                            ClosingDate = item.dt_fechamento?.ToDateTime(),
                            ComissionValue = item.vl_comissao,
                            PayDay = item.dt_pagto?.ToDateTime(),
                            StatusName = item.nm_situacao_extrado,
                        });
                    }
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

        public static List<ComissionStatementDetail> Map(CorretorDetalheExtratoComissaoResponse response) {
            try {
                var mappedList = new List<ComissionStatementDetail>();
                foreach (var item in response.Corretor_Detalhe_Extrato_Comissao_table_1) {
                    mappedList.Add(new ComissionStatementDetail() {
                        Broker = new Broker() {
                            LegacyCode = item.id_pessoa_corretor,
                            LegacyUserId = item.ColunaSemNome13,
                            Name = item.nm_corretor,
                        },
                        StatementNumber = item.cd_extrato,
                        Competency = item.dt_competencia,
                        EntryCount = item.nr_qtd_lancamentos,
                        ComissionValue = item.vl_comissao,
                        PayDay = item.dt_pagto?.ToDateTime(),
                        PaymentRequestDate = item.dt_solicitacao_pagamento?.ToDateTime(),
                        StatusName = item.nm_situacao_extrado,
                        NotTaxableComissionValue = item.vl_comissao_nao_tributavel,
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

        public static List<ComissionStatementType> Map(CorretorExtratoTipoComissaoResponse response) {
            try {
                var mappedList = new List<ComissionStatementType>();
                foreach (var item in response.Corretor_Extrato_Tipo_Comissao_table_1) {
                    mappedList.Add(new ComissionStatementType() {
                        Broker = new Broker() {
                            LegacyCode = item.id_pessoa_corretor,
                            LegacyUserId = item.ColunaSemNome4
                        },
                        ComissionTypeId = item.codigoTipoComissao,
                        ComissionTypeName = item.descricaoTipoComissao,
                        ComissionValue = item.valorComissao,
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

        public static List<ComissionStatementBusiness> Map(CorretorExtratoComissaoPorRamoResponse response) {
            try {
                var mappedList = new List<ComissionStatementBusiness>();
                foreach (var item in response.ConsultaExtratoComissaoPorRamo) {
                    mappedList.Add(new ComissionStatementBusiness() {
                        Broker = new Broker() {
                            LegacyCode = item.id_pessoa_corretor,
                            LegacyUserId = item.ColunaSemNome6
                        },
                        BusinessId = item.CodigoRamo,
                        BusinessName = item.DescricaoRamo,
                        ComissionTypeId = item.CodigoTipoComissao,
                        ComissionTypeName = item.DescricaoTipoComissao,
                        ComissionValue = item.ValorComissao,
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

        public static List<ComissionStatementEntry> Map(CorretorLancamentoExtratoComissaoResponse response) {
            try {
                var mappedList = new List<ComissionStatementEntry>();
                foreach (var item in response.Consulta_Lancamento_Extrato_Comissao_table_1) {
                    mappedList.Add(new ComissionStatementEntry() {
                        StatementNumber = item.cd_extrato,
                        Competency = item.dt_competencia,
                        EntryNumber = item.cd_lancamento,
                        ProposalNumber = (int)item.nr_proposta,
                        PolicyNumber = (long)item.nr_apolice,
                        EndorsementNumber = item.nr_endosso,
                        ComissionTypeId = item.cd_tipo_comissao,
                        ComissionTypeName = item.nm_tp_comissao,
                        BusinessId = item.cd_ramo,
                        BusinessName = item.nm_ramo,
                        InstallmentNumber = item.nr_parcela,
                        InsuredName = item.nm_segurado,
                        ComissionPercentage = item.Pe_comissao,
                        TariffPremiumValue = item.vl_premio_base,
                        //ComissionValue = item.vl_comissao
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

    }
}