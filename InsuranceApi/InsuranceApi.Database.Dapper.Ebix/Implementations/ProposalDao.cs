using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class ProposalDao : IProposalDao {
        private readonly IConfiguration configuration;

        public ProposalDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task AddAsync(ProposalEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));                     //-- Proposta
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO Proposal( ProposalCode, PolicyId, EndorsementId, ProposalDate, ExpirationDate, EffectiveDateStart, EffectiveDateEnd,");
                sbSql.AppendLine("                      StatusCode, ProductId, SalesChannelId, BrokerId, ProducerId, TakerId, TakerAddressId, InsuredId, InsuredAddressId,");
                sbSql.AppendLine("                      InclusionUserId, ChangeUserId, InsuranceType)");
                sbSql.AppendLine("              VALUES( @ProposalCode, @PolicyId, @EndorsementId, @ProposalDate, @ExpirationDate, @EffectiveDateStart, @EffectiveDateEnd,");
                sbSql.AppendLine("                      @StatusCode, @ProductId, @SalesChannelId, @BrokerId, @ProducerId, @TakerId, @TakerAddressId, @InsuredId, @InsuredAddressId,");
                sbSql.AppendLine("                      @InclusionUserId, @ChangeUserId, @InsuranceType )");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

                var proposalId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    ProposalCode = item.CodigoProposta,
                    PolicyId = item.IdApolice,
                    EndorsementId = item.IdEndosso,
                    ProposalDate = item.DataProposta,
                    ExpirationDate = item.DataValidade,
                    EffectiveDateStart = item.DataInicioVigencia,
                    EffectiveDateEnd = item.DataFimVigencia,
                    StatusCode = item.StatusProposta,
                    ProductId = item.Product.CodigoProduto,
                    SalesChannelId = item.SalesChannel.SalesChannelId,
                    BrokerId = item.Broker.IdPessoa,
                    ProducerId = item.IdPessoaProdutor,
                    TakerId = item.Taker.IdPessoa,
                    TakerAddressId = item.Taker.Endereco.IdEndereco,
                    InsuredId = item.Insured.IdPessoa,
                    InsuredAddressId = item.Insured.Endereco.IdEndereco,
                    InclusionUserId = item.UsuarioInclusao.CodigoUsuario,
                    ChangeUserId = item.UsuarioAlteracao.CodigoUsuario,
                    InsuranceType = item.TipoSeguro
                });

                //-- Dados de Cobrança
                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalBillingData ( ProposalId, DueDateFirst, BillingDay, PaymentFormCodeFirst, PaymentFormCodeOthers, InstallmentId, FrequencyId )");
                sbSql.AppendLine("VALUES ( @ProposalId, @DueDateFirst, @BillingDay, @PaymentFormCodeFirst, @PaymentFormCodeOthers, @InstallmentId, @FrequencyId )");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ProposalId = proposalId,
                    DueDateFirst = item.DadosCobranca.DataVencimentoPrimeiraParcela,
                    BillingDay = item.DadosCobranca.DiaCobranca,
                    PaymentFormCodeFirst = item.DadosCobranca.FormaPagamentoPrimeiraParcela.CodigoFormaPagamento,
                    PaymentFormCodeOthers = item.DadosCobranca.FormaPagamentoDemaisParcelas.CodigoFormaPagamento,
                    InstallmentId = item.DadosCobranca.Parcelamento.IdParcelamento,
                    FrequencyId = item.DadosCobranca.PeridiocidadePagamento.IdPeridiocidadePagamento,
                });

                //-- Dados de Garantia
                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalGuaranteeData ( ProposalId, CoverageId, TariffPremiumValue, ComissionPercentage, CurrencyRateValue, AdditionalFractionationValue, ComissionValue,");
                sbSql.AppendLine("                                    InsuredAmountValue, RiskRatioValue, CurrencyCode, IOFValue, IOFPercentage, InsuredObjectCode, InsuredObjectText,  BiddingNumber,");
                sbSql.AppendLine("                                    LegalRecourseTypeId, ApeelDepositAmount, HasGrievance, GrievancePercentage, TermTypeId,");
                sbSql.AppendLine("                                    LegalCaseNumber, InsuredType, LaborCourtId, CivilCourtId, ");
                sbSql.AppendLine("                                    CreditorTypeId, CreditorStateId, CreditorLocalityId, JudicialDiscussionValue, LawsuitTypeId, LawsuitTypeDescription,");
                sbSql.AppendLine("                                     CourtDocumentNumber, CourtName, CdaNumber, AdministrativeCaseNumber, TributeTypeId,");
                sbSql.AppendLine("                                    TributeTypeComplement, RequesterEmail, RequesterName, RequesterPhoneNumber )");
                sbSql.AppendLine("                           VALUES ( @ProposalId, @CoverageId, @TariffPremiumValue, @ComissionPercentage, @CurrencyRateValue, @AdditionalFractionationValue, @ComissionValue,");
                sbSql.AppendLine("                                    @InsuredAmountValue, @RiskRatioValue, @CurrencyCode, @IOFValue, @IOFPercentage, @InsuredObjectCode, @InsuredObjectText,  @BiddingNumber,");
                sbSql.AppendLine("                                    @LegalRecourseTypeId, @ApeelDepositAmount, @HasGrievance, @GrievancePercentage, @TermTypeId,");
                sbSql.AppendLine("                                    @LegalCaseNumber, @InsuredType, @LaborCourtId, @CivilCourtId, ");
                sbSql.AppendLine("                                    @CreditorTypeId, @CreditorStateId, @CreditorLocalityId, @JudicialDiscussionValue, @LawsuitTypeId, @LawsuitTypeDescription,");
                sbSql.AppendLine("                                    @CourtDocumentNumber, @CourtName, @CdaNumber, @AdministrativeCaseNumber, @TributeTypeId,");
                sbSql.AppendLine("                                    @TributeTypeComplement, @RequesterEmail, @RequesterName, @RequesterPhoneNumber )");

                int? tipoSegurado = null;
                if (item.DadosGarantia.TipoDeSegurado != null) {
                    tipoSegurado = (int)item.DadosGarantia.TipoDeSegurado;
                }

                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ProposalId = proposalId,
                    CoverageId = item.DadosGarantia.Coverage.IdCobertura,
                    TariffPremiumValue = item.DadosGarantia.ValorPremioTarifario,
                    ComissionPercentage = item.DadosGarantia.PercentualComissao,
                    CurrencyRateValue = item.DadosGarantia.ValorTaxaMoeda,
                    AdditionalFractionationValue = item.DadosGarantia.ValorAdicionalFracionamento,
                    ComissionValue = item.DadosGarantia.ValorComissao,
                    InsuredAmountValue = item.DadosGarantia.ValorImportanciaSegurada ?? item.DadosGarantia.ValorImportanciaSeguradaRecursal ?? item.DadosGarantia.ValorImportanciaSeguradaJudicial,
                    RiskRatioValue = item.DadosGarantia.ValorTaxaRisco,
                    CurrencyCode = item.DadosGarantia.CodigoMoeda,
                    IOFValue = item.DadosGarantia.ValorIOF,
                    IOFPercentage = item.DadosGarantia.PercentualIOF,
                    InsuredObjectCode = item.InsuredObject.InsuredObjectId,
                    BiddingNumber = item.DadosGarantia.NumeroLicitacao,
                    InsuredObjectText = item.InsuredObject.Contents,
                    LegalRecourseTypeId = item.DadosGarantia.LegalRecourseType?.LegalRecourseTypeId,
                    ApeelDepositAmount = item.DadosGarantia.ValorDepositoRecursal,
                    HasGrievance = item.DadosGarantia.PossuiAgravo,
                    GrievancePercentage = item.DadosGarantia.PercentualAgravo,
                    TermTypeId = item.DadosGarantia.TermType?.Id,
                    LegalCaseNumber = item.DadosGarantia.NumeroProcesso,
                    InsuredType = tipoSegurado,
                    LaborCourtId = item.DadosGarantia.LaborCourt?.LaborCourtId,
                    CivilCourtId = item.DadosGarantia.CivilCourt?.CivilCourtId,
                    CreditorTypeId = item.DadosGarantia.TipoDeCredor?.CreditorTypeId,
                    CreditorStateId = item.DadosGarantia.EstadoDoCredor?.StateId,
                    CreditorLocalityId = item.DadosGarantia.MunicipioDoCredor?.LocalityId,
                    JudicialDiscussionValue = item.DadosGarantia.ValorDiscussaoJudicial,
                    LawsuitTypeId = item.DadosGarantia.TipoAcao?.LawsuitTypeId,
                    LawsuitTypeDescription = item.DadosGarantia.DescricaoOutroTipoAcao,
                    //LawsuitNumber = proposta.DadosGarantia.NumeroAcao,
                    CourtDocumentNumber = item.DadosGarantia.CpfCnpjJuizo,
                    CourtName = item.DadosGarantia.NomeJuizo,
                    CdaNumber = item.DadosGarantia.NumeroCDA,
                    AdministrativeCaseNumber = item.DadosGarantia.NumeroProcessoAdministrativo,
                    TributeTypeId = item.DadosGarantia.TipoTributo?.TributeTypeId,
                    TributeTypeComplement = item.DadosGarantia.ComplementoTipoTributo,
                    RequesterEmail = item.DadosGarantia.EmailSolicitante,
                    RequesterName = item.DadosGarantia.NomeSolicitante,
                    RequesterPhoneNumber = item.DadosGarantia.TelefoneSolicitante
                });

                //-- Reclamantes
                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalClaimant ( ProposalId, ClaimantName, ClaimantDocumentNumber, IsDefault )");
                sbSql.AppendLine("                      VALUES ( @ProposalId, @ClaimantName, @ClaimantDocumentNumber, @IsDefault )");

                foreach (var reclamante in item.DadosGarantia.Reclamantes) {
                    await connection.ExecuteAsync(sbSql.ToString(), new {
                        ProposalId = proposalId,
                        ClaimantName = reclamante.NomeReclamante,
                        ClaimantDocumentNumber = reclamante.CpfCnpjReclamante,
                        IsDefault = reclamante.IsPrincipal
                    });
                }

                //-- Parcelas
                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalInstallment ( ProposalId, Number, AdditionalFractionationValue, DueDate, IOFValue, TariffPremiumValue, TotalPremiumValue, CostValue, StatusCode )");
                sbSql.AppendLine("                         VALUES ( @ProposalId, @Number, @AdditionalFractionationValue, @DueDate, @IOFValue, @TariffPremiumValue, @TotalPremiumValue, @CostValue, @StatusCode )");
                foreach (var parcela in item.Parcelas) {
                    await connection.ExecuteAsync(sbSql.ToString(), new {
                        ProposalId = proposalId,
                        Number = parcela.NumeroParcela,
                        AdditionalFractionationValue = parcela.ValorAdicionalFracionamento,
                        DueDate = parcela.DataVencimento,
                        IOFValue = parcela.ValorIof,
                        TariffPremiumValue = parcela.ValorPremioTarifario,
                        TotalPremiumValue = parcela.ValorPremioTotal,
                        CostValue = parcela.ValorCusto,
                        StatusCode = parcela.StatusParcela.ToString()
                    });
                }

                //-- Comissionamento
                sbSql = new StringBuilder();
                sbSql.AppendLine("insert into ProposalCommissionDistribution (ProposalId, TypeOfComission, ProducerId, IsPrincipalProducer, ParticipationPercentage, [Print], ComissionValue, ProducerCpfCnpj, ProposalPercentage)");
                sbSql.AppendLine("values(@ProposalId, @TypeOfComission, @ProducerId, @IsPrincipalProducer, @ParticipationPercentage, @Print, @ComissionValue, @ProducerCpfCnpj, @ProposalPercentage)");
                var comissao = item.Comissao.FirstOrDefault();

                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ProposalId = proposalId,
                    TypeOfComission = comissao.TypeOfComission,
                    ProducerId = comissao.ProducerId,
                    IsPrincipalProducer = comissao.IsPrincipalProducer,
                    ParticipationPercentage = comissao.ParticipationPercentage,
                    Print = comissao.Print,
                    ComissionValue = comissao.ComissionValue,
                    ProducerCpfCnpj = comissao.ProducerCpfCnpj,
                    ProposalPercentage = comissao.ProposalPercentage

                });

                //-- Cláusulas
                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalClause ( ProposalId, ClauseProductCoverageId )");
                sbSql.AppendLine("  VALUES ( @ProposalId, @ClauseProductCoverageId )");
                foreach (var clausula in item.Clausulas) {
                    if (clausula.IsChecked) {
                        await connection.ExecuteAsync(sbSql.ToString(), new {
                            ProposalId = proposalId,
                            ClauseProductCoverageId = clausula.ClauseProductCoverageId
                        });
                    }
                }
            } catch (Exception e) {
                throw new DaoException("Erro gravando dados da proposta", e);
            }
        }

        public async Task<int> GetAsync(int proposalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ProposalId");
                sbSql.AppendLine("  FROM Proposal ");
                sbSql.AppendLine(" WHERE ProposalCode = @ProposalCode");
                var id = await connection.QueryFirstAsync<int>(sbSql.ToString(), new {
                    ProposalCode = proposalCode
                });
                return id;
            } catch (Exception e) {
                throw new DaoException("Erro obtendo proposta", e);
            }
        }

        public async Task UpdateStatusAsync(int proposalCode, int statusCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE Proposal");
                sbSql.AppendLine("   SET StatusCode = @StatusCode");
                sbSql.AppendLine(" WHERE ProposalCode = @ProposalCode");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    StatusCode = statusCode,
                    ProposalCode = proposalCode
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualizando status da proposta", e);
            }
        }
    }
}
