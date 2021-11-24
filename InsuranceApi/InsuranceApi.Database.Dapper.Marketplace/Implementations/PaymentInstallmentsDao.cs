using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Marketplace;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Marketplace.Implementations {
    public class PaymentInstallmentsDao : IPaymentInstallmentsDao {
        private readonly IConfiguration configuration;

        public PaymentInstallmentsDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task AddAsync(int policyId, IList<ParcelaEntity> parcelas) {
            try {

                var sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO PaymentInstallments ( ApoliceId, NrParcela, VlAdicional, DtVencimento, VlIof, VlPremioTarifario, VlPremioTotal, VlCusto, DvSituacao )");
                sbSql.AppendLine("  VALUES ( @ApoliceId, @NrParcela, @VlAdicional, @DtVencimento, @VlIof, @VlPremioTarifario, @VlPremioTotal, @VlCusto, @DvSituacao )");

                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                foreach (var parcela in parcelas) {
                    await connection.ExecuteAsync(sbSql.ToString(), new {
                        ApoliceId = policyId,
                        NrParcela = parcela.NumeroParcela,
                        VlAdicional = parcela.ValorAdicionalFracionamento,
                        DtVencimento = parcela.DataVencimento,
                        VlIof = parcela.ValorIof,
                        VlPremioTarifario = parcela.ValorPremioTarifario,
                        VlPremioTotal = parcela.ValorPremioTotal,
                        VlCusto = parcela.ValorCusto,
                        DvSituacao = parcela.StatusParcela.ToString()
                    });
                }

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados das parcelas no Marketplace", e);
            }
        }

    }
}