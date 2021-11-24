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
    public class ProposalParcDao : IProposalParcDao {
        private readonly IConfiguration configuration;

        public ProposalParcDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task AddAsync(int proposalId, IList<ParcelaEntity> parcelas) {
            try {

                var sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalParc ( CreatedDate, ProposalId, NumberParc, AditionalValue, PaymentDueDate, ValueIOF, TariffPremiumValue, TotalPremiumValue, ValueCost, SituationDv )");
                sbSql.AppendLine("  VALUES ( @CreatedDate, @ProposalId, @NumberParc, @AditionalValue, @PaymentDueDate, @ValueIOF, @TariffPremiumValue, @TotalPremiumValue, @ValueCost, @SituationDv )");

                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                foreach (var parcela in parcelas) {
                    await connection.ExecuteAsync(sbSql.ToString(), new {
                        CreatedDate = DateTime.Now,
                        ProposalId = proposalId,
                        NumberParc = parcela.NumeroParcela,
                        AditionalValue = parcela.ValorAdicionalFracionamento,
                        PaymentDueDate = parcela.DataVencimento,
                        ValueIOF = parcela.ValorIof,
                        TariffPremiumValue = parcela.ValorPremioTarifario,
                        TotalPremiumValue = parcela.ValorPremioTotal,
                        ValueCost = parcela.ValorCusto,
                        SituationDv = parcela.StatusParcela.ToString()
                    });
                }

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados das parcelas no Marketplace", e);
            }
        }

    }
}