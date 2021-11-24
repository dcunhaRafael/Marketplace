using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class RefusalReasonService : BaseLogger, IRefusalReasonService {
        private readonly IRefusalReasonRepository refusalReasonRepository;

        public RefusalReasonService(ILogger<RefusalReasonService> logger, IRefusalReasonRepository refusalReasonRepository) : base(logger) {
            this.refusalReasonRepository = refusalReasonRepository;
        }

        public async Task<IList<RefusalReason>> ListAsync(RecordStatusEnum? status) {
            var methodParameters = new { status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await refusalReasonRepository.ListAsync(status);
                var payloads = from a in items select new RefusalReason() { RefusalReasonId = a.RefusalReasonId.Value, Name = a.Name, LegacyCode = a.LegacyCode };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos motivos de recusa: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
