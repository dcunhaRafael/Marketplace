using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class ProposalApplication : IProposalApplication {
        private readonly IProposalService proposalService;
        private readonly IPolicyService policyService;
        private readonly IBCDataService bcDataService;
        private readonly IBrokerApplication brokerApplication;
        private readonly IInsuredObjectApplication insuredObjectApplication;
        private readonly ILegalRecourseTypeApplication legalRecourseTypeApplication;
        private readonly ISalesChannelApplication salesChannelApplication;
        private readonly ITakerApplication takerApplication;
        private readonly IProposalDao proposalDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IBrokerDao brokerMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IBureauDao bureauMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IClaimantsDao claimantsMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.ICommercialEntityStructureDao commercialEntityStructureMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.ICoverageDao coverageMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IInsuredDao insuredMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IInsuredContactDao insuredContactMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.ILegalRecourseTypeParameterDao legalRecourseTypeParameterMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IPaymentInstallmentsDao paymentInstallmentsMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProductDao productMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalDao proposalMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalParcDao proposalParcMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalInsuredObjectDao proposalInsuredObjectMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalStatusDao proposalStatusMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalTypeDao proposalTypeMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IRecursalPolicyExpDateDao recursalPolicyExpDateMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IRegistryBureauDao registryBureauMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.ITakerDao takerMpDao;
        private readonly InsuranceApi.Domain.Interfaces.Dao.Marketplace.IWarrantyOptionsDao warrantyOptionsMpDao;

        public ProposalApplication(
             IProposalService proposalService, IPolicyService policyService, IBCDataService bcDataService,
             IBrokerApplication brokerApplication,
             IInsuredObjectApplication insuredObjectApplication,
             ILegalRecourseTypeApplication legalRecourseTypeApplication,
             ISalesChannelApplication salesChannelApplication,
             ITakerApplication takerApplication,
             IProposalDao proposalDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IBrokerDao brokerMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IBureauDao bureauMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IClaimantsDao claimantsMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.ICommercialEntityStructureDao commercialEntityStructureMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.ICoverageDao coverageMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IInsuredDao insuredMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IInsuredContactDao insuredContactMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.ILegalRecourseTypeParameterDao legalRecourseTypeParameterMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IPaymentInstallmentsDao paymentInstallmentsMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProductDao productMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalDao proposalMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalParcDao proposalParcMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalInsuredObjectDao proposalInsuredObjectMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalStatusDao proposalStatusMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalTypeDao proposalTypeMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IRecursalPolicyExpDateDao recursalPolicyExpDateMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IRegistryBureauDao registryBureauMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.ITakerDao takerMpDao,
             InsuranceApi.Domain.Interfaces.Dao.Marketplace.IWarrantyOptionsDao warrantyOptionsMpDao) {
            this.proposalService = proposalService;
            this.policyService = policyService;
            this.bcDataService = bcDataService;
            this.brokerApplication = brokerApplication;
            this.insuredObjectApplication = insuredObjectApplication;
            this.legalRecourseTypeApplication = legalRecourseTypeApplication;
            this.salesChannelApplication = salesChannelApplication;
            this.takerApplication = takerApplication;
            this.proposalDao = proposalDao;
            this.brokerMpDao = brokerMpDao;
            this.bureauMpDao = bureauMpDao;
            this.claimantsMpDao = claimantsMpDao;
            this.commercialEntityStructureMpDao = commercialEntityStructureMpDao;
            this.coverageMpDao = coverageMpDao;
            this.insuredMpDao = insuredMpDao;
            this.insuredContactMpDao = insuredContactMpDao;
            this.legalRecourseTypeParameterMpDao = legalRecourseTypeParameterMpDao;
            this.paymentInstallmentsMpDao = paymentInstallmentsMpDao;
            this.productMpDao = productMpDao;
            this.proposalMpDao = proposalMpDao;
            this.proposalParcMpDao = proposalParcMpDao;
            this.proposalInsuredObjectMpDao = proposalInsuredObjectMpDao;
            this.proposalStatusMpDao = proposalStatusMpDao;
            this.proposalTypeMpDao = proposalTypeMpDao;
            this.recursalPolicyExpDateMpDao = recursalPolicyExpDateMpDao;
            this.registryBureauMpDao = registryBureauMpDao;
            this.takerMpDao = takerMpDao;
            this.warrantyOptionsMpDao = warrantyOptionsMpDao;
        }

        public async Task<PropostaRetornoImprimirEntity> PrintAsync(int endorsementId) {
            try {
                return await proposalService.PrintAsync(endorsementId);
            } catch (Exception e) {
                if ((e is DaoException || e is ServiceException || e is ApplicationException)) {
                    if (!e.Message.Equals("Dados não preenchidos.")) {
                        throw new BusinessException("Endosso não encontrado.");
                    }
                }
                throw new ApplicationException($"Erro ao obter minuta ({e.Message}).", e);
            }
        }

        public async Task<ProposalReturnGravarEntity> AddAsync(ProposalEntity proposal) {
            try {

                proposal.Broker = await brokerApplication.GetAsync(null, proposal.Broker.IdUsuarioCorretor);

                var corretorCadastradoInterno = await brokerApplication.GetAsync(proposal.Broker.IdPessoa);
                if (corretorCadastradoInterno == null) {
                    throw new BusinessException("Não foi possível obter o corretor associado ao usuário.");
                }

                proposal.IdPessoaProdutor = corretorCadastradoInterno.IdPessoa;

                proposal.SalesChannel = await salesChannelApplication.GetAsync(proposal.Product.CodigoProduto.Value, proposal.IdPessoaProdutor.Value);
                if (proposal.SalesChannel == null) {
                    throw new BusinessException("Não foi possível obter o canal de venda padrão para o produto e usuário logado.");
                }

                proposal.Taker = await takerApplication.GetAsync(proposal.Taker.CpfCnpj.FormatCpfCnpjToString().ApenasNumericos(), proposal.Broker.IdUsuarioCorretor);
                if (proposal.TipoSeguro.Equals(TipoSeguroEnum.Recursal)) {
                    var taxasRecursalTomador = await legalRecourseTypeApplication.GetAppealFeeAsync(
                                                proposal.Taker.IdPessoa ?? 0,
                                                proposal.Product.CodigoProduto.Value,
                                                proposal.DadosGarantia.Coverage.IdCobertura ?? 0,
                                                proposal.DadosGarantia.ValorImportanciaSeguradaRecursal ?? 0,
                                                proposal.DadosGarantia.TermType.Id);

                    if (taxasRecursalTomador == null) {
                        var taxasRecursal = await legalRecourseTypeApplication.GetRateAsync(proposal.DadosGarantia.ValorImportanciaSegurada ?? 0, proposal.DadosGarantia.TermType.Id);
                        if (taxasRecursal == null) {
                            throw new BusinessException("Taxas não localidadas para a IS e prazo informados.");
                        }
                        proposal.DadosGarantia.ValorPremioTotal = taxasRecursal.PremiumValue ?? 0;
                    } else {
                        proposal.DadosGarantia.ValorPremioTotal = taxasRecursalTomador.PremiumValue ?? 0;
                    }

                    proposal.DadosGarantia.ValorPremioTarifario = proposal.DadosGarantia.ValorPremioTotal;
                }

                proposal.InsuredObject.Contents = await insuredObjectApplication.GetTextAsync(proposal);

                if (proposal.DadosGarantia.TermType != null) {
                    if (proposal.DadosGarantia.TermType.TermUnit == TermUnitEnum.Years) {
                        proposal.DataFimVigencia = proposal.DataInicioVigencia.Value.Date.AddYears(proposal.DadosGarantia.TermType.TermSize.Value);
                    } else if (proposal.DadosGarantia.TermType.TermUnit == TermUnitEnum.Days) {
                        proposal.DataFimVigencia = proposal.DataInicioVigencia.Value.Date.AddDays(proposal.DadosGarantia.TermType.TermSize.Value);
                    }
                }

                proposal.DadosCobranca.FormaPagamentoPrimeiraParcela.CodigoFormaPagamento = proposal.DadosGarantia.Coverage.DefaultPaymentFormId;
                proposal.DadosCobranca.FormaPagamentoDemaisParcelas.CodigoFormaPagamento = proposal.DadosGarantia.Coverage.DefaultPaymentFormId;
                if (proposal.DadosGarantia.Coverage.DefaultPaymentDay != null) {
                    switch (proposal.DadosGarantia.Coverage.DefaultPaymentDay) {
                        case PaymentDayEnum.PrimeiroDiaMes:
                            proposal.DadosCobranca.DataVencimentoPrimeiraParcela = DateTime.Now.FirstDayOfMonth(true);
                            break;
                        case PaymentDayEnum.UltimoDiaMes:
                            proposal.DadosCobranca.DataVencimentoPrimeiraParcela = DateTime.Now.LastDayOfMonth();
                            break;
                        default:
                            break;
                    }
                }

                proposal.DadosCobranca.Parcelamento.QuantidadeParcelas = 1;
                proposal.DadosCobranca.Parcelamento.IdParcelamento = proposal.DadosGarantia.Coverage.DefaultPaymentInstallmentId;

                proposal.DadosCobranca.PeridiocidadePagamento.IdPeridiocidadePagamento = proposal.DadosGarantia.Coverage.DefaultPaymentFrequencyId;

                var parametrosCalculo = await takerApplication.GetParameterAsync(proposal.Taker.IdPessoa.Value);
                if (parametrosCalculo == null) {
                    throw new BusinessException("Não foi possível encontrar os parâmetros de cálculo para a empresa.");
                }

                decimal percentualComissao = decimal.Zero;
                if (parametrosCalculo != null) {
                    percentualComissao = parametrosCalculo.PercentualComissao;
                }
                proposal.DadosGarantia.PercentualComissao = percentualComissao;

                decimal percentualTaxaRisco = decimal.Zero;
                if (parametrosCalculo != null) {
                    percentualTaxaRisco = parametrosCalculo.ValorTaxaRisco;
                }
                proposal.DadosGarantia.ValorTaxaRisco = percentualTaxaRisco;

                var retornoGravar = await proposalService.AddAsync(proposal, proposal.Broker.IdUsuarioCorretor.Value, proposal.Broker.CodigoSusep);
                if (retornoGravar == null) {
                    throw new ApplicationException("Erro na gravação da proposta no legado!");
                }

                CarregarDadosNaoTratadosPeloLegado(proposal, retornoGravar.Proposta);

                // Complementa os dados de comissão
                retornoGravar.Proposta.Comissao.Add(new ProposalCommissionDistributionEntity() {
                    ProducerId = proposal.Broker.IdPessoa,
                    ComissionValue = retornoGravar.Proposta.DadosGarantia.ValorComissao.Value,
                    TypeOfComission = (int)TipoComissaoEnum.Corretagem,
                    ParticipationPercentage = Math.Round((retornoGravar.Proposta.DadosGarantia.PercentualComissao.Value / 100) * 100, 2),
                    ProposalPercentage = retornoGravar.Proposta.DadosGarantia.PercentualComissao.Value,
                    IsPrincipalProducer = 1,
                    Print = 1,
                    ProducerCpfCnpj = proposal.Broker.CpfCnpj
                });

                // Complementos do Marketplace
                if (retornoGravar.Proposta.DadosGarantia.LegalRecourseType.LegalRecourseTypeId != null) {
                    retornoGravar.Proposta.DadosGarantia.LegalRecourseTypeParameter =
                        await legalRecourseTypeApplication.GetParameterAsync(retornoGravar.Proposta.DadosGarantia.LegalRecourseType.LegalRecourseTypeId.Value);
                }

                await InsertProposal(proposal, retornoGravar);
                await InsertProposalMarketplace(retornoGravar);

                return retornoGravar;

            } catch (Exception e) {
                if ((e is BusinessException || e is DaoException || e is ServiceException)) {
                    throw e;
                }
                throw new ApplicationException($"Erro na gravação da proposta no legado ({e.Message}).", e);
            }
        }

        public async Task<ProposalReturnGravarEntity> AddRenovalAsync(ProposalRenewalEntity renewal) {
            try {

                var broker = await brokerApplication.GetAsync(renewal.BrokerDocument.Value);
                if (broker == null || broker.IdUsuarioCorretor == null) {
                    throw new BusinessException("Não foi possível obter os dados do corretor para acesso ao legado.");
                }
                var policies = await policyService.ListPolicyAsync(new PolicySearchEntity() { NumeroApolice = renewal.PolicyCode?.ToString(), IdUsuario = broker.IdUsuarioCorretor.Value, CodigoSusepUsuario = broker.CodigoSusep });
                var currentPolicy = policies.FirstOrDefault();
                if (currentPolicy == null) {
                    throw new BusinessException("Não foi possível obter a apólice no legado.");
                }

                var currentProposal = await proposalService.GetAsync(int.Parse(currentPolicy.NumeroProposta), broker.IdUsuarioCorretor.Value, broker.CodigoSusep);
                if (currentProposal == null) {
                    throw new BusinessException("Não foi possível obter a proposta no legado.");
                }

                // Atualiza os dados da proposta com os valores da renovação
                currentProposal.DataInicioVigencia = renewal.NewStartOfTerm;
                currentProposal.DataFimVigencia = renewal.NewEndOfTerm;
                currentProposal.DadosGarantia.ValorImportanciaSegurada = renewal.NewInsuredAmount;
                currentProposal.InsuredObject.Contents = renewal.NewInsuredObject;
                currentProposal.DadosCobranca.DataVencimentoPrimeiraParcela = renewal.NewStartOfTerm;

                // Gera a proposta de renovação no legado
                var retornoGravar = await proposalService.AddAsync(currentProposal, broker.IdUsuarioCorretor.Value, broker.CodigoSusep);
                if (retornoGravar == null) {
                    throw new ApplicationException("Erro na gravação da proposta no legado!");
                }

                // Grava a proposta no db do marketplace
                retornoGravar.Proposta.Broker = broker;
                retornoGravar.Proposta.Id = await InsertProposalMarketplace(retornoGravar);

                return retornoGravar;

            } catch (Exception e) {
                if ((e is BusinessException || e is DaoException || e is ServiceException)) {
                    throw e;
                }
                throw new ApplicationException($"Erro na gravação da proposta no legado ({e.Message}).", e);
            }
        }

        private async Task InsertProposal(ProposalEntity proposal, ProposalReturnGravarEntity retornoGravar) {
            try {

                await proposalDao.AddAsync(retornoGravar.Proposta);

            } catch (Exception e) {
                throw new ApplicationException($"Erro na gravação da proposta no portal ({e.Message}). A mesma deverá ser cancelada manualmente no legado.", e);
            }
        }

        private async Task<int> InsertProposalMarketplace(ProposalReturnGravarEntity retornoGravar) {
            try {

                var broker = await brokerMpDao.GetAsync(retornoGravar.Proposta.Broker.IdUsuarioCorretor.Value);
                if (broker == null) {
                    throw new BusinessException("Não foi possível identificar o corretor no Marketplace.");
                }
                var taker = await takerMpDao.GetAsync(retornoGravar.Proposta.Taker.CpfCnpj.Value);
                if (taker == null) {
                    throw new BusinessException("Não foi possível identificar a empresa no Marketplace.");
                }
                var insured = await insuredMpDao.GetAsync(retornoGravar.Proposta.Insured.CpfCnpj.Value);
                int insuredId;
                if (insured == null) {
                    if (retornoGravar.Proposta.Insured.Contato?.Email != null) {
                        retornoGravar.Proposta.Insured.Contato.Id = await insuredContactMpDao.AddAsync(new InsuredEntity() {
                            Nome = retornoGravar.Proposta.Insured.Nome,
                            CpfCnpj = retornoGravar.Proposta.Insured.CpfCnpj.Value,
                            Contato = new ContatoEntity() { Email = retornoGravar.Proposta.Insured.Contato.Email },
                            IdPessoa = retornoGravar.Proposta.Insured.IdPessoa
                        });
                    }
                    insuredId = await insuredMpDao.AddAsync(retornoGravar.Proposta.Insured);
                } else {
                    insuredId = insured.Id;
                }
                var commercialEntityStructure = await commercialEntityStructureMpDao.GetAsync(broker.CpfCnpj);
                if (commercialEntityStructure == null) {
                    throw new BusinessException("Não foi possível identificar a estrutura comercial no Marketplace.");
                }
                var proposalType = await proposalTypeMpDao.GetAsync(retornoGravar.Proposta.Product.CodigoExterno.Value, retornoGravar.Proposta.DadosGarantia.Coverage.ExternalCode.Value);
                if (proposalType == null) {
                    throw new BusinessException("Não foi possível identificar o tipo de proposta no Marketplace.");
                }
                var product = await productMpDao.GetAsync(retornoGravar.Proposta.Product.CodigoExterno.Value);
                if (product == null) {
                    throw new BusinessException("Não foi possível identificar o produto no Marketplace.");
                }
                var coverage = await coverageMpDao.GetAsync(retornoGravar.Proposta.DadosGarantia.Coverage.ExternalCode.Value);
                if (coverage == null) {
                    throw new BusinessException("Não foi possível identificar a modalidade no Marketplace.");
                }
                var insuredObject = await proposalInsuredObjectMpDao.GetAsync(proposalType.Id);
                if (insuredObject == null) {
                    throw new BusinessException("Não foi possível identificar o objeto segurado no Marketplace.");
                }
                var proposalStatus = await proposalStatusMpDao.GetAsync((int)retornoGravar.Proposta.StatusProposta);
                if (proposalStatus == null) {
                    throw new BusinessException("Não foi possível identificar o status da proposta no Marketplace.");
                }

                BureauEntity bureau = null;
                RegistryBureauEntity registryBureau = null;
                LegalRecourseTypeParameterEntity legalRecourseTypeParameter = null;
                TermTypeEntity termType = null;

                if (retornoGravar.Proposta.TipoSeguro == TipoSeguroEnum.Recursal) {
                    bureau = await bureauMpDao.GetAsync(retornoGravar.Proposta.DadosGarantia.LaborCourt.ExternalCode);
                    if (bureau == null) {
                        throw new BusinessException("Não foi possível identificar o tribunal no Marketplace.");
                    }
                    registryBureau = await registryBureauMpDao.GetAsync(retornoGravar.Proposta.DadosGarantia.CivilCourt.ExternalCode);
                    if (registryBureau == null) {
                        throw new BusinessException("Não foi possível identificar a vara no Marketplace.");
                    }
                    legalRecourseTypeParameter = await legalRecourseTypeParameterMpDao.GetAsync(retornoGravar.Proposta.DadosGarantia.LegalRecourseType.LegalRecourseTypeId.Value);
                    if (legalRecourseTypeParameter == null) {
                        throw new BusinessException("Não foi possível identificar o parâmetro do tipo de recurso no Marketplace.");
                    }
                    termType = await recursalPolicyExpDateMpDao.GetAsync(retornoGravar.Proposta.DadosGarantia.TermType.Id);
                    if (termType == null) {
                        throw new BusinessException("Não foi possível identificar o prazo no Marketplace.");
                    }

                    //THOR pois a Aviatec ao invés de usar o campo ProcessNumer para guardar o número do processo usa o campo ContractNumber que seria para número da Licitação/Contrato
                    retornoGravar.Proposta.DadosGarantia.NumeroLicitacao = retornoGravar.Proposta.DadosGarantia.NumeroProcesso?.ApenasNumericos();
                }

                //-- Grava o pai de todos
                var proposalId = await proposalMpDao.AddAsync(retornoGravar.Proposta, commercialEntityStructure.CommercialStructureId.Value, broker.IdCorretor, taker.TakerId.Value, insuredId,
                    insured.InsuredTypeId, proposalType.Id, product.CodigoProduto.Value, coverage.IdCobertura.Value, proposalStatus.Id, proposalStatus.Name, insuredObject.Id,
                    legalRecourseTypeParameter?.Id, termType?.Id, bureau?.Id, registryBureau?.Id);

                //-- Dados dos tipos de recurso, a API trata apenas 1 tipo de recurso
                if (retornoGravar.Proposta.TipoSeguro == TipoSeguroEnum.Recursal) {
                    await warrantyOptionsMpDao.AddAsync(new WarrantyOptionsEntity() {
                        DeadLineValidity = retornoGravar.Proposta.DataInicioVigencia.Value,
                        DeadLineValidityOption = termType.Id,
                        ProposalId = proposalId,
                        RecursalModalityAmountOfInsuredValue = retornoGravar.Proposta.DadosGarantia.ValorImportanciaSeguradaRecursal.Value,
                        RecursalModalityDepositValue = retornoGravar.Proposta.DadosGarantia.LegalRecourseTypeParameter.ValorDepositoRecursal,
                        RecursalModalityMaxValue = legalRecourseTypeParameter.ApeelDepositAmount,
                        RecursalModalityPercentageHarm = retornoGravar.Proposta.DadosGarantia.PercentualAgravo.Value,
                        RecursalModalityType = retornoGravar.Proposta.DadosGarantia.LegalRecourseType.Name,
                    });
                }

                //-- Dados dos reclamantes, a API trata apenas 1 reclamante
                if (retornoGravar.Proposta.TipoSeguro == TipoSeguroEnum.Recursal) {
                    await claimantsMpDao.AddAsync(new ClaimantsEntity() {
                        ProposalId = proposalId,
                        InsuredId = insuredId
                    });
                }

                //-- Cláusulas *** NÃO TRATA CLÁUSULAS NA API, JSON DE ENTRADA NÃO TEM ESSA INFORMAÇÃO ***
                //sbSql = new StringBuilder();
                //sbSql.AppendLine("INSERT INTO ProposalClauses ( ProposalId, ClauseId )");
                //sbSql.AppendLine("  VALUES ( @ProposalId, @ClauseId )");
                //foreach (var parcela in retornoGravar.Proposta.Clausulas) {
                //    await connection.ExecuteAsync(sbSql.ToString(), new {
                //        ProposalId = proposalId,
                //        ClauseId = parcela.ClauseProductCoverageId,
                //    });
                //}

                //-- Parcelas
                await proposalParcMpDao.AddAsync(proposalId, retornoGravar.Proposta.Parcelas);
                //await paymentInstallmentsMpDao.AddAsync(retornoGravar.Proposta.IdApolice, retornoGravar.Proposta.Parcelas);

                return proposalId;

            } catch (Exception e) {
                throw new ApplicationException($"Erro na gravação da proposta no Marketplace ({e.Message}). A mesma deverá ser cancelada manualmente no legado.", e);
            }
        }

        public async Task<ProposalReturnEmitirEntity> ApproveAsync(int proposalCode, int proposalBrokerUserId) {
            try {

                var propostaRetornoEmitirEntity = new ProposalReturnEmitirEntity();

                var resCheck = await CheckApproveProposalAsync(proposalCode, proposalBrokerUserId);
                if (resCheck.Success) {
                    if (resCheck.StatusProposta == StatusPropostaEnum.EmSubscricao) {
                        var resApprove = await ApproveProposalAsync(proposalCode, proposalBrokerUserId);
                        propostaRetornoEmitirEntity.StatusProposta = resApprove.StatusProposta;
                    } else {
                        propostaRetornoEmitirEntity.StatusProposta = resCheck.StatusProposta;
                    }
                } else {
                    throw new ServiceException(resCheck.Message);
                }

                //-- Atualiza DB Ebix
                await proposalDao.UpdateStatusAsync(proposalCode, (int)propostaRetornoEmitirEntity.StatusProposta);

                //-- Atualiza DB marketplace
                var proposalStatus = await proposalStatusMpDao.GetAsync((int)propostaRetornoEmitirEntity.StatusProposta);
                if (proposalStatus == null) {
                    throw new BusinessException("Não foi possível identificar o status da proposta no Marketplace.");
                }
                await proposalMpDao.UpdateStatusAsync(proposalCode, proposalStatus);


                return propostaRetornoEmitirEntity;

            } catch (Exception e) {
                if ((e is DaoException || e is ServiceException || e is ApplicationException)) {
                    throw e;
                }
                throw new ApplicationException($"Erro na aprovação da proposta ({e.Message}).", e);
            }
        }

        public async Task<ProposalEntity> GetAsync(int proposalCode, int? proposalBrokerUserId) {
            try {

                var broker = await brokerApplication.GetAsync(null, proposalBrokerUserId);

                return await proposalService.GetAsync(proposalCode, broker.IdUsuarioCorretor.Value, broker.CodigoSusep);

            } catch (Exception e) {
                if ((e is DaoException || e is ServiceException || e is ApplicationException)) {
                    throw e;
                }
                throw new ApplicationException($"Erro obtendo proposta ({e.Message}).", e);
            }
        }

        public async Task<PropostaRetornoVerificarAprovacaoEntity> CheckApproveProposalAsync(int proposalCode, int proposalBrokerUserId) {
            try {

                var broker = await brokerApplication.GetAsync(null, proposalBrokerUserId);
                var res = await proposalService.CheckApprovalAsync(proposalCode, broker.IdUsuarioCorretor.Value, broker.CodigoSusep);

                return res;

            } catch (Exception e) {
                if ((e is DaoException || e is ServiceException || e is ApplicationException)) {
                    throw e;
                }
                throw new ApplicationException($"Erro na verificação da aprovação da proposta ({e.Message}).", e);
            }
        }

        private async Task<ProposalReturnAprovarEntity> ApproveProposalAsync(int proposalCode, int proposalBrokerUserId) {
            try {

                var broker = await brokerApplication.GetAsync(null, proposalBrokerUserId);
                var proposalStatus = await proposalService.ApproveAsync(proposalCode, broker.IdUsuarioCorretor.Value, broker.CodigoSusep);
                await proposalDao.UpdateStatusAsync(proposalCode, (int)proposalStatus.StatusProposta);

                return proposalStatus;

            } catch (Exception e) {
                if ((e is DaoException || e is ServiceException || e is ApplicationException)) {
                    throw e;
                }
                throw new ApplicationException($"Erro na aprovação da proposta ({e.Message}).", e);
            }
        }

        private void CarregarDadosNaoTratadosPeloLegado(ProposalEntity propostaAntesEnvioLegado, ProposalEntity propostaRetornadaLegado) {
            if (propostaRetornadaLegado != null) {
                propostaRetornadaLegado.TipoSeguro = propostaAntesEnvioLegado.TipoSeguro;
                propostaRetornadaLegado.IdPessoaProdutor = propostaAntesEnvioLegado.IdPessoaProdutor;
                propostaRetornadaLegado.Broker = propostaAntesEnvioLegado.Broker;
                propostaRetornadaLegado.SalesChannel = propostaAntesEnvioLegado.SalesChannel;
                propostaRetornadaLegado.Product = propostaAntesEnvioLegado.Product;
                propostaRetornadaLegado.DadosGarantia.Coverage = propostaAntesEnvioLegado.DadosGarantia.Coverage;
                propostaRetornadaLegado.DadosGarantia.TipoDeCredor = propostaAntesEnvioLegado.DadosGarantia.TipoDeCredor;
                propostaRetornadaLegado.DadosGarantia.EstadoDoCredor = propostaAntesEnvioLegado.DadosGarantia.EstadoDoCredor;
                propostaRetornadaLegado.DadosGarantia.MunicipioDoCredor = propostaAntesEnvioLegado.DadosGarantia.MunicipioDoCredor;
                propostaRetornadaLegado.DadosGarantia.Reclamantes = propostaAntesEnvioLegado.DadosGarantia.Reclamantes;
                propostaRetornadaLegado.DadosGarantia.NumeroLicitacao = propostaAntesEnvioLegado.DadosGarantia.NumeroLicitacao;
                propostaRetornadaLegado.DadosGarantia.ValorMaximoDepositoRecursal = propostaAntesEnvioLegado.DadosGarantia.ValorMaximoDepositoRecursal;
                propostaRetornadaLegado.DadosGarantia.ValorDepositoRecursal = propostaAntesEnvioLegado.DadosGarantia.ValorDepositoRecursal;
                propostaRetornadaLegado.DadosGarantia.PossuiAgravo = propostaAntesEnvioLegado.DadosGarantia.PossuiAgravo;
                propostaRetornadaLegado.DadosGarantia.PercentualAgravo = propostaAntesEnvioLegado.DadosGarantia.PercentualAgravo;
                if (propostaRetornadaLegado.TipoSeguro == TipoSeguroEnum.Recursal) {
                    propostaRetornadaLegado.DadosGarantia.ValorImportanciaSeguradaRecursal = propostaRetornadaLegado.DadosGarantia.ValorImportanciaSegurada;
                    propostaRetornadaLegado.DadosGarantia.ValorImportanciaSegurada = null;
                } else if (propostaRetornadaLegado.TipoSeguro == TipoSeguroEnum.JudicialCivel ||
                    propostaRetornadaLegado.TipoSeguro == TipoSeguroEnum.JudicialTrabalhista ||
                    propostaRetornadaLegado.TipoSeguro == TipoSeguroEnum.JudicialFiscal) {
                    propostaRetornadaLegado.DadosGarantia.ValorImportanciaSeguradaJudicial = propostaRetornadaLegado.DadosGarantia.ValorImportanciaSegurada;
                    propostaRetornadaLegado.DadosGarantia.ValorImportanciaSegurada = null;
                }
                propostaRetornadaLegado.DadosGarantia.NumeroProcesso = propostaAntesEnvioLegado.DadosGarantia.NumeroProcesso;
                propostaRetornadaLegado.DadosGarantia.LegalRecourseType = propostaAntesEnvioLegado.DadosGarantia.LegalRecourseType;
                propostaRetornadaLegado.DadosGarantia.TermType = propostaAntesEnvioLegado.DadosGarantia.TermType;
                propostaRetornadaLegado.DadosGarantia.TipoDeSegurado = propostaAntesEnvioLegado.DadosGarantia.TipoDeSegurado;
                propostaRetornadaLegado.DadosGarantia.LaborCourt = propostaAntesEnvioLegado.DadosGarantia.LaborCourt;
                propostaRetornadaLegado.DadosGarantia.CivilCourt = propostaAntesEnvioLegado.DadosGarantia.CivilCourt;
                propostaRetornadaLegado.DadosGarantia.ValorDiscussaoJudicial = propostaAntesEnvioLegado.DadosGarantia.ValorDiscussaoJudicial;
                propostaRetornadaLegado.DadosGarantia.TipoAcao = propostaAntesEnvioLegado.DadosGarantia.TipoAcao;
                propostaRetornadaLegado.DadosGarantia.DescricaoOutroTipoAcao = propostaAntesEnvioLegado.DadosGarantia.DescricaoOutroTipoAcao;
                propostaRetornadaLegado.DadosGarantia.NumeroAcao = propostaAntesEnvioLegado.DadosGarantia.NumeroAcao;
                propostaRetornadaLegado.DadosGarantia.CpfCnpjJuizo = propostaAntesEnvioLegado.DadosGarantia.CpfCnpjJuizo;
                propostaRetornadaLegado.DadosGarantia.NomeJuizo = propostaAntesEnvioLegado.DadosGarantia.NomeJuizo;
                propostaRetornadaLegado.DadosGarantia.NumeroCDA = propostaAntesEnvioLegado.DadosGarantia.NumeroCDA;
                propostaRetornadaLegado.DadosGarantia.NumeroProcessoAdministrativo = propostaAntesEnvioLegado.DadosGarantia.NumeroProcessoAdministrativo;
                propostaRetornadaLegado.DadosGarantia.TipoTributo = propostaAntesEnvioLegado.DadosGarantia.TipoTributo;
                propostaRetornadaLegado.DadosGarantia.ComplementoTipoTributo = propostaAntesEnvioLegado.DadosGarantia.ComplementoTipoTributo;
                propostaRetornadaLegado.DadosGarantia.EmailSolicitante = propostaAntesEnvioLegado.DadosGarantia.EmailSolicitante;
                propostaRetornadaLegado.DadosGarantia.NomeSolicitante = propostaAntesEnvioLegado.DadosGarantia.NomeSolicitante;
                propostaRetornadaLegado.DadosGarantia.TelefoneSolicitante = propostaAntesEnvioLegado.DadosGarantia.TelefoneSolicitante;
                propostaRetornadaLegado.InsuredObject.InsuredObjectId = propostaAntesEnvioLegado.InsuredObject.InsuredObjectId;
                propostaRetornadaLegado.InsuredObject.Contents = propostaAntesEnvioLegado.InsuredObject.Contents;
                propostaRetornadaLegado.Clausulas = propostaAntesEnvioLegado.Clausulas;
            }
        }
    }
}
