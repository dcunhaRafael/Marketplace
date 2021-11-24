
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
    public class OccurrenceTypeRepository : BaseRepository, IOccurrenceTypeRepository {
        private readonly IOccurrenceTypeDocumentRepository occurrenceTypeDocumentRepository;
        private readonly IOccurrenceTypeLiberationUserRepository occurrenceTypeLiberationUserRepository;

        public OccurrenceTypeRepository(IOccurrenceTypeDocumentRepository occurrenceTypeDocumentRepository,
            IOccurrenceTypeLiberationUserRepository occurrenceTypeLiberationUserRepository,
            IDatabaseFactory databaseOptions) : base(databaseOptions) {
            this.occurrenceTypeDocumentRepository = occurrenceTypeDocumentRepository;
            this.occurrenceTypeLiberationUserRepository = occurrenceTypeLiberationUserRepository;
        }

        public async Task<IList<OccurrenceType>> ListAsync(OccurrenceType filters) {
            var query = new StringBuilder()
                .AppendLine("SELECT ot.*, ")
                .AppendLine("   p.ProductId, p.NmProduto as Name, p.CdProduto as LegacyCode, ")
                .AppendLine("   c.CoverageId, c.NmCobertura as Name, c.IdProdutoCobertura as LegacyCode, ")
                .AppendLine("   pr.Id as ProfileId, pr.Description as Name ")
                .AppendLine("FROM OccurrenceType ot")
                .AppendLine("INNER JOIN Product p ON p.ProductId = ot.ProductId")
                .AppendLine("INNER JOIN Coverage c ON c.CoverageId = ot.CoverageId")
                .AppendLine("LEFT JOIN Profile pr ON pr.Id = ot.ProfileId")
                .AppendLine("WHERE (@ProductId IS NULL OR ot.ProductId = @ProductId)")
                .AppendLine("AND (@CoverageId IS NULL OR ot.CoverageId = @CoverageId)")
                .AppendLine("AND (@OccurrenceCode IS NULL OR ot.OccurrenceCode = @OccurrenceCode)")
                .AppendLine("AND (@Name IS NULL OR ot.Name LIKE '%' + @Name + '%')")
                .AppendLine("AND (@Description IS NULL OR ot.Description  LIKE '%' + @Description + '%')")
                .AppendLine("AND (@Type IS NULL OR ot.Type = @Type)")
                .AppendLine("AND (@ValidationRule IS NULL OR ot.ValidationRule = @ValidationRule)")
                .AppendLine("AND (@IsTransmissionLocked IS NULL OR ot.IsTransmissionLocked = @IsTransmissionLocked)")
                .AppendLine("AND (@IsIssuanceLocked IS NULL OR ot.IsIssuanceLocked = @IsIssuanceLocked)")
                .AppendLine("AND (@RequiredAction IS NULL OR ot.RequiredAction = @RequiredAction)")
                .AppendLine("AND (@AutomaticRefusal IS NULL OR ot.AutomaticRefusal = @AutomaticRefusal)")
                .AppendLine("AND (@ProfileId IS NULL OR ot.ProfileId = @ProfileId)")
                .AppendLine("AND (@Status IS NULL OR ot.Status = @Status)")
                .AppendLine("ORDER BY p.NmProduto, c.NmCobertura, ot.Name");

            using var dbConn = base.GetMarketplaceDbConnection();
            var entity = await dbConn.QueryAsync<OccurrenceType, Product, Coverage, Profile, OccurrenceType>(
                query.ToString(),
                param: new {
                    filters.ProductId,
                    filters.CoverageId,
                    filters.OccurrenceCode,
                    filters.Name,
                    filters.Description,
                    filters.Type,
                    filters.ValidationRule,
                    filters.IsTransmissionLocked,
                    filters.IsIssuanceLocked,
                    filters.RequiredAction,
                    filters.AutomaticRefusal,
                    filters.ProfileId,
                    filters.Status
                },
                map: (ot, p, c, pr) => {
                    ot.Product = p;
                    ot.Coverage = c;
                    ot.Profile = pr;
                    return ot;
                },
                splitOn: "ProductId, CoverageId, ProfileId");

            return entity.ToList();
        }

        public async Task<OccurrenceType> GetAsync(int occurrenceTypeId) {
            var query = new StringBuilder()
                .AppendLine("SELECT ot.*, ")
                .AppendLine("   p.ProductId, p.NmProduto as Name, p.CdProduto as LegacyCode, ")
                .AppendLine("   c.CoverageId, c.NmCobertura as Name, c.IdProdutoCobertura as LegacyCode, ")
                .AppendLine("   pr.Id as ProfileId, pr.Description as Name ")
                .AppendLine("FROM OccurrenceType ot")
                .AppendLine("INNER JOIN Product p ON p.ProductId = ot.ProductId")
                .AppendLine("INNER JOIN Coverage c ON c.CoverageId = ot.CoverageId")
                .AppendLine("LEFT JOIN Profile pr ON pr.Id = ot.ProfileId")
                .AppendLine("WHERE ot.OccurrenceTypeId = @OccurrenceTypeId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var task = await dbConn.QueryAsync<OccurrenceType, Product, Coverage, Profile, OccurrenceType>(
                query.ToString(),
                param: new {
                    OccurrenceTypeId = occurrenceTypeId
                },
                map: (ot, p, c, pr) => {
                    ot.Product = p;
                    ot.Coverage = c;
                    ot.Profile = pr;
                    return ot;
                },
                splitOn: "ProductId, CoverageId, ProfileId");

            var entity = task.FirstOrDefault();
            if (entity != null) {
                entity.Documents = await occurrenceTypeDocumentRepository.ListAsync(occurrenceTypeId);
                entity.LiberationUsers = await occurrenceTypeLiberationUserRepository.ListAsync(occurrenceTypeId);
            }

            return entity;
        }

        public async Task<int> AddAsync(OccurrenceType item) {
            var sbSql = new StringBuilder()
                .AppendLine("INSERT INTO OccurrenceType ( ProductId, CoverageId, OccurrenceCode, Name, Description, Type, ValidationRule, IsTransmissionLocked, IsIssuanceLocked, RequiredAction, AutomaticRefusal, ProfileId,")
                .AppendLine("      NormalSignalingTimeout, WarningSignalingTimeout, CriticalSignalingTimeout, Status, InclusionUserId, InclusionDate )")
                .AppendLine("  VALUES ( @ProductId, @CoverageId, @OccurrenceCode, @Name, @Description, @Type, @ValidationRule, @IsTransmissionLocked, @IsIssuanceLocked, @RequiredAction, @AutomaticRefusal, @ProfileId,")
                .AppendLine("      @NormalSignalingTimeout, @WarningSignalingTimeout, @CriticalSignalingTimeout, @Status, @InclusionUserId, @InclusionDate )")
                .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

            using var dbConn = base.GetMarketplaceDbConnection();
            var id = await dbConn.QueryAsync<int>(sbSql.ToString(),
                                                      new {
                                                          item.ProductId,
                                                          item.CoverageId,
                                                          item.OccurrenceCode,
                                                          item.Name,
                                                          item.Description,
                                                          item.Type,
                                                          item.ValidationRule,
                                                          item.IsTransmissionLocked,
                                                          item.IsIssuanceLocked,
                                                          item.RequiredAction,
                                                          item.AutomaticRefusal,
                                                          item.ProfileId,
                                                          item.NormalSignalingTimeout,
                                                          item.WarningSignalingTimeout,
                                                          item.CriticalSignalingTimeout,
                                                          item.Status,
                                                          item.InclusionUserId,
                                                          item.InclusionDate
                                                      });

            return id.First();
        }

        public async Task UpdateAsync(OccurrenceType item) {
            var query = new StringBuilder()
                .AppendLine("UPDATE OccurrenceType")
                .AppendLine("SET ProductId = @ProductId, ")
                .AppendLine("   CoverageId = @CoverageId, ")
                .AppendLine("   OccurrenceCode = @OccurrenceCode, ")
                .AppendLine("   Name = @Name, ")
                .AppendLine("   Description = @Description, ")
                .AppendLine("   Type = @Type, ")
                .AppendLine("   ValidationRule = @ValidationRule,")
                .AppendLine("   IsTransmissionLocked = @IsTransmissionLocked, ")
                .AppendLine("   IsIssuanceLocked = @IsIssuanceLocked, ")
                .AppendLine("   RequiredAction = @RequiredAction, ")
                .AppendLine("   ProfileId = @ProfileId,")
                .AppendLine("   NormalSignalingTimeout = @NormalSignalingTimeout, ")
                .AppendLine("   WarningSignalingTimeout = @WarningSignalingTimeout, ")
                .AppendLine("   CriticalSignalingTimeout = @CriticalSignalingTimeout,")
                .AppendLine("   Status = @Status, ")
                .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
                .AppendLine("   LastChangeDate = @LastChangeDate")
                .AppendLine("WHERE OccurrenceTypeId = @OccurrenceTypeId");

            using var dbConn = base.GetMarketplaceDbConnection();
            var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
                param: new {
                    item.OccurrenceTypeId,
                    item.ProductId,
                    item.CoverageId,
                    item.OccurrenceCode,
                    item.Name,
                    item.Description,
                    Type = (int)item.Type,
                    ValidationRule = (int)item.ValidationRule,
                    item.IsTransmissionLocked,
                    item.IsIssuanceLocked,
                    RequiredAction = (int)item.RequiredAction,
                    item.AutomaticRefusal,
                    item.ProfileId,
                    item.NormalSignalingTimeout,
                    item.WarningSignalingTimeout,
                    item.CriticalSignalingTimeout,
                    item.Status,
                    item.LastChangeUserId,
                    item.LastChangeDate
                });
            if (affectedRows == 0) {
                throw new DataException("Registro não encontrado.");
    }
}

public async Task UpdateStatusAsync(OccurrenceType item) {
    var query = new StringBuilder()
        .AppendLine("UPDATE OccurrenceType")
        .AppendLine("SET Status = @Status, ")
        .AppendLine("   LastChangeUserId = @LastChangeUserId, ")
        .AppendLine("   LastChangeDate = @LastChangeDate")
        .AppendLine("WHERE OccurrenceTypeId = @OccurrenceTypeId");

    using var dbConn = GetMarketplaceDbConnection();
    var affectedRows = await dbConn.ExecuteAsync(query.ToString(),
        param: new {
            item.OccurrenceTypeId,
            item.Status,
            item.LastChangeUserId,
            item.LastChangeDate
        });
    if (affectedRows == 0) {
        throw new DataException("Registro não encontrado.");
    }
}
    }
}
