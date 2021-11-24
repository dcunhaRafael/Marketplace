using Application.Interfaces.Services;
using AutoMapper;
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
    public class AppServiceService : BaseLogger, IAppServiceService {
        private readonly IMapper mapper;
        private readonly IAppServiceRepository appServiceRepository;
        private readonly IAppServiceLogRepository appServiceLogRepository;

        public AppServiceService(ILogger<AppServiceService> logger,
            IAppServiceRepository appServiceRepository, IAppServiceLogRepository appServiceLogRepository) : base(logger) {
            this.appServiceRepository = appServiceRepository;
            this.appServiceLogRepository = appServiceLogRepository;

            var mapperConfig = new MapperConfiguration(cfg => {

                cfg.CreateMap<Domain.Entities.AppService, Domain.Payload.AppService>();
                cfg.CreateMap<Domain.Payload.AppService, Domain.Entities.AppService>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task<IList<AppService>> ListAsync() {
            var methodParameters = new { };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await appServiceRepository.ListAsync();
                var payloads = from a in items select mapper.Map<Domain.Payload.AppService>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos serviços de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<AppService> GetAsync(int appServiceId) {
            var methodParameters = new { appServiceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await appServiceRepository.GetAsync(appServiceId);
                return mapper.Map<Domain.Payload.AppService>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<AppService> GetAsync(string name) {
            var methodParameters = new { name };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await appServiceRepository.GetAsync(name);
                return mapper.Map<Domain.Payload.AppService>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<int> SaveAsync(AppService item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.AppService>(item);
                entity.Status = RecordStatusEnum.Active;

                int id = item.AppServiceId ?? 0;
                if (item.AppServiceId == null) {
                    entity.InclusionDate = DateTime.Now;
                    entity.InclusionUserId = item.LoggedUserId;
                    id = await appServiceRepository.AddAsync(entity);
                } else {
                    entity.LastChangeDate = DateTime.Now;
                    entity.LastChangeUserId = item.LoggedUserId;
                    await appServiceRepository.UpdateAsync(entity);
                }

                return id;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro salvando serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task DeleteAsync(AppService item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.AppService>(item);
                entity.Status = RecordStatusEnum.Inactive;
                entity.LastChangeDate = DateTime.Now;
                entity.LastChangeUserId = item.LoggedUserId;
                await appServiceRepository.UpdateStatusAsync(entity);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro excluindo serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task SendKeepAliveAsync(int appServiceId) {
            var methodParameters = new { appServiceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                await appServiceRepository.UpdateKeepAliveAsync(appServiceId);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro enviando sinal de keep alive do serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task UpdateExecutionAsync(int appServiceId, ExecutionStatusEnum status, string message, string data) {
            var methodParameters = new { appServiceId, status, message };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = new Domain.Entities.AppService() {
                    AppServiceId = appServiceId,
                    ExecutionStatus = status,
                    ExecutionMessage = message,
                    ExecutionData = data
                };
                await appServiceRepository.UpdateExecutionAsync(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro atualizando status de execução do serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task AddLogAsync(int appServiceId, LogLevel logLevel, string message) {
            var methodParameters = new { appServiceId, logLevel, message };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = new Domain.Entities.AppServiceLog() {
                    AppServiceId = appServiceId,
                    LogLevel = logLevel,
                    Message = message
                };
                await appServiceLogRepository.AddAsync(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro gravando log do serviço de aplicação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }
    }
}
