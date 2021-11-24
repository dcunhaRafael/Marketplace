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
    public class LoadedTakerDao : ILoadedTakerDao {
        private readonly IConfiguration configuration;

        public LoadedTakerDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(LoadedTakerEntity tomador) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder()
                    .AppendLine("INSERT INTO LoadedTaker( UploadedFileId, FileLineIndex, RecordStatus, JsonRecord, JsonValidation, TakerType, TakerName, TakerDocumentNumber, Address, ")
                    .AppendLine("  AddressNumber, AddressComplement, District, City, State, ZipCode, Rating, Tax, Limit, ResponseStatus, SystemStatus, LoadedDate, Status,")
                    .AppendLine("  HigherPresumedBilling, Score, Telephone, ZipCodeAssumedInvoicingAmount, AssumedInvoicingAmountSerasa )")
                    .AppendLine("VALUES( @UploadedFileId, @FileLineIndex, @RecordStatus, @JsonRecord, @JsonValidation, @TakerType, @TakerName, @TakerDocumentNumber, @Address, ")
                    .AppendLine("  @AddressNumber, @AddressComplement, @District, @City, @State, @ZipCode, @Rating, @Tax, @Limit, @ResponseStatus, @SystemStatus, @LoadedDate, @Status,")
                    .AppendLine("  @HigherPresumedBilling, @Score, @Telephone, @ZipCodeAssumedInvoicingAmount, @AssumedInvoicingAmountSerasa  )")
                    .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");
                var id = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    tomador.UploadedFileId,
                    tomador.FileLineIndex,
                    RecordStatus = (int)tomador.RecordStatus,
                    tomador.JsonRecord,
                    tomador.JsonValidation,
                    TakerType = (int)tomador.TakerType,
                    tomador.TakerName,
                    tomador.TakerDocumentNumber,
                    tomador.Address,
                    tomador.AddressNumber,
                    tomador.AddressComplement,
                    tomador.District,
                    tomador.City,
                    tomador.State,
                    tomador.ZipCode,
                    tomador.Rating,
                    tomador.Tax,
                    tomador.Limit,
                    tomador.ResponseStatus,
                    tomador.SystemStatus,
                    tomador.LoadedDate,
                    tomador.Status,
                    tomador.HigherPresumedBilling,
                    tomador.Score,
                    tomador.Telephone,
                    tomador.ZipCodeAssumedInvoicingAmount,
                    tomador.AssumedInvoicingAmountSerasa
                });
                return id;
            } catch (Exception e) {
                throw new DaoException("Erro inserindo registro de carga de tomador", e);
            }
        }

        public async Task UpdateLegacyIdAsync(int loadedTakerRecordId, int legacyId, int legacyUserId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder()
                    .AppendLine("UPDATE LoadedTaker")
                    .AppendLine("SET LegacyId = @LegacyId, LegacyUserId = @LegacyUserId")
                    .AppendLine("WHERE LoadedTakerRecordId = @LoadedTakerRecordId");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    LegacyId = legacyId,
                    LegacyUserId = legacyUserId,
                    LoadedTakerRecordId = loadedTakerRecordId
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualiazndo códigos externos do registro de carga de tomador", e);
            }
        }

        public async Task UpdateSignatureIdAsync(int loadedTakerSignatureId, int signatureId) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder()
                    .AppendLine("UPDATE LoadedTakerSignature")
                    .AppendLine("   SET SignatureId = @SignatureId")
                    .AppendLine(" WHERE LoadedTakerSignatureId = @LoadedTakerSignatureId");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    SignatureId = signatureId,
                    LoadedTakerSignatureId = loadedTakerSignatureId
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualiando id da assinatura do registro de carga de tomador", e);
            }
        }

        public async Task UpateStatusAsync(int loadedTakerRecordId, StatusCargaTomadorEnum status) {
           try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros")); 
                StringBuilder sbSql = new StringBuilder()
                    .AppendLine("UPDATE LoadedTaker")
                    .AppendLine("SET Status = @Status")
                    .AppendLine("WHERE LoadedTakerRecordId = @LoadedTakerRecordId");
                await connection.ExecuteAsync(sbSql.ToString(), new {
                    Status = (int)status,
                    LoadedTakerRecordId = loadedTakerRecordId
                });
            } catch (Exception e) {
                throw new DaoException("Erro atualiando status do registro de carga de tomador", e);
            }
        }

        public async Task<int> AddSignatureAsync(LoadedTakerSignatureEntity transmissao) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("PortalSeguros"));
                StringBuilder sbSql = new StringBuilder()
                    .AppendLine("INSERT INTO LoadedTakerSignature (LoadedTakerRecordId, LegalRepresentativeDocumentNumber, LegalRepresentativeName, LegalRepresentativeBornIn, LegalRepresentativeMail, DateUtc, UserId)")
                    .AppendLine("VALUES (@LoadedTakerRecordId, @LegalRepresentativeDocumentNumber, @LegalRepresentativeName, @LegalRepresentativeBornIn, @LegalRepresentativeMail, @DateUtc, @UserId)")
                    .AppendLine("SELECT CAST(SCOPE_IDENTITY() as int)");
                var id = await connection.QuerySingleAsync<int>(sbSql.ToString(), new {
                    transmissao.LoadedTakerRecordId,
                    transmissao.LegalRepresentativeDocumentNumber,
                    transmissao.LegalRepresentativeName,
                    transmissao.LegalRepresentativeBornIn,
                    transmissao.LegalRepresentativeMail,
                    transmissao.DateUtc,
                    transmissao.UserId
                });
                return id;
            } catch (Exception e) {
                throw new DaoException("Erro gravando transmissão de registro de carga de tomador", e);
            }
        }
    }
}
