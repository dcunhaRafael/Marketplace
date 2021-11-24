using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class ProposalAppealApplication : IProposalAppealApplication {
        private readonly IInsuredService insuredService;
        private readonly IZipCodeApplication zipCodeApplication;
        private readonly IProposalApplication proposalApplication;
        private readonly ICoverageApplication coverageApplication;
        private readonly IProductApplication productApplication;
        private readonly ILegalRecourseTypeApplication legalRecourseTypeApplication;
        private readonly ILaborCourtApplication laborCourtApplication;
        private readonly ICivilCourtApplication civilCourtApplication;
        private readonly ITermTypeApplication termTypeApplication;
        private readonly IInsuredApplication insuredApplication;
        private readonly IAppParameterApplication appParameterApplication;

        public ProposalAppealApplication(
             IInsuredService insuredService,
             IInsuredApplication insuredApplication,
             IZipCodeApplication zipCodeApplication,
             IProposalApplication proposalApplication,
             IProductApplication productApplication,
             ILegalRecourseTypeApplication legalRecourseTypeApplication,
             ILaborCourtApplication tribunalApplication,
             ICivilCourtApplication civilCourtApplication,
             ICoverageApplication coverageApplication,
             ITermTypeApplication termTypeApplication,
             IAppParameterApplication appParameterApplication) {
            this.appParameterApplication = appParameterApplication;
            this.proposalApplication = proposalApplication;
            this.civilCourtApplication = civilCourtApplication;
            this.productApplication = productApplication;
            this.legalRecourseTypeApplication = legalRecourseTypeApplication;
            laborCourtApplication = tribunalApplication;
            this.termTypeApplication = termTypeApplication;
            this.coverageApplication = coverageApplication;
            this.insuredService = insuredService;
            this.zipCodeApplication = zipCodeApplication;
            this.insuredApplication = insuredApplication;
        }

        public async Task<ProposalReturnGravarEntity> AddAsync(ProposalAppealEntity entity, int brokerUserIdbrokerUserId) {
            try {

                ProposalEntity proposalEntity = new ProposalEntity() {
                    Product = await productApplication.GetAsync(entity.CodigoProduto)
                };
                if (proposalEntity.Product == null) {
                    throw new BusinessException("Produto não encontrado.");
                }

                proposalEntity.DadosGarantia.Coverage = await coverageApplication.GetAsync(entity.CodigoProduto, entity.CodigoModalidade);
                if (proposalEntity.DadosGarantia.Coverage == null) {
                    throw new BusinessException("Configurações da Modalidade x Produto não localizadas.");
                }

                var parameter = await appParameterApplication.ListAsync();
                var parameterCoverage =
                    parameter.Where(x => x.AppParameterId.Equals((int)AppParameterEnum.ModalidadeTipoSeguroRecursal)
                              || x.AppParameterId.Equals((int)AppParameterEnum.ModalidadeTipoSeguroRecursalSME)).ToList();

                var isCoverageAppeal = parameterCoverage.Where(x => x.Value.Equals(entity.CodigoModalidade)).FirstOrDefault();
                if (isCoverageAppeal == null)
                    throw new BusinessException("Não foi possível encontrar a modalidade 'recursal'.");

                proposalEntity.DadosGarantia.TermType = await termTypeApplication.GetAsync(entity.CodigoPrazoAno, entity.CodigoModalidade.ToInt());
                if (proposalEntity.DadosGarantia.TermType == null) {
                    throw new BusinessException("Não foi possível encontrar o prazo para modalidade.");
                }

                proposalEntity.DadosGarantia.LegalRecourseType = await legalRecourseTypeApplication.GetAsync(entity.CodigoTipoRecurso);
                if (proposalEntity.DadosGarantia.LegalRecourseType == null) {
                    throw new BusinessException("Recurso não encontrado.");
                }

                proposalEntity.DadosGarantia.LaborCourt = await laborCourtApplication.GetAsync(entity.CodigoTribunal);
                if (proposalEntity.DadosGarantia.LaborCourt == null) {
                    throw new BusinessException("Tribunal não encontrado.");
                }

                if (entity.CodigoVara != null || entity.CodigoVara > 0) {
                    proposalEntity.DadosGarantia.CivilCourt = await civilCourtApplication.GetAsync(entity.CodigoVara.Value, entity.CodigoTribunal);
                    if (proposalEntity.DadosGarantia.CivilCourt == null) {
                        throw new BusinessException("Não foi possível encontrar a vara para o tribunal.");
                    }
                }

                if (entity.PercentualAgravo == null) {
                    var coberturaAgravo = await coverageApplication.GetParametersAsync(proposalEntity.DadosGarantia.Coverage.ExternalCode.Value);
                    proposalEntity.DadosGarantia.PercentualAgravo = coberturaAgravo.DefaultValue;
                } else {
                    proposalEntity.DadosGarantia.PercentualAgravo = entity.PercentualAgravo;
                }

                if (entity.ValorDepositoRecursal == null) {
                    var tipoRecursoParametro = await legalRecourseTypeApplication.GetParameterAsync(proposalEntity.DadosGarantia.LegalRecourseType.LegalRecourseTypeId ?? 0);
                    proposalEntity.DadosGarantia.ValorDepositoRecursal = tipoRecursoParametro.ValorDepositoRecursal;
                } else {
                    proposalEntity.DadosGarantia.ValorDepositoRecursal = entity.ValorDepositoRecursal;
                }

                proposalEntity.DadosGarantia.ValorImportanciaSegurada = proposalEntity.DadosGarantia.ValorDepositoRecursal * (1 + proposalEntity.DadosGarantia.PercentualAgravo / 100);
                proposalEntity.DadosGarantia.ValorImportanciaSeguradaRecursal = proposalEntity.DadosGarantia.ValorDepositoRecursal * (1 + proposalEntity.DadosGarantia.PercentualAgravo / 100);
                proposalEntity.IsPremioInformado = true;
                proposalEntity.DataInicioVigencia = DateTime.Now;
                proposalEntity.DataProposta = DateTime.Now;
                proposalEntity.StatusProposta = StatusPropostaEnum.EmNegociacao;
                proposalEntity.TipoSeguro = TipoSeguroEnum.Recursal;
                proposalEntity.DadosGarantia.NumeroProcesso = entity.NumeroProcesso;
                proposalEntity.Taker.CpfCnpjCorretor = entity.CpfCnpjTomador;
                proposalEntity.Broker.IdUsuarioCorretor = brokerUserIdbrokerUserId;
                proposalEntity.DadosGarantia.Reclamantes = MapDadosReclamente(entity);
                proposalEntity.DadosGarantia.ValorAdicionalFracionamento = 0;
                proposalEntity.DadosGarantia.ValorIOF = 0;

                await LoadInsured(proposalEntity);

                return await proposalApplication.AddAsync(proposalEntity);

            } catch (Exception e) {
                if ((e is BusinessException || e is DaoException || e is ServiceException)) {
                    throw e;
                }
                throw new ApplicationException("Erro na gravação da proposta.", e);
            }
        }

        private async Task LoadInsured(ProposalEntity entity) {
            switch (entity.DadosGarantia.TipoDeSegurado) {
                case TipoSeguradoRecursalEnum.Tribunal:
                    if (string.IsNullOrWhiteSpace(entity.DadosGarantia.LaborCourt.ExternalPersonCode)) {

                        //-- Cadastra primeiro no legado
                        var cep = await zipCodeApplication.GetAsync(entity.DadosGarantia.LaborCourt.ZipCode?.FormatCep().Replace("-", ""));

                        entity.Insured = new InsuredEntity {
                            Nome = entity.DadosGarantia.LaborCourt.Name,
                            TipoPessoa = TipoPessoaEnum.OrgãoPublico,
                            CpfCnpj = 0,
                            IdPessoa = null,
                            Contato = null,
                            Endereco = new EnderecoEntity {
                                Logradouro = entity.DadosGarantia.LaborCourt.Address,
                                Numero = entity.DadosGarantia.LaborCourt.AddressNumber,
                                Complemento = entity.DadosGarantia.LaborCourt.AddressComplement,
                                Bairro = entity.DadosGarantia.LaborCourt.District,
                                Cidade = entity.DadosGarantia.LaborCourt.City,
                                UF = entity.DadosGarantia.LaborCourt.State,
                                IdCidade = cep.IdCidade.Value,
                                IdUf = (int)EnumExtension.ParseFromDefaultValue<UfEnum>(entity.DadosGarantia.LaborCourt.State),
                                Cep = entity.DadosGarantia.LaborCourt.ZipCode?.FormatCep().Replace("-", ""),
                                TipoEndereco = new TipoEnderecoEntity {
                                    IdTipoEndereco = 5, //-- Fixo Cobrança
                                    NomeTipoEndereco = ""
                                }
                            }
                        };
                        var retornoIncluirSegurado = await insuredService.AddAsync(entity.Insured);
                        //-- Atualiza os ids do legado para a vara
                        entity.DadosGarantia.LaborCourt.ExternalPersonCode = Convert.ToString(retornoIncluirSegurado.IdPessoa);
                        entity.DadosGarantia.LaborCourt.ExternalAddressCode = Convert.ToString(retornoIncluirSegurado.IdEndereco);

                        await laborCourtApplication.UpdateAsync(entity.DadosGarantia.LaborCourt);

                        //-- Prossegue com os ids do legado
                        entity.Insured.IdPessoa = retornoIncluirSegurado.IdPessoa;
                        entity.Insured.Endereco.IdEndereco = retornoIncluirSegurado.IdEndereco;
                    } else {
                        //-- Prossegue com os ids do legado
                        entity.Insured = new InsuredEntity {
                            IdPessoa = Convert.ToInt32(entity.DadosGarantia.LaborCourt.ExternalPersonCode),
                            Endereco = new EnderecoEntity {
                                IdEndereco = Convert.ToInt32(entity.DadosGarantia.LaborCourt.ExternalAddressCode),
                            }
                        };
                    }
                    break;

                case TipoSeguradoRecursalEnum.Vara:
                    if (string.IsNullOrWhiteSpace(entity.DadosGarantia.CivilCourt.ExternalPersonCode)) {

                        //-- Cadastra primeiro no legado
                        var cep = await zipCodeApplication.GetAsync(entity.DadosGarantia.CivilCourt.ZipCode?.FormatCep().Replace("-", ""));

                        entity.Insured = new InsuredEntity {
                            Nome = entity.DadosGarantia.CivilCourt.Name,
                            TipoPessoa = TipoPessoaEnum.OrgãoPublico,
                            CpfCnpj = 0,
                            IdPessoa = null,
                            Contato = null,

                            Endereco = new EnderecoEntity {
                                Logradouro = entity.DadosGarantia.CivilCourt.Address,
                                Numero = entity.DadosGarantia.CivilCourt.AddressNumber,
                                Complemento = entity.DadosGarantia.CivilCourt.AddressComplement,
                                Bairro = entity.DadosGarantia.CivilCourt.District,
                                Cidade = entity.DadosGarantia.CivilCourt.City,
                                UF = entity.DadosGarantia.CivilCourt.State,
                                IdCidade = cep.IdCidade.Value,
                                IdUf = (int)EnumExtension.ParseFromDefaultValue<UfEnum>(entity.DadosGarantia.CivilCourt.State),
                                Cep = entity.DadosGarantia.CivilCourt.ZipCode?.FormatCep().Replace("-", ""),
                                TipoEndereco = new TipoEnderecoEntity {
                                    IdTipoEndereco = 5, //-- Fixo Cobrança
                                    NomeTipoEndereco = ""
                                }
                            }
                        };
                        var retornoIncluirSegurado = await insuredService.AddAsync(entity.Insured);
                        //-- Atualiza os ids do legado para a vara
                        entity.DadosGarantia.CivilCourt.ExternalPersonCode = Convert.ToString(retornoIncluirSegurado.IdPessoa);
                        entity.DadosGarantia.CivilCourt.ExternalAddressCode = Convert.ToString(retornoIncluirSegurado.IdEndereco);

                        await civilCourtApplication.UpdateAsync(entity.DadosGarantia.CivilCourt);

                        //-- Prossegue com os ids do legado
                        entity.Insured.IdPessoa = retornoIncluirSegurado.IdPessoa;
                        entity.Insured.Endereco.IdEndereco = retornoIncluirSegurado.IdEndereco;
                    } else {
                        //-- Prossegue com os ids do legado
                        entity.Insured = new InsuredEntity {
                            IdPessoa = Convert.ToInt32(entity.DadosGarantia.CivilCourt.ExternalPersonCode),
                            Endereco = new EnderecoEntity {
                                IdEndereco = Convert.ToInt32(entity.DadosGarantia.CivilCourt.ExternalAddressCode),
                            }
                        };
                    }
                    break;

                case TipoSeguradoRecursalEnum.Reclamante:
                default:
                    var dadosReclamante = entity.DadosGarantia.Reclamantes.Where(x => x.IsPrincipal == true).FirstOrDefault();
                    if (dadosReclamante == null) {
                        throw new BusinessException("Nenhum reclamante foi definido como sendo o principal.");
                    }

                    var dadosEndereco = await zipCodeApplication.GetAsync(dadosReclamante.Segurado.Endereco.Cep);
                    entity.Insured = new InsuredEntity {
                        Nome = dadosReclamante.NomeReclamante,
                        TipoPessoa = (dadosReclamante.CpfCnpjReclamante.ToString().Length <= 11 ? TipoPessoaEnum.PessoaFisica : TipoPessoaEnum.PessoaJuridica),
                        CpfCnpj = dadosReclamante.CpfCnpjReclamante,
                        IdPessoa = null,
                        Contato = null,
                        Endereco = new EnderecoEntity {
                            Logradouro = dadosEndereco.Logradouro,
                            Numero = string.IsNullOrWhiteSpace(dadosReclamante.Segurado.Endereco.Numero) ? "s/n" : dadosReclamante.Segurado.Endereco.Numero,
                            Complemento = dadosReclamante.Segurado.Endereco.Complemento,
                            Bairro = dadosEndereco.Bairro,
                            Cidade = dadosEndereco.Cidade,
                            UF = dadosEndereco.UF,
                            IdUf = dadosEndereco.IdUf,
                            Cep = dadosReclamante.Segurado.Endereco.Cep,
                            IdCidade = dadosEndereco.IdCidade.Value,
                            TipoEndereco = new TipoEnderecoEntity {
                                IdTipoEndereco = 5, //-- Fixo Cobrança
                                NomeTipoEndereco = ""
                            }
                        }
                    };

                    var registeredInsured = await insuredApplication.IsRegisteredAsync(dadosReclamante.CpfCnpjReclamante.Value);
                    if (registeredInsured == null) {
                        var addedData = await insuredService.AddAsync(entity.Insured);
                        entity.Insured.IdPessoa = addedData.IdPessoa;
                        entity.Insured.Endereco.IdEndereco = addedData.IdEndereco;
                    } else {
                        entity.Insured.IdPessoa = registeredInsured.IdPessoa;
                        entity.Insured.Endereco.IdEndereco = registeredInsured.Endereco.IdEndereco;
                    }
                    break;
            }
        }

        private static List<DadosReclamanteEntity> MapDadosReclamente(ProposalAppealEntity entity) {
            List<DadosReclamanteEntity> mapperList = new List<DadosReclamanteEntity>();
            var mapperObj = new DadosReclamanteEntity() {
                IsPrincipal = true,
                CpfCnpjReclamante = entity.CpfCnpjReclamente.ToLong(),
                NomeReclamante = entity.NomeRazaoSocialReclamente,
                Segurado = new InsuredEntity() {
                    Endereco = new EnderecoEntity() {
                        Cep = entity.CepReclamente,
                        Bairro = entity.BairroReclamente,
                        Cidade = entity.CidadeReclamente,
                        Numero = entity.NumeroReclamente,
                        UF = entity.UfReclamente,
                        Complemento = entity.ComplementoReclamente
                    }
                }
            };

            mapperList.Add(mapperObj);

            return mapperList;
        }
    }
}
