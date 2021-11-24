using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Dao.Marketplace;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Marketplace.Implementations {
    public class ProposalDao : IProposalDao {
        private readonly IConfiguration configuration;

        public ProposalDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(ProposalEntity item, int commercialStructureId, int brokerId, int takerId, int insuredId, int insuredTypeId, int proposalTypeId, int productId, int coverageId,
            int proposalStatusId, string proposalStatusName, int insuredObjectId, int? legalRecourseTypeParameterId, int? recursalPolicyExpDateId, int? bureauId, int? registryBureauId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO Proposal ( CreatedDate, UpdatedDate, RemovedDate, UpdatedByThatUserId, CdRetorno, NmRetorno, NrProposta, IdApolice, ");
                sbSql.AppendLine("  CurrentStepNumber, Code, ProposalStatusId, BrokerId, CompanyTypeId, ProposalTypeId, ProductId, TakerId, CommercialStructureId, ");
                sbSql.AppendLine("  InsuredId, PolicyIniExpDate, PolicyExpDays, PolicyFinExpDate, WarrantyValue, ContractNumber, ProposalInsuredObjectId, Modality, ");
                sbSql.AppendLine("  NetPremium, TariffPremium, TotalPrize, IOF, AddingFractionation, PayMethodFirstInstallment, PayMethodInstallment, InstallmentExpDate, ");
                sbSql.AppendLine("  InstallmentForm, FormOtherPayInstallments, DayDueInstallments, LegalRecourseTypeParameterId, DepositAmount, Harm, ");
                sbSql.AppendLine("  AmountOfInsuredValue, RecursalPolicyExpDateId, StartVality, ProcessNumber, LaborCourtAndJusticeRegionId, LabourCourtId, ");
                sbSql.AppendLine("  InsuredTypeId, ProposalOtherBrokersId, ProposalParcId, AverageMixId, AverageMixTipoAplicacao, I4proProposalStatus, ");
                sbSql.AppendLine("  AverageMixPercentual, ComissaoOriginal, PremioOriginal, TypeInsuredProposal)");
                sbSql.AppendLine("VALUES ( @CreatedDate, @UpdatedDate, @RemovedDate, @UpdatedByThatUserId, @CdRetorno, @NmRetorno, @NrProposta, @IdApolice, ");
                sbSql.AppendLine("  @CurrentStepNumber, @Code, @ProposalStatusId, @BrokerId, @CompanyTypeId, @ProposalTypeId, @ProductId, @TakerId, @CommercialStructureId, ");
                sbSql.AppendLine("  @InsuredId, @PolicyIniExpDate, @PolicyExpDays, @PolicyFinExpDate, @WarrantyValue, @ContractNumber, @ProposalInsuredObjectId, @Modality, ");
                sbSql.AppendLine("  @NetPremium, @TariffPremium, @TotalPrize, @IOF, @AddingFractionation, @PayMethodFirstInstallment, @PayMethodInstallment, @InstallmentExpDate, ");
                sbSql.AppendLine("  @InstallmentForm, @FormOtherPayInstallments, @DayDueInstallments, @LegalRecourseTypeParameterId, @DepositAmount, @Harm, ");
                sbSql.AppendLine("  @AmountOfInsuredValue, @RecursalPolicyExpDateId, @StartVality, @ProcessNumber, @LaborCourtAndJusticeRegionId, @LabourCourtId, ");
                sbSql.AppendLine("  @InsuredTypeId, @ProposalOtherBrokersId, @ProposalParcId, @AverageMixId, @AverageMixTipoAplicacao, @I4proProposalStatus, ");
                sbSql.AppendLine("  @AverageMixPercentual, @ComissaoOriginal, @PremioOriginal, @TypeInsuredProposal)");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

                var proposalId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    CreatedDate = DateTime.Now,                 //--Fixo
                    UpdatedDate = (DateTime?)null,              //--Fixo
                    RemovedDate = (DateTime?)null,              //--Fixo
                    UpdatedByThatUserId = item.UsuarioInclusao.CodigoUsuario,
                    CdRetorno = item.CdRetorno,
                    NmRetorno = item.NmRetorno,
                    NrProposta = item.CodigoProposta,
                    IdApolice = item.IdApolice,
                    CurrentStepNumber = 4,                      //--Fixo
                    Code = item.CodigoProposta,                 //--Repete o número da proposta
                    ProposalStatusId = proposalStatusId,
                    BrokerId = brokerId,
                    CompanyTypeId = (item.Taker.TipoPessoa == TipoPessoaEnum.OrgãoPublico ? 1 : 2),  //--Fixo: 1 = público, 2 = privado
                    ProposalTypeId = proposalTypeId,
                    ProductId = productId,
                    TakerId = takerId,
                    CommercialStructureId = commercialStructureId,
                    InsuredId = insuredId,
                    PolicyIniExpDate = item.DataInicioVigencia,
                    PolicyExpDays = item.DiasPrazoVigencia,
                    PolicyFinExpDate = item.DataFimVigencia,
                    WarrantyValue = item.DadosGarantia.ValorImportanciaSegurada ?? item.DadosGarantia.ValorImportanciaSeguradaRecursal ?? item.DadosGarantia.ValorImportanciaSeguradaJudicial,
                    ContractNumber = item.DadosGarantia.NumeroLicitacao,
                    ProposalInsuredObjectId = insuredObjectId,
                    Modality = coverageId,
                    NetPremium = item.DadosGarantia.ValorPremioTarifario,
                    TariffPremium = item.DadosGarantia.ValorPremioTarifario,
                    TotalPrize = item.DadosGarantia.ValorPremioTotal,
                    IOF = 0M,                                   //--Fixo
                    AddingFractionation = item.DadosGarantia.ValorAdicionalFracionamento,

                    PayMethodFirstInstallment = item.DadosCobranca.Parcelamento.IdParcelamento,
                    PayMethodInstallment = item.DadosCobranca.PeridiocidadePagamento.IdPeridiocidadePagamento,
                    InstallmentExpDate = item.DadosCobranca.DataVencimentoPrimeiraParcela,
                    InstallmentForm = item.DadosCobranca.FormaPagamentoPrimeiraParcela.CodigoFormaPagamento,
                    FormOtherPayInstallments = (item.DadosCobranca.Parcelamento.QuantidadeParcelas > 1 ? item.DadosCobranca.FormaPagamentoDemaisParcelas.CodigoFormaPagamento : (int?)null),
                    DayDueInstallments = (item.DadosCobranca.Parcelamento.QuantidadeParcelas > 1 ? item.DadosCobranca.DiaCobranca: (int?)null),

                    LegalRecourseTypeParameterId = legalRecourseTypeParameterId,
                    DepositAmount = 0,                          //--Fixo
                    Harm = item.DadosGarantia.PercentualAgravo ?? 0,
                    AmountOfInsuredValue = 0,                   //--Fixo
                    RecursalPolicyExpDateId = recursalPolicyExpDateId,
                    StartVality = (DateTime?)null,              //--Fixo
                    ProcessNumber = item.DadosGarantia.NumeroProcesso,
                    LaborCourtAndJusticeRegionId = bureauId,
                    LabourCourtId = registryBureauId,
                    InsuredTypeId = insuredTypeId,
                    ProposalOtherBrokersId = (int?)null,        //--Fixo
                    ProposalParcId = (int?)null,                //--Fixo
                    AverageMixId = (int?)null,                  //--Fixo
                    AverageMixTipoAplicacao = (int?)null,       //--Fixo
                    I4proProposalStatus = proposalStatusName,
                    AverageMixPercentual = (decimal?)null,      //--Fixo
                    ComissaoOriginal = (decimal?)null,          //--Fixo
                    PremioOriginal = (decimal?)null,            //--Fixo
                    TypeInsuredProposal = (item.TipoSeguro == TipoSeguroEnum.Recursal ? "Reclamante" : "")
                });

                return proposalId;

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados da proposta no Marketplace", e);
            }
        }

        public async Task<int> GetAsync(int proposalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ProposalId");
                sbSql.AppendLine("FROM Proposal ");
                sbSql.AppendLine("WHERE ProposalCode = @ProposalCode");
                var id = await connection.QueryFirstAsync<int>(sbSql.ToString(), new {
                    ProposalCode = proposalCode
                });
                return id;
            } catch (Exception e) {
                throw new DaoException("Erro obtendo proposta no Marketplace", e);
            }
        }

        public async Task UpdateStatusAsync(int proposalCode, ProposalStatusEntity statusCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE Proposal");
                sbSql.AppendLine("SET ProposalStatusId = @ProposalStatusId, I4proProposalStatus = @ProposalStatusName, UpdatedDate = getdate() ");
                sbSql.AppendLine("WHERE NrProposta = @ProposalCode");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ProposalStatusId = statusCode.Id,
                    ProposalStatusName = statusCode.Name,
                    ProposalCode = proposalCode
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualizando status da proposta no Marketplace", e);
            }
        }

        public async Task UpdatePolicyAsync(int proposalCode, long policyNumber, ProposalStatusEntity statusCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE Proposal");
                sbSql.AppendLine("SET IdApolice = @PolicyNumber, ProposalStatusId = @ProposalStatusId, I4proProposalStatus = @ProposalStatusName, UpdatedDate = getdate() ");
                sbSql.AppendLine("WHERE NrProposta = @ProposalCode");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    PolicyNumber = policyNumber,
                    ProposalStatusId = statusCode.Id,
                    ProposalStatusName = statusCode.Name,
                    ProposalCode = proposalCode
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualizando dados da apólice da proposta", e);
            }
        }
    }
}
