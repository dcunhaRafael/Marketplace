//using Domain.Entities;
using Domain.Payload;
using Domain.Util.Extensions;
using Integration.BMG.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Integration.BMG.Mappers {
    public static class TakerMapper {

        public static IList<TakerData> Map(TomadorBuscarResponse response) {
            try {
                var mappedList = new List<TakerData>();
                if (response.Tomador_Buscar != null) {
                    foreach (var item in response.Tomador_Buscar) {
                        var mappedObject = new TakerData() {
                            LegacyCode = item.id_pessoa,
                            AddressLegacyCode = item.id_endereco,
                            Person = new Person {
                                Name = item.nm_pessoa,
                                CpfCnpjNumber = item.nr_cnpj_cpf.ToLong(),
                                PersonType = new FixedDomain {
                                    Name = item.nm_tipo_pessoa,
                                    LegacyCode = item.cd_tp_pessoa,
                                },
                                Addresses = new List<PersonAddress>(){
                                    new PersonAddress {
                                        IsMainAddress = true,
                                        AddressType = new AddressType {
                                            Id = item.id_tp_endereco?.ToInt(),
                                            Name = item.nm_tp_endereco,
                                        },
                                        StreetName = item.nm_logradouro,
                                        Number = item.nr_rua_endereco,
                                        Complement = item.nm_complemento,
                                        District = item.nm_bairro,
                                        City = new City {
                                            Name = item.nm_cidade,
                                            Id = item.id_local?.ToInt(),
                                            State = new State {
                                                Name = "", //TODO
                                                Initials = item.nm_uf,
                                                Id = item.cd_uf?.ToInt(),
                                            }
                                        },
                                        ZipCode = item.nm_cep.ToInt(),
                                    }
                                },
                            },
                            TotalInsuredAmountValue = item.vl_is_total.ToDecimal(),
                            AvailableBalanceValue = item.vl_saldo_disponivel.ToDecimal(),
                            RegistrationExpirationDate = item.dt_validade_cadastro_ress.ToDate(),
                            IsActive = item.dv_ativo,
                            IsCcgSigned = "OK".Equals(item.nm_status_ccg, StringComparison.CurrentCultureIgnoreCase)
                        };

                        mappedObject.CreditAssessments = new List<TakerCreditAssessment>() {
                            new TakerCreditAssessment {
                                CreditLimitAmount = item.vl_lim_credito.ToDecimal(),
                                RiskRate = item.vl_taxa.ToDecimal(),
                                Rating = item.cd_classe_risco,
                                DueDate = item.dt_validade_cadastro_ress.ToDate(),
                            }
                        };

                        var broker = item.Corretor_Tomador.FirstOrDefault();
                        if (broker != null) {
                            mappedObject.Broker = new Broker {
                                //Id = 0,
                                LegacyCode = broker.id_pessoa,
                                //AddressLegacyCode = broker.id_endereco,
                                LegacyUserId = broker.id_usuario_corretor.ToInt(),
                                SusepCode = broker.cd_susep_corretor,
                                //Person = new Person {
                                //    Id = 0,
                                //    Name = broker.nm_pessoa,
                                //    CpfCnpjNumber = broker.nr_cnpj_cpf.ToLong(),
                                //}
                            };
                        }

                        mappedList.Add(mappedObject);
                    }
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

        public static TakerCreditLimit Map(TomadorBuscarLimitesCreditoResponse response) {
            try {
                TakerCreditLimit mappedObject = null;
                if (response.Tomador_BuscarLimitesCredito.FirstOrDefault() != null) {
                    var parameters = response.Tomador_BuscarLimitesCredito.FirstOrDefault();
                    mappedObject = new TakerCreditLimit {
                        Name = parameters.nm_pessoa,
                        CpfCnpjNumber = parameters?.nr_cnpj_cpf.ToLong(),
                        LegacyCode = parameters.id_pessoa,
                        AvailableCreditLimit = parameters.vl_lim_credito.ToDecimal(),
                        AvailableCreditLimitReinsurance = parameters.vl_disponivel_credito_ress.ToDecimal(),
                        ValidityDate = parameters.dt_validade_tomador?.ToDate(),
                        ComplianceValidityDate = parameters.dt_validade_compliance?.ToDate(),
                        Sublimits = new List<TakerCreditSubLimit>(),
                        TakersParentsGroup = new List<TakerCreditSubLimitParentGroup>()
                    };

                    foreach (var sl in parameters.Sublimites) {
                        var subLimit = new TakerCreditSubLimit() {
                            CoverageGroupName = sl.nm_grupo_coberturas,
                            GroupId = sl.id_grp_sub_limite,
                            TakerGroupId = sl.id_grp_sub_limite_tomador,
                            SubLimitValue = sl.vl_sublimite_cedito,
                            AvailableSubLimitValue = sl.vl_sublimite_credito_disponivel,
                            Coverages = new List<TakerCreditSubLimitCoverage>()
                        };
                        foreach (var coverage in sl.Coberturas) {
                            subLimit.Coverages.Add(new TakerCreditSubLimitCoverage() {
                                SubLimitCoverageGroupId = coverage.id_grp_sub_limite_cobertura,
                                Product = new Product() {
                                    ProductId = coverage.cd_produto,
                                    Name = coverage.nm_produto
                                },
                                Coverage = new Coverage() {
                                    CoverageId = coverage.id_produto_cobertura,
                                    Name = coverage.nm_comercial
                                }
                            });
                        }
                        mappedObject.Sublimits.Add(subLimit);
                    }

                    foreach (var parent in parameters.TomadoresGrupoPais) {
                        mappedObject.TakersParentsGroup.Add(new TakerCreditSubLimitParentGroup() {
                            Name = parent.nm_pessoa_pai,
                            CpfCnpjNumber = parent.nr_cnpj_cpf_pai,
                            LegacyCode = parent.id_pessoa_pai,
                            ParticipationPercentage = parent.pe_participacao,
                            GenerateAccumulation = parent.dv_gera_acumulo,
                            AvailableCreditLimit = parent.vl_lim_credito_pai,
                            AvailableCreditLimitReinsurance = parent.vl_disponivel_credito_ress_pai,
                            ValidityDate = parent.dt_validade_tomador_pai?.ToDate(),
                            ComplianceValidityDate = parent.dt_validade_compliance_pai?.ToDate(),
                        });
                    }
                }
                return mappedObject;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

        public static IList<RiskRating> Map(ClasseRiscoResponse response) {
            try {
                var mappedList = new List<RiskRating>();
                if (response.Classe_Risco != null) {
                    foreach (var item in response.Classe_Risco) {
                        var mappedItem = new RiskRating() {
                            RiskRatingCode = item.cd_classe_risco,
                            Name = item.nm_classe_risco
                        };
                        mappedList.Add(mappedItem);
                    }
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }
    }
}