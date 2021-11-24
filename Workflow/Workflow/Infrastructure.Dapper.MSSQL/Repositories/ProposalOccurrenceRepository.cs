using Dapper;
using Domain.Entities;
using Domain.Enumerators;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dapper.MSSQL.Repositories {
    public class ProposalOccurrenceRepository : BaseRepository, IProposalOccurrenceRepository {

        public ProposalOccurrenceRepository(IDatabaseFactory databaseOptions) : base(databaseOptions) { }

        public async Task<IList<ProposalOccurrence>> ListAsync(Domain.Payload.ProposalOccurrenceFilters filters) {
            var query = new StringBuilder()
                .AppendLine("SELECT distinct po.ProposalOccurrenceId, po.ProposalId, po.OccurrenceStatus, po.ApprovalRefusalDate, po.UserComments, po.Status, po.InclusionDate, po.LastChangeDate,")
                .AppendLine("   (SELECT COUNT(1) ")
                .AppendLine("    FROM OccurrenceTypeDocument ")
                .AppendLine("    WHERE OccurrenceTypeId = po.OccurrenceTypeId AND Status = @Active) as DocumentTypeCount,")
                .AppendLine("   (SELECT COUNT(1)")
                .AppendLine("    FROM ProposalOccurrence a")
                .AppendLine("    INNER JOIN OccurrenceType b ON b.OccurrenceTypeId = a.OccurrenceTypeId")
                .AppendLine("    INNER JOIN OccurrenceTypeDocument c ON c.OccurrenceTypeId = b.OccurrenceTypeId AND c.Status = @Active AND c.IsRequired = @Required")
                .AppendLine("    LEFT JOIN ProposalOccurrenceDocument d ON d.ProposalOccurrenceId = a.ProposalOccurrenceId AND d.DocumentTypeId = c.DocumentTypeId AND d.Status = @Active")
                .AppendLine("    WHERE a.ProposalOccurrenceId = po.ProposalOccurrenceId")
                .AppendLine("    AND d.ProposalOccurrenceDocumentId IS NULL) as DocumentTypePendingCount,")
                .AppendLine("   p.Id as ProposalId, p.NrProposta as ProposalNumber, p.StartVality as ProposalDate, p.PolicyIniExpDate as EffectiveDateStart, p.PolicyFinExpDate as EffectiveDateEnd,")
                .AppendLine("   pt.Id as ProposalTypeId, pt.Description as ProposalTypeName,")
                .AppendLine("   prd.ProductId, prd.NmProduto as ProductName,")
                .AppendLine("   cov.CoverageId, cov.NmCobertura as CoverageName,")
                .AppendLine("   b.BrokerId, b.NmPessoa as BrokerName, b.NrCnpjCpf as BrokerCpfCnpjNumber,")
                .AppendLine("   t.Id as TakerId, t.NmPessoa as TakerName, t.NrCnpjCpf as TakerCpfCnpjNumber,")
                .AppendLine("   i.InsuredId, i.NmPessoa as InsuredName, i.NrCnpjCpf as InsuredCpfCnpjNumber,")
                .AppendLine("   ot.OccurrenceTypeId, ot.OccurrenceCode, ot.Name as OccurrenceName, ot.Description as OccurrenceDescription, ot.Type, ot.ValidationRule, ot.IsTransmissionLocked, ot.IsIssuanceLocked, ot.RequiredAction, ot.AutomaticRefusal, ot.ProfileId, ot.NormalSignalingTimeout, ot.WarningSignalingTimeout, ot.CriticalSignalingTimeout,")
                .AppendLine("   rr.RefusalReasonId, rr.Name as RefusalReasonName,")
                .AppendLine("   pfl.Id as ProfileId, pfl.Description as ProfileName,")
                .AppendLine("   ui.Id as InclusionUserId, RTRIM(LTRIM(ui.Name)) as InclusionUserName, ui.Email as InclusionUserEmail,")
                .AppendLine("   uc.Id as LastChangeUserId, RTRIM(LTRIM(uc.Name)) as LastChangeUserName, uc.Email as LastChangeUserEmail")
                .AppendLine("FROM ProposalOccurrence po")
                .AppendLine("INNER JOIN Proposal p ON p.Id = po.ProposalId")
                .AppendLine("INNER JOIN ProposalType pt on pt.Id = p.ProposalTypeId")
                .AppendLine("INNER JOIN Product prd on prd.CdProduto = pt.CdProduto")
                .AppendLine("INNER JOIN Coverage cov on cov.IdProdutoCobertura = pt.IdProdutoCobertura")
                .AppendLine("INNER JOIN Broker b on b.BrokerId = p.BrokerId")
                .AppendLine("INNER JOIN Taker t on t.Id = p.TakerId")
                .AppendLine("INNER JOIN Insured i on i.InsuredId = p.InsuredId")
                .AppendLine("INNER JOIN OccurrenceType ot on ot.OccurrenceTypeId = po.OccurrenceTypeId")
                .AppendLine("LEFT JOIN RefusalReason rr on rr.RefusalReasonId = po.RefusalReasonId")
                .AppendLine("INNER JOIN Profile pfl on pfl.Id = ot.ProfileId")
                .AppendLine("INNER JOIN Users ui on ui.Id = po.InclusionUserId")
                .AppendLine("LEFT JOIN Users uc on uc.Id = po.LastChangeUserId")
                .AppendLine("LEFT JOIN OccurrenceTypeLiberationUser otlu ON otlu.OccurrenceTypeId = po.OccurrenceTypeId AND otlu.UserId = COALESCE(@LoggedUserId, otlu.UserId) AND otlu.Status = @Active")
                .AppendLine("WHERE (@ProposalOccurrenceId IS NULL OR po.ProposalOccurrenceId = @ProposalOccurrenceId)")
                .AppendLine("AND (@LoggedUserId IS NULL OR (otlu.UserId IS NOT NULL OR")    //--sem usuário ou... tem liberador específico ou tem perfil
                .AppendLine("     ((SELECT COUNT(1) FROM OccurrenceTypeLiberationUser WHERE OccurrenceTypeId = po.OccurrenceTypeId) = 0 AND ot.ProfileId = @LoggedUserProfileId)))")
                .AppendLine("AND (@ProductId IS NULL OR prd.ProductId = @ProductId)")
                .AppendLine("AND (@CoverageId IS NULL OR cov.CoverageId = @CoverageId)")
                .AppendLine("AND (@ProposalNumber IS NULL OR p.NrProposta = @ProposalNumber)")
                .AppendLine("AND (@DateFrom IS NULL OR po.InclusionDate >= @DateFrom)")
                .AppendLine("AND (@DateTo IS NULL OR po.InclusionDate <= @DateTo)")
                .AppendLine("AND (@OccurrenceStatus IS NULL OR po.OccurrenceStatus = @OccurrenceStatus)")
                .AppendLine("AND (@OccurrenceTypeId IS NULL OR ot.OccurrenceTypeId = @OccurrenceTypeId)")
                .AppendLine("AND (@BrokerId IS NULL OR p.BrokerId = @BrokerId)")
                .AppendLine("AND (@TakerId IS NULL OR p.TakerId = @TakerId)")
                .AppendLine("AND (@InsuredId IS NULL OR p.InsuredId = @InsuredId)")
                .AppendLine("ORDER BY po.OccurrenceStatus, p.NrProposta");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<dynamic>(
                query.ToString(),
                param: new {
                    Active = (int)RecordStatusEnum.Active,
                    Required = true,
                    filters.ProposalOccurrenceId,
                    filters.LoggedUserId,
                    filters.LoggedUserProfileId,
                    filters.ProductId,
                    filters.CoverageId,
                    filters.ProposalNumber,
                    filters.DateFrom,
                    filters.DateTo,
                    filters.OccurrenceStatus,
                    filters.OccurrenceTypeId,
                    filters.BrokerId,
                    filters.TakerId,
                    filters.InsuredId,
                }
            );

            return entity.Select(x => new ProposalOccurrence() {
                ProposalOccurrenceId = x.ProposalOccurrenceId,
                ProposalId = x.ProposalId,
                Proposal = new Proposal() {
                    ProposalId = x.ProposalId,
                    ProposalNumber = x.ProposalNumber,
                    ProposalDate = x.ProposalDate,
                    EffectiveDateStart = x.EffectiveDateStart,
                    EffectiveDateEnd = x.EffectiveDateEnd,
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
                        Name = x.BrokerName
                    },
                    Taker = new Taker() {
                        TakerId = x.TakerId,
                        Name = x.TakerName
                    },
                    Insured = new Insured() {
                        InsuredId = x.InsuredId,
                        Name = x.InsuredName
                    }
                },
                OccurrenceTypeId = x.OccurrenceTypeId,
                OccurrenceType = new OccurrenceType() {
                    OccurrenceTypeId = x.OccurrenceTypeId,
                    OccurrenceCode = x.OccurrenceCode,
                    Name = x.OccurrenceName,
                    Description = x.OccurrenceDescription,
                    Type = (OccurrenceTypeEnum)x.Type,
                    ValidationRule = (ValidationRuleEnum)x.ValidationRule,
                    IsTransmissionLocked = x.IsTransmissionLocked,
                    IsIssuanceLocked = x.IsIssuanceLocked,
                    RequiredAction = (RequiredActionEnum)x.RequiredAction,
                    AutomaticRefusal = (x.AutomaticRefusal == 1 ? true : false),
                    ProfileId = x.ProfileId,
                    Profile = new Profile() {
                        ProfileId = x.ProfileId,
                        Name = x.ProfileName
                    },
                    NormalSignalingTimeout = x.NormalSignalingTimeout,
                    WarningSignalingTimeout = x.WarningSignalingTimeout,
                    CriticalSignalingTimeout = x.CriticalSignalingTimeout
                },
                OccurrenceStatus = (OccurrenceStatusEnum)x.OccurrenceStatus,
                ApprovalRefusalDate = x.ApprovalRefusalDate,
                RefusalReasonId = x.RefusalReasonId,
                RefusalReason = new RefusalReason() {
                    RefusalReasonId = x.RefusalReasonId,
                    Name = x.RefusalReasonName
                },
                UserComments = x.UserComments,
                DocumentTypeCount = x.DocumentTypeCount,
                DocumentTypePendingCount = x.DocumentTypePendingCount,
                Status = (RecordStatusEnum)x.Status,
                InclusionDate = x.InclusionDate,
                InclusionUser = new User() {
                    UserId = x.InclusionUserId,
                    Name = x.InclusionUserName,
                    Email = x.InclusionUserEmail
                },
                LastChangeDate = x.LastChangeDate,
                LastChangeUser = new User() {
                    UserId = x.LastChangeUserId,
                    Name = x.LastChangeUserName,
                    Email = x.LastChangeUserEmail
                }
            }).ToList();
        }

        public async Task<ProposalOccurrence> GetAsync(long proposalOccurrenceId) {
            var occurrence = await ListAsync(new Domain.Payload.ProposalOccurrenceFilters() {
                ProposalOccurrenceId = proposalOccurrenceId
            });
            return occurrence.FirstOrDefault();
        }

        public async Task<long> AddAsync(ProposalOccurrence item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO ProposalOccurrence ( ProposalId, OccurrenceTypeId, OccurrenceStatus, ApprovalRefusalDate, RefusalReasonId, UserComments, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @ProposalId, @OccurrenceTypeId, @OccurrenceStatus, @ApprovalRefusalDate, @RefusalReasonId, @UserComments, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as bigint)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<long>(sbSql.ToString(),
                                                      new {
                                                          item.ProposalId,
                                                          item.OccurrenceTypeId,
                                                          item.OccurrenceStatus,
                                                          item.ApprovalRefusalDate,
                                                          item.RefusalReasonId,
                                                          item.UserComments,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });
            return id.First();
        }

        public async Task UpdateAsync(ProposalOccurrence item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE ProposalOccurrence")
                .AppendLine("SET OccurrenceStatus = @OccurrenceStatus, ApprovalRefusalDate = @ApprovalRefusalDate, RefusalReasonId = @RefusalReasonId, UserComments = @UserComments,")
                .AppendLine("    Status = @Status, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE ProposalOccurrenceId = @ProposalOccurrenceId");

            using var dbConn = GetMarketplaceDbConnection();
            int affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.ProposalOccurrenceId,
                    item.OccurrenceStatus,
                    item.ApprovalRefusalDate,
                    item.RefusalReasonId,
                    item.UserComments,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Ocorrência não localizada.");
            }
        }

        public async Task UpdateStatusAsync(ProposalOccurrence item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE ProposalOccurrence")
                .AppendLine("SET Status = @Status, LastChangeUserId = @LastChangeUserId, LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE ProposalOccurrenceId = @ProposalOccurrenceId");

            using var dbConn = GetMarketplaceDbConnection();
            int affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.ProposalOccurrenceId,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Ocorrência não localizada.");
            }
        }

        public async Task<IList<User>> ListLiberationusersAsync(long proposalOccurrenceId) {
            var query = new StringBuilder()
                .AppendLine("SELECT lu.Id as UserId, RTRIM(LTRIM(lu.Name)) as Name, lu.Email")
                .AppendLine("FROM ProposalOccurrence po")
                .AppendLine("LEFT JOIN (SELECT a.OccurrenceTypeId, 1 as SourceType, b.*")
                .AppendLine("			FROM OccurrenceTypeLiberationUser a ")
                .AppendLine("			INNER JOIN Users b ON b.Id = a.UserId ")
                .AppendLine("			WHERE a.Status = @Active")
                .AppendLine("			UNION ALL")
                .AppendLine("			SELECT a.OccurrenceTypeId, 2 as SourceType, b.*")
                .AppendLine("			FROM OccurrenceType a ")
                .AppendLine("			INNER JOIN Users b ON b.ProfileId = a.ProfileId) lu")
                .AppendLine("ON lu.OccurrenceTypeId = po.OccurrenceTypeId ")
                .AppendLine("WHERE po.ProposalOccurrenceId = @ProposalOccurrenceId")
                .AppendLine("AND lu.SourceType = (CASE ")
                .AppendLine("						WHEN (SELECT COUNT(1) FROM OccurrenceTypeLiberationUser WHERE OccurrenceTypeId = po.OccurrenceTypeId) > 0 ")
                .AppendLine("						THEN 1 ")
                .AppendLine("						ELSE 2 ")
                .AppendLine("					END)")
                .AppendLine("ORDER BY lu.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<User>(
                query.ToString(),
                param: new { ProposalOccurrenceId = proposalOccurrenceId, Active = (int)RecordStatusEnum.Active });

            return entity.ToList();
        }

    }
}
