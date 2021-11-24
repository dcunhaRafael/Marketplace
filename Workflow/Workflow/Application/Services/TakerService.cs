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
    public class TakerService : BaseLogger, ITakerService {
        private readonly ITakerRepository takerRepository;

        public TakerService(ILogger<TakerService> logger, ITakerRepository takerRepository) : base(logger) {
            this.takerRepository = takerRepository;
        }

        public async Task<IList<Taker>> ListAsync(int brokerId, string name, RecordStatusEnum? status) {
            var methodParameters = new { brokerId, name, status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await takerRepository.ListAsync(brokerId, name, status);
                var payloads = from a in items select new Taker() { 
                    TakerId = (int)a.TakerId, 
                    Name = a.Name, 
                    CpfCnpjNumber = a.CpfCnpjNumber, 
                    LegacyCode = a.LegacyCode };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem das empresas: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
