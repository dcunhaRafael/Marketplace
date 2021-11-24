using Application.Interfaces.Services;
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
    public class UpdateIndexService : BaseLogger, IUpdateIndexService {
        private readonly IUpdateIndexRepository updateIndexRepository;

        public UpdateIndexService(ILogger<UpdateIndexService> logger, IUpdateIndexRepository updateIndexRepository) : base(logger) {
            this.updateIndexRepository = updateIndexRepository;
        }

        public async Task<IList<UpdateIndex>> ListAsync() {
            var methodParameters = new { };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await updateIndexRepository.ListAsync();
                var payloads = from a in items select new UpdateIndex() { Id = a.Id, Name = a.Name };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos índices de atualização: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
