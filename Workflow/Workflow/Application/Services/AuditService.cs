using Application.Interfaces.Services;
using Domain.Payload;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class AuditService : BaseLogger, IAuditService {
        private readonly IAuditRepository auditRepository;

        public AuditService(ILogger<AuditService> logger, IAuditRepository auditRepository) : base(logger) {
            this.auditRepository = auditRepository;
        }

        public async Task AddAsync(Audit item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = new Domain.Entities.Audit() {
                    Level = item.Level,
                    ActionDate = DateTime.Now,
                    FeatureName = item.FeatureName,
                    ActionName = item.ActionName,
                    Url = item.Url,
                    IpAddress = item.IpAddress,
                    BrowserName = item.BrowserName,
                    OsName = item.OsName,
                    Request = item.Request,
                    Response = item.Response,
                    UserTypeId = item.UserTypeId,
                    UserId = item.UserId,
                };

                await auditRepository.AddAsync(entity);

            } catch (Exception e) {
                LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
