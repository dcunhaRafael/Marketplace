using Dapper;
using Domain.Entities;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Util.Extensions;
using System;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class ProposalRepository : BaseRepository, IProposalRepository {

        public ProposalRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<Proposal> GetAsync(int proposalNumber) {
            var query = new StringBuilder()
                .AppendLine("SELECT p.Id as ProposalId, p.NrProposta as ProposalNumber, p.StartVality as ProposalDate, p.PolicyIniExpDate as EffectiveDateStart, p.PolicyFinExpDate as EffectiveDateEnd, p.WarrantyValue as InsuredAmount,")
                .AppendLine("   pt.Id as ProposalTypeId, pt.Description as ProposalTypeName,")
                .AppendLine("   prd.ProductId, prd.NmProduto as ProductName,")
                .AppendLine("   cov.CoverageId, cov.NmCobertura as CoverageName,")
                .AppendLine("   b.BrokerId, b.NmPessoa as BrokerName, b.NrCnpjCpf as BrokerCpfCnpjNumber, b.IdPessoa as BrokerLegacyCode, b.IdUsuarioCorretor as BrokerLegacyUserId,")
                .AppendLine("   t.Id as TakerId, t.NmPessoa as TakerName, t.NrCnpjCpf as TakerCpfCnpjNumber, t.IdPessoa as TakerLegacyCode, ")
                .AppendLine("   i.InsuredId, i.NmPessoa as InsuredName, i.NrCnpjCpf as InsuredCpfCnpjNumber")
                .AppendLine("FROM Proposal p")
                .AppendLine("INNER JOIN ProposalType pt on pt.Id = p.ProposalTypeId")
                .AppendLine("INNER JOIN Product prd on prd.CdProduto = pt.CdProduto")
                .AppendLine("INNER JOIN Coverage cov on cov.IdProdutoCobertura = pt.IdProdutoCobertura")
                .AppendLine("INNER JOIN Broker b on b.BrokerId = p.BrokerId")
                .AppendLine("INNER JOIN Taker t on t.Id = p.TakerId")
                .AppendLine("INNER JOIN Insured i on i.InsuredId = p.InsuredId")
                .AppendLine("WHERE p.NrProposta = @ProposalNumber");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<dynamic>(
                query.ToString(),
                param: new {
                    ProposalNumber = proposalNumber,
                }
            );

            return entity.Select(x => new Proposal() {
                ProposalId = x.ProposalId,
                ProposalNumber = x.ProposalCode,
                ProposalDate = x.ProposalDate,
                EffectiveDateStart = x.EffectiveDateStart,
                EffectiveDateEnd = x.EffectiveDateEnd,
                InsuredAmount = x.InsuredAmount,
                ProposalType = new ProposalType() {
                    ProposalTypeId = x.ProposalTypeId,
                    Name = x.ProposalTypeName
                },
                Product = new Product() {
                    ProductId = x.ProductId,
                    Name = x.ProductName
                },
                Coverage = new Coverage() {
                    CoverageId = x.CoverageId,
                    Name = x.CoverageName
                },
                Broker = new Broker() {
                    BrokerId = x.BrokerId,
                    Name = x.BrokerName,
                    CpfCnpjNumber = CastExtensions.ToLong(x.BrokerCpfCnpjNumber) ?? 0,
                    LegacyCode = x.BrokerLegacyCode,
                    LegacyUserId = int.Parse(x.BrokerLegacyUserId)
                },
                Taker = new Taker() {
                    TakerId = x.TakerId,
                    Name = x.TakerName,
                    CpfCnpjNumber = CastExtensions.ToLong(x.TakerCpfCnpjNumber) ?? 0,
                    LegacyCode = x.TakerLegacyCode
                },
                Insured = new Insured() {
                    InsuredId = x.InsuredId,
                    Name = x.InsuredName,
                    CpfCnpjNumber = CastExtensions.ToLong(x.InsuredCpfCnpjNumber) ?? 0
                }
            }).FirstOrDefault();
        }

    }
}
