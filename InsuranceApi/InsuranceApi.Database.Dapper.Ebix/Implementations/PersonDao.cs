using Dapper;
using Microsoft.Extensions.Configuration;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class PersonDao : IPersonDao {
        private readonly IConfiguration configuration;
        
        public PersonDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<IList<PersonEntity>> ListAsync(string name, string document, TipoPessoaEnum? personType, StatusTypeEnum? statusType) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT PersonId, PersonTypeId, Name, Document, FormattedDocument, PersonStatusTypeId, UserId, DateUtc");
                sbSql.AppendLine("  FROM Person");
                sbSql.AppendLine(" WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')");
                sbSql.AppendLine("   AND (@Document IS NULL OR Document LIKE '%' + @Document + '%' OR FormattedDocument = @Document)");
                sbSql.AppendLine("   AND (@PersonTypeId IS NULL OR PersonTypeId = @PersonTypeId)");
                sbSql.AppendLine("   AND (@PersonStatusTypeId IS NULL OR PersonStatusTypeId = @PersonStatusTypeId)");
                sbSql.AppendLine(" ORDER BY PersonStatusTypeId, Name");
                var items = await connection.QueryAsync<PersonEntity>(sbSql.ToString(),
                    new {
                        Name = name,
                        Document = document,
                        PersonTypeId = (int?)personType,
                        PersonStatusTypeId = (int?)statusType
                    });
                return items.ToList();
            } catch (Exception e) {
                throw new DaoException("Erro listando pessoas", e);
            }
        }
    }
}
