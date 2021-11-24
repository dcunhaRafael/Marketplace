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
    public class AuditoriaDao : IAuditoriaDao {
        private readonly IConfiguration configuration;

        public AuditoriaDao(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task AddAsync(AuditoriaEntity item) {
            try {
                using var connection = new SqlConnection(configuration.GetConnectionString("Marketplace"));
                StringBuilder sbSql = new StringBuilder();
                sbSql.AppendLine("INSERT INTO Auditoria ( DataHora, TipoUsuarioId, UsuarioId, UrlChamada, Funcionalidade, TipoAcao, IP, Navegador, SistemaOperacional, Nivel, Request, Response )")
                     .AppendLine("  VALUES ( @DataHora, @TipoUsuarioId, @UsuarioId, @UrlChamada, @Funcionalidade, @TipoAcao, @IP, @Navegador, @SistemaOperacional, @Nivel, @Request, @Response )");

                await connection.ExecuteAsync(sbSql.ToString(), new {
                    DataHora = DateTime.Now,             //--Fixo
                    item.TipoUsuarioId,
                    item.UsuarioId, 
                    item.UrlChamada, 
                    item.Funcionalidade,
                    item.TipoAcao,
                    item.IP, 
                    item.Navegador,
                    item.SistemaOperacional,
                    item.Nivel,
                    item.Request,
                    item.Response
                });

            } catch (Exception e) {
                throw new DaoException("Erro gravando dados de auditoria", e);
            }
        }

    }
}