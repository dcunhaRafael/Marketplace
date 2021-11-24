using InsuranceApi.Domain.BusinessObjects.AppSettings;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Interfaces.Application;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class AuditApplication : AppConfigHttp, IAuditApplication {
        private readonly Domain.Interfaces.Dao.Marketplace.IAuditoriaDao auditoriaDao;
        private readonly Domain.Interfaces.Dao.Marketplace.IUserDao userDao;

        public AuditApplication(IOptions<ServiceSettings> serviceSettings,
            Domain.Interfaces.Dao.Marketplace.IAuditoriaDao auditoriaDao,
            Domain.Interfaces.Dao.Marketplace.IUserDao userDao): base(serviceSettings) {
            this.auditoriaDao = auditoriaDao;
            this.userDao = userDao;
        }

        public async Task AddAsync(AuditoriaEntity item) {
            try {

                if (string.IsNullOrWhiteSpace(item.UsuarioNome)) {
                    item.UsuarioId = AppConfigHttp.CodigoUsuarioPadraoMarketplace;  //Fixo para os casos que não tem nenhum 
                } else {
                    var userId = await userDao.GetIdAsync(item.UsuarioNome);
                    if (userId == null) {
                        throw new Exception("Identificador do usuário não encontrado.");
                    }
                    item.UsuarioId = userId.Value;
                }
                await auditoriaDao.AddAsync(item);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao gravar log de auditoria.", e);
            }
        }
    }
}
