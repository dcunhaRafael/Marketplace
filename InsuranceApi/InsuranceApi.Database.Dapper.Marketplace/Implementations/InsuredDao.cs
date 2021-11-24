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
    public class InsuredDao : IInsuredDao {
        private readonly IConfiguration configuration;

        public InsuredDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<InsuredEntity> GetAsync(long cpfCnpj) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT InsuredId as Id, IdPessoa, NmPessoa as Nome, InsuredTypeId");
                sbSql.AppendLine("FROM Insured");
                sbSql.AppendLine("WHERE CAST(NrCnpjCpf as bigint) = @CpfCnpj");
                var item = await connection.QueryAsync<InsuredEntity>(sbSql.ToString(), new {
                    CpfCnpj = cpfCnpj
                });
                return item.FirstOrDefault();
            } catch (Exception e) {
                throw new DaoException("Erro obtendo segurado no Marketplace", e);
            }
        }

        public async Task<int> AddAsync(InsuredEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO Insured ( InsuredContactId, IdPessoa, NmPessoa, InsuredTypeId, NrCnpjCpf, IdEndereco, IdTpEndereco, ");
                sbSql.AppendLine("  NmTpEndereco, NmLogradouro, NrRuaEndereco, NmComplemento, NmBairro, NmCidade, NmCep, CdUf, NmUf)");
                sbSql.AppendLine("VALUES ( @InsuredContactId, @IdPessoa, @NmPessoa, @InsuredTypeId, @NrCnpjCpf, @IdEndereco, @IdTpEndereco, ");
                sbSql.AppendLine("  @NmTpEndereco, @NmLogradouro, @NrRuaEndereco, @NmComplemento, @NmBairro, @NmCidade, @NmCep, @CdUf, @NmUf)");
                sbSql.AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");

                var insuredId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    InsuredContactId = item.Contato.Id,
                    IdPessoa = item.IdPessoa,
                    NmPessoa = item.Nome,
                    InsuredTypeId = item.InsuredTypeId,
                    NrCnpjCpf = item.CpfCnpj,
                    IdEndereco = item.Endereco.IdEndereco,
                    IdTpEndereco = item.Endereco.TipoEndereco.IdTipoEndereco,
                    NmTpEndereco = item.Endereco.TipoEndereco.NomeTipoEndereco,
                    NmLogradouro = item.Endereco.Logradouro,
                    NrRuaEndereco = item.Endereco.Numero,
                    NmComplemento = item.Endereco.Complemento,
                    NmBairro = item.Endereco.Bairro,
                    NmCidade = item.Endereco.Cidade,
                    NmCep = item.Endereco.Cep,
                    CdUf = item.Endereco.IdUf,
                    NmUf = item.Endereco.UF
                });

                return insuredId;

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados do segurado no Marketplace", e);
            }
        }

    }
}