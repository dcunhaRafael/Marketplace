using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Marketplace;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Marketplace.Implementations {
    public class ClaimantsDao : IClaimantsDao {
        private readonly IConfiguration configuration;

        public ClaimantsDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(ClaimantsEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO Claimants ( ProposalId, InsuredId, IsTheMajor, IsNewInsured, CreatedDate, ProposalId1, RemovedDate, UpdatedByThatUserId, UpdatedDate )");
                sbSql.AppendLine("VALUES ( @ProposalId, @InsuredId, @IsTheMajor, @IsNewInsured, @CreatedDate, @ProposalId1, @RemovedDate, @UpdatedByThatUserId, @UpdatedDate )");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

                var insuredId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    ProposalId = item.ProposalId,
                    InsuredId = item.InsuredId,
                    IsTheMajor = 1,                         //--Fixo
                    IsNewInsured = 0,                       //--Fixo
                    CreatedDate = DateTime.Now,             //--Fixo
                    ProposalId1 = (int?)null,               //--Fixo
                    RemovedDate = (DateTime?)null,          //--Fixo
                    UpdatedByThatUserId = (int?)null,       //--Fixo
                    UpdatedDate = (DateTime?)null           //--Fixo
                });

                return insuredId;

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados do reclamante no Marketplace", e);
            }
        }

    }
}