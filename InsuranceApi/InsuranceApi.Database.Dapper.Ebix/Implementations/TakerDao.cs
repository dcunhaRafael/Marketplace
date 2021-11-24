using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class TakerDao : BaseDao, ITakerDao {
        private readonly IConfiguration configuration;

        public TakerDao(IConfiguration configuration, IAppAuditorshipDao appAuditorshipDao) : base(appAuditorshipDao) {
            this.configuration = configuration;
        }

        public async Task UpdateAsync(int takerExternalCode, long personId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE Taker");
                sbSql.AppendLine("   SET ExternalId = @ExternalId ");
                sbSql.AppendLine(" WHERE PersonId = @PersonId");

                await connection.ExecuteAsync(sbSql.ToString(),
                            new {
                                ExternalId = takerExternalCode,
                                PersonId = personId
                            });
            } catch (Exception e) {
                throw new DaoException("Erro atualizando pessoa do tomador", e);
            }
        }

        public async Task<long> AddAsync(TakerModel item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO Person (PersonTypeId, Name, Document, FormattedDocument, PersonStatusTypeId, UserId, DateUtc) ");
                sbSql.AppendLine("VALUES ");
                sbSql.AppendLine("(@PersonTypeId, @Name, @Document, @FormattedDocument, @PersonStatusTypeId, @UserId, @DateUtc )");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as BigInt)");
                var personId = await connection.QuerySingleAsync<long>(sbSql.ToString(),
                                                      new {
                                                          PersonTypeId = item.TipoPessoa,
                                                          Name = item.NomePessoa,
                                                          Document = item.CpfCnpj,
                                                          FormattedDocument = item.CpfCnpj?.FormatLongToCpfCnpj(),
                                                          PersonStatusTypeId = RecordStatusEnum.Ativo,
                                                          UserId = item.LoggedUserId,
                                                          DateUtc = DateTime.UtcNow
                                                      });

                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO PersonEntityType ( PersonId, EntityTypeId, Status, UserId, DateUtc )");
                sbSql.AppendLine("                       VALUES( @PersonId, @EntityTypeId, @Status, @UserId, @DateUtc )");
                await connection.QueryAsync<long>(sbSql.ToString(),
                                       new {
                                           PersonId = personId,
                                           EntityTypeId = (int)EntityTypeEnum.Tomador,
                                           Status = RecordStatusEnum.Ativo,
                                           UserId = item.LoggedUserId,
                                           DateUtc = DateTime.UtcNow
                                       });

                sbSql = new StringBuilder();
                sbSql.AppendLine(@"insert into Taker (PersonId, ExternalId) ");
                sbSql.AppendLine(@"values (@PersonId, @ExternalId ) ");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");
                var tomadorId = await connection.QueryAsync<int>(sbSql.ToString(),
                             new {
                                 PersonId = personId,
                                 ExternalId = item.IdPessoa
                             });

                sbSql = new StringBuilder();
                sbSql.AppendLine(@"INSERT INTO [dbo].[TakerScore] ([TakerId],[CompanyQuality],[Score],[CreditLimit], [Rating],[Tax], [DateUtc])");
                sbSql.AppendLine(@" VALUES (@TakerId, @CompanyQuality, @Score, @CreditLimit, @Rating, @Tax, @DateUtc) ");
                await connection.ExecuteAsync(sbSql.ToString(),
                            new {
                                TakerId = tomadorId,
                                CompanyQuality = item.QualidadeEmpresa,
                                Score = item.Pontuacao,
                                CreditLimit = item.Limite,
                                Rating = item.Rating,
                                Tax = item.Taxa,
                                DateUtc = DateTime.UtcNow
                            });

                return personId;
            } catch (Exception e) {
                throw new DaoException("Erro incluindo pessoa do tomador", e);
            }
        }
    }
}
