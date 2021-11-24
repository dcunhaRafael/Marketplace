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
    public class InsuredService : BaseLogger, IInsuredService {
        private readonly IInsuredRepository insuredRepository;

        public InsuredService(ILogger<InsuredService> logger, IInsuredRepository insuredRepository) : base(logger) {
            this.insuredRepository = insuredRepository;
        }

        public async Task<IList<Insured>> ListAsync(string name, RecordStatusEnum? status) {
            var methodParameters = new { name, status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await insuredRepository.ListAsync(name, status);
                var payloads = from a in items select new Insured() { 
                    InsuredId = (int)a.InsuredId, 
                    Name = a.Name, 
                    CpfCnpjNumber = a.CpfCnpjNumber, 
                    LegacyCode = a.LegacyCode };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos segurados: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
