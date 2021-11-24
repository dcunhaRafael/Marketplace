
using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class PolicyRenovationRepository : BaseRepository, IPolicyRenovationRepository {

        public PolicyRenovationRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) {

        }

        public async Task<IList<PolicyRenovation>> ListAsync(PolicyRenovation filters) {
            var query = new StringBuilder()
                .AppendLine("SELECT a.*, ")
                .AppendLine("	CAST(b.NrCnpjCpf AS BIGINT) AS BrokerDocument, b.NmPessoa as BrokerName,")
                .AppendLine("	CAST(t.NrCnpjCpf AS BIGINT) AS TakerDocument, t.NmPessoa as TakerName, CONVERT(decimal(18,2), t.VlLimCredito) as TakerCreditLimit, t.CdClasseRisco as TakerRating, CONVERT(decimal(18,8), t.VlTaxa) as TakerRiskRate,")
                .AppendLine("	CAST(i.NrCnpjCpf AS BIGINT) AS InsuredDocument, i.NmPessoa as InsuredName,")
                .AppendLine("	p.NrProposta AS NewProposalNumber, ps.Description as NewProposalStatusName, pr.NmProduto AS ProductName, cv.NmCobertura AS CoverageName")
                .AppendLine("FROM PolicyRenovation a")
                .AppendLine("LEFT JOIN Broker b ON b.IdPessoa = a.BrokerExternalId")
                .AppendLine("LEFT JOIN Taker t ON t.IdPessoa = a.TakerExternalId")
                .AppendLine("LEFT JOIN Insured i ON i.IdPessoa = a.InsuredExternalId")
                .AppendLine("LEFT JOIN Proposal p ON p.Id = a.ProposalId")
                .AppendLine("LEFT JOIN ProposalStatus ps ON ps.Id = p.ProposalStatusId")
                .AppendLine("LEFT JOIN Product pr ON pr.CdProduto = a.ProductExternalId")
                .AppendLine("LEFT JOIN Coverage cv ON cv.IdProdutoCobertura = a.CoverageExternalId")
                .AppendLine("WHERE (@PolicyBatchId IS NULL OR a.PolicyBatchId = @PolicyBatchId)")
                .AppendLine("AND (@BrokerExternalId IS NULL OR a.BrokerExternalId = @BrokerExternalId)")
                .AppendLine("AND (@TakerExternalId IS NULL OR a.TakerExternalId = @TakerExternalId)")
                .AppendLine("AND (@InsuredExternalId IS NULL OR a.InsuredExternalId = @InsuredExternalId)")
                .AppendLine("ORDER BY a.PolicyRenovationId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyRenovation>(
                query.ToString(),
                param: new {
                    filters.PolicyBatchId,
                    filters.BrokerExternalId,
                    filters.TakerExternalId,
                    filters.InsuredExternalId
                });

            return entity.ToList();
        }

        public async Task<PolicyRenovation> GetAsync(int policyRenovationId) {
            var query = new StringBuilder()
                .AppendLine("SELECT a.*, ")
                .AppendLine("	CAST(b.NrCnpjCpf AS BIGINT) AS BrokerDocument, b.NmPessoa as BrokerName,")
                .AppendLine("	CAST(t.NrCnpjCpf AS BIGINT) AS TakerDocument, t.NmPessoa as TakerName, CONVERT(decimal(18,2), t.VlLimCredito) as TakerCreditLimit, t.CdClasseRisco as TakerRating, CONVERT(decimal(18,8), t.VlTaxa) as TakerRiskRate,")
                .AppendLine("	CAST(i.NrCnpjCpf AS BIGINT) AS InsuredDocument, i.NmPessoa as InsuredName,")
                .AppendLine("	p.NrProposta AS NewProposalNumber, ps.Description as NewProposalStatusName, pr.NmProduto AS ProductName, cv.NmCobertura AS CoverageName")
                .AppendLine("FROM PolicyRenovation a")
                .AppendLine("LEFT JOIN Broker b ON b.IdPessoa = a.BrokerExternalId")
                .AppendLine("LEFT JOIN Taker t ON t.IdPessoa = a.TakerExternalId")
                .AppendLine("LEFT JOIN Insured i ON i.IdPessoa = a.InsuredExternalId")
                .AppendLine("LEFT JOIN Proposal p ON p.Id = a.ProposalId")
                .AppendLine("LEFT JOIN ProposalStatus ps ON ps.Id = p.ProposalStatusId")
                .AppendLine("LEFT JOIN Product pr ON pr.CdProduto = a.ProductExternalId")
                .AppendLine("LEFT JOIN Coverage cv ON cv.IdProdutoCobertura = a.CoverageExternalId")
                .AppendLine("WHERE a.PolicyRenovationId = @PolicyRenovationId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyRenovation>(
                query.ToString(),
                param: new {
                    PolicyRenovationId = policyRenovationId
                });

            return entity.FirstOrDefault();
        }

        public async Task<int> AddAsync(PolicyRenovation item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO PolicyRenovation ( PolicyBatchId, PolicyCode, ProductExternalId, CoverageExternalId, StartOfTerm, EndOfTerm, BrokerExternalId, TakerExternalId, InsuredExternalId, InsuredObject, ")
                .AppendLine("   InsuredAmount, RenovationUpdateIndexId, NewInsuredAmount, NewPremiumValue, NewStartOfTerm, NewEndOfTerm, NewProposalNumber, NewProposalStatusId, NewPolicyCode, NewInsuredObject, RenovationStatusId )")
                .AppendLine("  VALUES ( @PolicyBatchId, @PolicyCode, @ProductExternalId, @CoverageExternalId, @StartOfTerm, @EndOfTerm, @BrokerExternalId, @TakerExternalId, @InsuredExternalId, @InsuredObject, @InsuredAmount, ")
                .AppendLine("   @RenovationUpdateIndexId, @NewInsuredAmount, @NewPremiumValue, @NewStartOfTerm, @NewEndOfTerm, @NewProposalNumber, @NewProposalStatusId, @NewPolicyCode, @NewInsuredObject, @RenovationStatusId )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.PolicyBatchId,
                                                          item.PolicyCode,
                                                          item.ProductExternalId,
                                                          item.CoverageExternalId,
                                                          item.StartOfTerm,
                                                          item.EndOfTerm,
                                                          item.BrokerExternalId,
                                                          item.TakerExternalId,
                                                          item.InsuredExternalId,
                                                          item.InsuredObject,
                                                          item.InsuredAmount,
                                                          item.RenovationUpdateIndexId,
                                                          item.NewInsuredAmount,
                                                          item.NewPremiumValue,
                                                          item.NewStartOfTerm,
                                                          item.NewEndOfTerm,
                                                          item.NewProposalNumber,
                                                          item.NewProposalStatusId,
                                                          item.NewPolicyCode,
                                                          item.NewInsuredObject,
                                                          item.RenovationStatusId,
                                                      });

            return id.First();
        }

        public async Task UpdateAsync(PolicyRenovation item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatch")
                .AppendLine("SET NewInsuredAmount = @NewInsuredAmount, ")
                .AppendLine("   NewPremiumValue = @NewPremiumValue, ")
                .AppendLine("   NewStartOfTerm = @NewStartOfTerm, ")
                .AppendLine("   NewEndOfTerm = @NewEndOfTerm, ")
                .AppendLine("   NewProposalNumber = @NewProposalNumber, ")
                .AppendLine("   NewProposalStatusId = @NewProposalStatusId, ")
                .AppendLine("   NewPolicyCode = @NewPolicyCode, ")
                .AppendLine("   NewInsuredObject = @NewInsuredObject, ")
                .AppendLine("   RenovationUpdateIndexId = @RenovationUpdateIndexId, ")
                .AppendLine("   RenovationStatusId = @RenovationStatusId, ")
                .AppendLine("   ProposalId = @ProposalId, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate ")
                .AppendLine("WHERE PolicyRenovationId = @PolicyRenovationId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.NewInsuredAmount,
                    item.NewPremiumValue,
                    item.NewStartOfTerm,
                    item.NewEndOfTerm,
                    item.NewProposalNumber,
                    item.NewProposalStatusId,
                    item.NewPolicyCode,
                    item.NewInsuredObject,
                    item.RenovationUpdateIndexId,
                    item.RenovationStatusId,
                    item.ProposalId,
                    item.LastChangeUserId,
                    item.LastChangeDate,
                    item.PolicyRenovationId,
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task<IList<PolicyRenovation>> ListProposalCreationPendingAsync() {
            var query = new StringBuilder()
                .AppendLine("SELECT a.*, CAST(c.NrCnpjCpf AS BIGINT) AS BrokerDocument, d.CodigoSerieBC AS RenovationUpdateIndexBcCode")
                .AppendLine("FROM PolicyRenovation a")
                .AppendLine("INNER JOIN PolicyBatch b ON b.PolicyBatchId = a.PolicyBatchId AND b.Status = 1")
                .AppendLine("INNER JOIN Broker c ON c.IdPessoa = a.BrokerExternalId")
                .AppendLine("LEFT JOIN IndiceDeAtualizacao d ON d.Id = a.RenovationUpdateIndexId")
                .AppendLine("WHERE d.CodigoSerieBC IS NOT NULL")
                .AppendLine("AND a.ProposalId IS NULL");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyRenovation>(query.ToString());

            return entity.ToList();
        }

    }
}
