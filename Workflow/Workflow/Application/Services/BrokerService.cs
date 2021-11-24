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
    public class BrokerService : BaseLogger, IBrokerService {
        private readonly IBrokerRepository brokerRepository;

        public BrokerService(ILogger<BrokerService> logger, IBrokerRepository brokerRepository) : base(logger) {
            this.brokerRepository = brokerRepository;
        }

        public async Task<IList<Broker>> ListAsync(string name, RecordStatusEnum? status) {
            var methodParameters = new { name, status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await brokerRepository.ListAsync(name, status);
                var payloads = from a in items select new Broker() { 
                    BrokerId = (int)a.BrokerId, 
                    Name = a.Name, 
                    CpfCnpjNumber = a.CpfCnpjNumber, 
                    LegacyCode = a.LegacyCode, 
                    LegacyUserId = a.LegacyUserId, 
                    SusepCode = a.SusepCode };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos corretores: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
