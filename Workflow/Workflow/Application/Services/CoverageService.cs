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
    public class CoverageService : BaseLogger, ICoverageService {
        private readonly ICoverageRepository coverageRepository;

        public CoverageService(ILogger<CoverageService> logger, ICoverageRepository coverageRepository) : base(logger) {
            this.coverageRepository = coverageRepository;
        }

        public async Task<IList<Coverage>> ListAsync(int productId, RecordStatusEnum? status) {
            var methodParameters = new { productId, status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await coverageRepository.ListAsync(productId, status);
                var payloads = from a in items select new Coverage() { CoverageId = a.CoverageId, Name = a.Name };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem das coberturas: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
