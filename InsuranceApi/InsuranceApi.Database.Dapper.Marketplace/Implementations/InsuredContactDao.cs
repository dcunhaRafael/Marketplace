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
    public class InsuredContactDao : IInsuredContactDao {
        private readonly IConfiguration configuration;

        public InsuredContactDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(InsuredEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO InsuredContact ( Nome, NrCnpjCpf, TpMeioComunicacao, ValorMeioComunicacao, IdPessoa )");
                sbSql.AppendLine("VALUES ( @Nome, @NrCnpjCpf, @TpMeioComunicacao, @ValorMeioComunicacao, @IdPessoa )");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

                var insuredId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    Nome = item.Nome,
                    NrCnpjCpf = item.CpfCnpj,
                    TpMeioComunicacao = "4", //--Fixo
                    ValorMeioComunicacao = item.Contato.Email,
                    IdPessoa = item.IdPessoa
                });

                return insuredId;

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados do contato do segurado no Marketplace", e);
            }
        }

    }
}