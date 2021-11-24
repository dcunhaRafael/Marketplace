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
    public class WarrantyOptionsDao : IWarrantyOptionsDao {
        private readonly IConfiguration configuration;

        public WarrantyOptionsDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(WarrantyOptionsEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO WarrantyOptions ( CreatedDate, UpdatedDate, RemovedDate, UpdatedByThatUserId, ProposalId, RecursalModalityType, RecursalModalityMaxValue, RecursalModalityDepositValue, RecursalModalityPercentageHarm, RecursalModalityAmountOfInsuredValue, DeadLineValidityOption, DeadLineValidity )");
                sbSql.AppendLine("VALUES ( @CreatedDate, @UpdatedDate, @RemovedDate, @UpdatedByThatUserId, @ProposalId, @RecursalModalityType, @RecursalModalityMaxValue, @RecursalModalityDepositValue, @RecursalModalityPercentageHarm, @RecursalModalityAmountOfInsuredValue, @DeadLineValidityOption, @DeadLineValidity )");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

                var insuredId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    CreatedDate = DateTime.Now,             //--Fixo
                    RemovedDate = (DateTime?)null,          //--Fixo
                    UpdatedByThatUserId = (int?)null,       //--Fixo
                    UpdatedDate = (DateTime?)null,          //--Fixo
                    ProposalId = item.ProposalId,
                    RecursalModalityType=item.RecursalModalityType,
                    RecursalModalityMaxValue = item.RecursalModalityMaxValue,
                    RecursalModalityDepositValue = item.RecursalModalityDepositValue,
                    RecursalModalityPercentageHarm = item.RecursalModalityPercentageHarm,
                    RecursalModalityAmountOfInsuredValue = item.RecursalModalityAmountOfInsuredValue,
                    DeadLineValidityOption = item.DeadLineValidityOption,
                    DeadLineValidity = item.DeadLineValidity
                });

                return insuredId;

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados da garantia no Marketplace", e);
            }
        }

    }
}