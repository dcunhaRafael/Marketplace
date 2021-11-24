using Dapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class ProposalSignatureDao : IProposalSignatureDao {
        private readonly IConfiguration configuration;

        public ProposalSignatureDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task AddAsync(ProposalAssinaturaEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ProposalId FROM Proposal WHERE ProposalCode = @ProposalCode");

                var proposalId = await connection.QuerySingleOrDefaultAsync<int?>(sbSql.ToString(), new {
                    ProposalCode = item.CodigoProposta
                });

                if (proposalId == null) {
                    throw new Exception("Dados da proposta não localizados para gravação.");
                }

                sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO ProposalSignature ( ProposalId, TransactionId, SignatureDate, SignedDocument, SignedDocumentName )");
                sbSql.AppendLine("VALUES ( @ProposalId, @TransactionId, @SignatureDate, @SignedDocument, @SignedDocumentName )");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    ProposalId = proposalId,
                    TransactionId = item.IdTransacao,
                    SignatureDate = item.DataAssinatura,
                    SignedDocument = item.DocumentoAssinatura,
                    SignedDocumentName = item.NomeDocumentoAssinatura
                });
            } catch (Exception e) {
                throw new DaoException("Erro adicionando assinatura da proposta", e);
            }
        }

        public async Task UpdateAsync(ProposalAssinaturaEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT ProposalId FROM Proposal WHERE ProposalCode = @ProposalCode");
                var proposalId = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    ProposalCode = item.CodigoProposta
                });
                sbSql = new StringBuilder();
                sbSql.AppendLine("UPDATE ProposalSignature");
                sbSql.AppendLine("   SET TransactionId      = @TransactionId");
                sbSql.AppendLine("     , SignatureDate      = @SignatureDate");
                sbSql.AppendLine("     , SignedDocument     = @SignedDocument");
                sbSql.AppendLine("     , SignedDocumentName = @SignedDocumentName");
                sbSql.AppendLine(" WHERE ProposalId = @ProposalId");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    TransactionId = item.IdTransacao,
                    SignatureDate = item.DataAssinatura,
                    SignedDocument = item.DocumentoAssinatura,
                    SignedDocumentName = item.NomeDocumentoAssinatura,
                    ProposalId = proposalId,
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualizando assinatura da proposta", e);
            }
        }

        public async Task<ProposalAssinaturaEntity> GetAsync(int proposalCode) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT B.ProposalCode       AS CodigoProposta");
                sbSql.AppendLine("     , A.TransactionId      AS IdTransacao");
                sbSql.AppendLine("     , A.SignatureDate      AS DataAssinatura");
                sbSql.AppendLine("     , A.SignedDocument     AS DocumentoAssinatura");
                sbSql.AppendLine("     , A.SignedDocumentName AS NomeDocumentoAssinatura");
                sbSql.AppendLine("  FROM ProposalSignature A");
                sbSql.AppendLine(" INNER JOIN Proposal B");
                sbSql.AppendLine("    ON B.ProposalId   = A.ProposalId");
                sbSql.AppendLine(" WHERE B.ProposalCode = @ProposalCode");
                var dadosAssinatura = await connection.QueryFirstOrDefaultAsync<ProposalAssinaturaEntity>(sbSql.ToString(), new {
                    ProposalCode = proposalCode
                });
                return dadosAssinatura;
            } catch (Exception e) {
                throw new DaoException("Erro obtendo assinatura da proposta", e);
            }
        }

        public async Task<StatusAssinaturaPropostaEnum> GetStatusAsync(int proposalCode) {
            try {
                StatusAssinaturaPropostaEnum status = StatusAssinaturaPropostaEnum.PendenteAssinatura;
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("SELECT CASE WHEN B.SignedDocument IS NULL THEN 1 ELSE 5 END");    // 1: Pendente, 5: Assinada
                sbSql.AppendLine("  FROM Proposal A");
                sbSql.AppendLine(" INNER JOIN ProposalSignature B");
                sbSql.AppendLine("    ON B.ProposalId   = A.ProposalId");
                sbSql.AppendLine(" WHERE A.ProposalCode = @ProposalCode");
                int? statusCode = await connection.QueryFirstAsync<int?>(sbSql.ToString(), new {
                    ProposalCode = proposalCode
                });
                if (statusCode != null) {
                    status = (StatusAssinaturaPropostaEnum)statusCode;
                }
                return status;
            } catch (Exception e) {
                throw new DaoException("Erro obtendo status da assinatura da proposta", e);
            }
        }
    }
}
