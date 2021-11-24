using Application.Interfaces.Services;
using AutoMapper;
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
    public class PolicyRenovationService : BaseLogger, IPolicyRenovationService {
        private readonly IMapper mapper;
        private readonly IPolicyRenovationRepository policyRenovationRepository;

        public PolicyRenovationService(ILogger<PolicyRenovationService> logger,
            IPolicyRenovationRepository policyRenovationRepository) : base(logger) {
            this.policyRenovationRepository = policyRenovationRepository;

            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Domain.Payload.PolicyRenovation, Domain.Entities.PolicyRenovation>();
                cfg.CreateMap<Domain.Entities.PolicyRenovation, Domain.Payload.PolicyRenovation>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task<IList<PolicyRenovation>> ListAsync(int policyBatchId) {
            var methodParameters = new { policyBatchId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyRenovationRepository.ListAsync(new Domain.Entities.PolicyRenovation() { PolicyBatchId = policyBatchId });
                var payloads = from a in items select mapper.Map<Domain.Payload.PolicyRenovation>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem das apólices do lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<PolicyRenovation> GetAsync(int policyRenovationId) {
            var methodParameters = new { policyRenovationId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await policyRenovationRepository.GetAsync(policyRenovationId);
                return mapper.Map<Domain.Payload.PolicyRenovation>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo apólice: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}