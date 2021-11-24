
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
    public class PolicyBatchRepository : BaseRepository, IPolicyBatchRepository {

        public PolicyBatchRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) {

        }

        public async Task<IList<PolicyBatch>> ListAsync(PolicyBatch filters) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM PolicyBatch")
                .AppendLine("WHERE (@PolicyBatchId IS NULL OR PolicyBatchId = @PolicyBatchId)")
                .AppendLine("AND (@BatchType IS NULL OR BatchType = @BatchType)")
                .AppendLine("AND (@Competency IS NULL OR Competency = @Competency)")
                .AppendLine("AND (@BatchStatus IS NULL OR BatchStatus = @BatchStatus)")
                .AppendLine("AND (@Status IS NULL OR Status = @Status)")
                .AppendLine("AND (@BrokerExternalId IS NULL OR BrokerExternalId = @BrokerExternalId)")
                .AppendLine("AND (@TakerExternalId IS NULL OR TakerExternalId = @TakerExternalId)")
                .AppendLine("AND (@InsuredExternalId IS NULL OR InsuredExternalId = @InsuredExternalId)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatch>(
                query.ToString(),
                param: new {
                    filters.PolicyBatchId,
                    filters.BatchType,
                    filters.Competency,
                    filters.BatchStatus,
                    filters.Status,
                    filters.BrokerExternalId,
                    filters.TakerExternalId,
                    filters.InsuredExternalId
                });

            return entity.ToList();
        }

        public async Task<IList<PolicyBatch>> ListNewAsync(PolicyBatchConfiguration filters) {
            var groupCount = 0;
            if (filters.GroupByBroker) groupCount++;
            if (filters.GroupByTaker) groupCount++;
            if (filters.GroupByInsured) groupCount++;

            var query = new StringBuilder()
                .AppendLine("SELECT ")
                .Append(filters.GroupByBroker ? "id_pessoa_corretor_lider" : "0").AppendLine(" as BrokerExternalId,")
                .Append(filters.GroupByTaker ? "id_pessoa_tomador" : "0").AppendLine(" as TakerExternalId,")
                .Append(filters.GroupByInsured ? "id_pessoa_segurado" : "0").AppendLine(" as InsuredExternalId,")
                .AppendLine("cast(year(getdate()) as varchar) + '/' + right('0' + cast(month(getdate()) as varchar), 2) as Competency,")
                .AppendLine("COUNT(cd_apolice) as PolicyCount,")
                .AppendLine("SUM(vl_is) as PolicyTotal")
                .AppendLine("FROM [Stage].[dbo].[hub_apolices_judiciais_homolog]")
                .AppendLine("WHERE validade_dias <= @ProcessDays")
                .AppendLine("AND loteid is null")
                .AppendLine("AND data_processamento  is null")
                .AppendLine("GROUP BY")
                .Append(filters.GroupByBroker ? "id_pessoa_corretor_lider" : "").AppendLine(groupCount > 1 ? "," : "")
                .Append(filters.GroupByTaker ? "id_pessoa_tomador" : "").AppendLine(groupCount > 1 ? "," : "")
                .Append(filters.GroupByInsured ? "id_pessoa_segurado" : "");

            using var dbConn = base.GetStageDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatch>(
                query.ToString(),
                param: new {
                    filters.ProcessDays
                });

            return entity.ToList();
        }

        public async Task<PolicyBatch> GetAsync(int policyBatchId) {
            var query = new StringBuilder()
                .AppendLine("SELECT *")
                .AppendLine("FROM PolicyBatch")
                .AppendLine("WHERE PolicyBatchId = @PolicyBatchId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatch>(
                query.ToString(),
                param: new {
                    PolicyBatchId = policyBatchId
                });

            return entity.FirstOrDefault();
        }

        public async Task<int> AddAsync(PolicyBatch item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO PolicyBatch ( BatchType, BrokerExternalId, TakerExternalId, InsuredExternalId, Competency, PolicyCount, PolicyTotal, BatchStatus, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @BatchType, @BrokerExternalId, @TakerExternalId, @InsuredExternalId, @Competency, @PolicyCount, @PolicyTotal, @BatchStatus, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.BatchType,
                                                          item.BrokerExternalId,
                                                          item.TakerExternalId,
                                                          item.InsuredExternalId,
                                                          item.Competency,
                                                          item.PolicyCount,
                                                          item.PolicyTotal,
                                                          item.BatchStatus,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });

            return id.First();
        }

        public async Task UpdateAsync(PolicyBatch item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatch")
                .AppendLine("SET BatchType = @BatchType, ")
                .AppendLine("   BrokerExternalId = @BrokerExternalId, ")
                .AppendLine("   TakerExternalId = @TakerExternalId, ")
                .AppendLine("   InsuredExternalId = @InsuredExternalId, ")
                .AppendLine("   Competency = @Competency, ")
                .AppendLine("   PolicyCount = @PolicyCount, ")
                .AppendLine("   PolicyTotal = @PolicyTotal, ")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchId = @PolicyBatchId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchId,
                    item.BatchType,
                    item.BrokerExternalId,
                    item.TakerExternalId,
                    item.InsuredExternalId,
                    item.Competency,
                    item.PolicyCount,
                    item.PolicyTotal,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task UpdateStatusAsync(PolicyBatch item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE PolicyBatch")
                .AppendLine("SET Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE PolicyBatchId = @PolicyBatchId");

            using var dbConn = GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task LinkBatchPoliciesAsync(PolicyBatch item, int processDays) {
            var query = new StringBuilder()
                .AppendLine("UPDATE [Stage].[dbo].[hub_apolices_judiciais_homolog]")
                .AppendLine("SET loteid = @PolicyBatchId,")
                .AppendLine("   data_processamento  = @InclusionDate")
                .AppendLine("WHERE 1 = 1")
                .AppendLine("and (@BrokerExternalId = 0 OR id_pessoa_corretor_lider = @BrokerExternalId)")
                .AppendLine("and (@TakerExternalId = 0 OR id_pessoa_tomador = @TakerExternalId)")
                .AppendLine("and (@InsuredExternalId = 0 OR id_pessoa_segurado = @InsuredExternalId)")
                .AppendLine("and validade_dias <= @ProcessDays")
                .AppendLine("and loteid is null")
                .AppendLine("and data_processamento  is null");

            using var dbConn = GetStageDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.PolicyBatchId,
                    item.InclusionDate,
                    item.BrokerExternalId,
                    item.TakerExternalId,
                    item.InsuredExternalId,
                    ProcessDays = processDays
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
            }
        }

        public async Task<IList<PolicyBatchItem>> ListItemsAsync(int policyBatchId, int? expirationDays) {
            var query = new StringBuilder()
                .AppendLine("SELECT ")
                .AppendLine("   a.id_endosso as EndorsementId,")
                .AppendLine("   a.cd_apolice as PolicyCode,")
                .AppendLine("   a.nm_tp_origem as SourceTypeName,")
                .AppendLine("   a.nr_endosso as EndorsementNumber,")
                .AppendLine("   a.dt_emissao as IssueDate,")
                .AppendLine("   a.dt_inicio_vigencia as StartOfTerm,")
                .AppendLine("   a.dt_fim_vigencia as EndOfTerm,")
                .AppendLine("   a.vl_is as InsuredAmount,")
                .AppendLine("   a.id_pessoa_corretor_lider as BrokerExternalId,")
                .AppendLine("   a.nm_corretor_lider as BrokerName,")
                .AppendLine("   a.nr_cpf_cnpj_corretor_lider as BrokerDocument,")
                .AppendLine("   b.nm_meio_comunicacao as BrokerEmails,")
                .AppendLine("   a.id_pessoa_segurado as InsuredExternalId,")
                .AppendLine("   a.nm_segurado as InsuredName,")
                .AppendLine("   a.nr_cpf_cnpj_segurado as InsuredDocument,")
                .AppendLine("   c.nm_meio_comunicacao as InsuredEmails,")
                .AppendLine("   a.cd_produto as ProductExternalId,")
                .AppendLine("   a.nm_produto as ProductName,")
                .AppendLine("   a.id_produto_cobertura as CoverageExternalId,")
                .AppendLine("   a.Nm_Cobertura as CoverageName,")
                .AppendLine("   a.validade_dias as ExpirationDays,")
                .AppendLine("   a.id_pessoa_tomador as TakerExternalId,")
                .AppendLine("   a.nm_tomador as TakerName,")
                .AppendLine("   a.nr_cpf_cnpj_tomador as TakerDocument,")
                .AppendLine("   d.nm_meio_comunicacao as TakerEmails,")
                .AppendLine("   (SELECT Id FROM IndiceDeAtualizacao WHERE Nome = a.indice) as RenovationUpdateIndexId,")
                .AppendLine("   a.data_processamento as ProcessingDate,")
                .AppendLine("   a.texto_livre as InsuredObject,")
                .AppendLine("   a.loteid as PolicyBatchId")
                .AppendLine("FROM hub_apolices_judiciais_homolog a")
                .AppendLine("LEFT JOIN contato_email b on b.id_pessoa = a.id_pessoa_corretor_lider")
                .AppendLine("LEFT JOIN contato_email c on c.id_pessoa = a.id_pessoa_segurado")
                .AppendLine("LEFT JOIN contato_email d on d.id_pessoa = a.id_pessoa_tomador")
                .AppendLine("WHERE a.loteid = @PolicyBatchId")
                .AppendLine("AND (@ExpirationDays IS NULL OR a.validade_dias = @ExpirationDays)");

            using var dbConn = base.GetStageDbConnection();
            var entity = await dbConn.QueryAsync<PolicyBatchItem>(
                query.ToString(),
                param: new {
                    PolicyBatchId = policyBatchId,
                    ExpirationDays = expirationDays
                });

            return entity.ToList();
        }
    }
}
