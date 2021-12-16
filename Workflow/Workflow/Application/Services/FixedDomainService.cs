using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class FixedDomainService : BaseLogger, IFixedDomainService {
        private readonly IFixedDomainRepository fixedDomainRepository;

        public FixedDomainService(ILogger<FixedDomainService> logger, IFixedDomainRepository fixedDomainRepository) : base(logger) {
            this.fixedDomainRepository = fixedDomainRepository;
        }

        public async Task<IList<FixedDomain>> List(string name, FixedDomainGroupNameEnum? groupName) {
            var methodParameters = new { name, groupName };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await fixedDomainRepository.ListAsync(name, groupName);
                return items;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new Domain.Payload.LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos domínios: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<FixedDomain> Get(int id) {
            var methodParameters = new { id };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await fixedDomainRepository.GetAsync(id);
                return item;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new Domain.Payload.LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo domínio: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task Save(FixedDomain item, Domain.Payload.LoggerComplement complement) {
            var methodParameters = new { item, complement };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters, complement);
            try {

                item.LastChangeUserId = complement.UserId;
                item.LastChangeDate = DateTime.Now;
                await fixedDomainRepository.UpdateAsync(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, complement);
                }
                throw new ServiceException($"Erro salvando domínio: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End", complement);
            }
        }
    }
}
