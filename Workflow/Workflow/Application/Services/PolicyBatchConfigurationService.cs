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
    public class PolicyBatchConfigurationService : BaseLogger, IPolicyBatchConfigurationService {
        private readonly IMapper mapper;
        private readonly IPolicyBatchConfigurationRepository policyBatchConfigurationRepository;
        private readonly IPolicyBatchConfigurationMailRepository policyBatchConfigurationMailRepository;
        private readonly IPolicyBatchConfigurationMailDestinationRepository policyBatchConfigurationMailDestinationRepository;

        public PolicyBatchConfigurationService(ILogger<PolicyBatchConfigurationService> logger,
            IPolicyBatchConfigurationRepository policyBatchConfigurationRepository,
            IPolicyBatchConfigurationMailRepository policyBatchConfigurationMailRepository,
            IPolicyBatchConfigurationMailDestinationRepository policyBatchConfigurationMailDestinationRepository) : base(logger) {
            this.policyBatchConfigurationRepository = policyBatchConfigurationRepository;
            this.policyBatchConfigurationMailRepository = policyBatchConfigurationMailRepository;
            this.policyBatchConfigurationMailDestinationRepository = policyBatchConfigurationMailDestinationRepository;

            var mapperConfig = new MapperConfiguration(cfg => {

                cfg.CreateMap<Domain.Entities.PolicyBatchConfiguration, Domain.Payload.PolicyBatchConfiguration>()
                    .ForMember(dto => dto.LoggedUserId, map => map.MapFrom(source => source.LastChangeUserId ?? source.InclusionUserId));

                cfg.CreateMap<Domain.Payload.PolicyBatchConfiguration, Domain.Entities.PolicyBatchConfiguration>();

                cfg.CreateMap<Domain.Payload.PolicyBatchConfigurationFilters, Domain.Entities.PolicyBatchConfiguration>();

                cfg.CreateMap<Domain.Payload.PolicyBatchConfigurationMail, Domain.Entities.PolicyBatchConfigurationMail>();
                //.ForMember(dto => dto.Destinations, map => map.MapFrom(source => source.Destinations));

                cfg.CreateMap<Domain.Entities.PolicyBatchConfigurationMail, Domain.Payload.PolicyBatchConfigurationMail>()
                    .ForMember(dto => dto.LoggedUserId, map => map.MapFrom(source => source.LastChangeUserId ?? source.InclusionUserId));
                //.ForMember(dto => dto.Destinations, map => map.MapFrom(source => source.Destinations));

                cfg.CreateMap<Domain.Payload.PolicyBatchConfigurationMailDestination, Domain.Entities.PolicyBatchConfigurationMailDestination>();
                cfg.CreateMap<Domain.Entities.PolicyBatchConfigurationMailDestination, Domain.Payload.PolicyBatchConfigurationMailDestination>();

                cfg.CreateMap<Domain.Payload.User, Domain.Entities.User>();
                cfg.CreateMap<Domain.Entities.User, Domain.Payload.User>();

            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task<IList<PolicyBatchConfiguration>> ListAsync(PolicyBatchConfigurationFilters filters) {
            var methodParameters = new { filters };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyBatchConfigurationRepository.ListAsync(mapper.Map<Domain.Entities.PolicyBatchConfiguration>(filters));
                var payloads = (from a in items select mapper.Map<Domain.Payload.PolicyBatchConfiguration>(a)).ToList();
                for (int i = 0; i < payloads.Count(); i++) {
                    var mails = await policyBatchConfigurationMailRepository.ListAsync(payloads[i].PolicyBatchConfigurationId.Value);
                    payloads[i].Mails = (from m in mails select mapper.Map<Domain.Payload.PolicyBatchConfigurationMail>(m)).ToList();
                    for (int j = 0; j < payloads[i].Mails.Count; j++) {
                        var destinations = await policyBatchConfigurationMailDestinationRepository.ListAsync(payloads[i].Mails[j].PolicyBatchConfigurationMailId.Value);
                        payloads[i].Mails[j].Destinations = (from d in destinations select mapper.Map<Domain.Payload.PolicyBatchConfigurationMailDestination>(d)).ToList();
                    }
                }
                return payloads;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem das configurações de lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<PolicyBatchConfiguration> GetAsync(int policyBatchConfigurationId) {
            var methodParameters = new { policyBatchConfigurationId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await policyBatchConfigurationRepository.GetAsync(policyBatchConfigurationId);
                return mapper.Map<Domain.Payload.PolicyBatchConfiguration>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo configuração de lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<int> SaveAsync(PolicyBatchConfiguration item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.PolicyBatchConfiguration>(item);
                entity.Status = RecordStatusEnum.Active;

                int id = item.PolicyBatchConfigurationId ?? 0;
                if (item.PolicyBatchConfigurationId == null) {
                    entity.InclusionDate = DateTime.Now;
                    entity.InclusionUserId = item.LoggedUserId;
                    id = await policyBatchConfigurationRepository.AddAsync(entity);
                } else {
                    entity.LastChangeDate = DateTime.Now;
                    entity.LastChangeUserId = item.LoggedUserId;
                    await policyBatchConfigurationRepository.UpdateAsync(entity);
                }

                return id;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro salvando configuração de lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task DeleteAsync(PolicyBatchConfiguration item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.PolicyBatchConfiguration>(item);
                entity.Status = RecordStatusEnum.Inactive;
                entity.LastChangeDate = DateTime.Now;
                entity.LastChangeUserId = item.LoggedUserId;
                await policyBatchConfigurationRepository.UpdateStatusAsync(entity);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro excluindo configuração de lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<IList<PolicyBatchConfigurationMail>> ListMailsAsync(int policyBatchConfigurationId) {
            var methodParameters = new { policyBatchConfigurationId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyBatchConfigurationMailRepository.ListAsync(policyBatchConfigurationId);
                var payloads = from a in items select mapper.Map<Domain.Payload.PolicyBatchConfigurationMail>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos e-mails de alerta: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<PolicyBatchConfigurationMail> GetMailAsync(int policyBatchConfigurationMailId) {
            var methodParameters = new { policyBatchConfigurationMailId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await policyBatchConfigurationMailRepository.GetAsync(policyBatchConfigurationMailId);
                return mapper.Map<Domain.Payload.PolicyBatchConfigurationMail>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo e-mail de alerta: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<int> SaveMailAsync(PolicyBatchConfigurationMail item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.PolicyBatchConfigurationMail>(item);
                entity.Status = RecordStatusEnum.Active;

                int id = item.PolicyBatchConfigurationMailId ?? 0;
                if (item.PolicyBatchConfigurationMailId == null) {
                    entity.InclusionDate = DateTime.Now;
                    entity.InclusionUserId = item.LoggedUserId;
                    id = await policyBatchConfigurationMailRepository.AddAsync(entity);
                } else {
                    entity.LastChangeDate = DateTime.Now;
                    entity.LastChangeUserId = item.LoggedUserId;
                    await policyBatchConfigurationMailRepository.UpdateAsync(entity);
                }

                // Usuários específicos
                await policyBatchConfigurationMailDestinationRepository.UpdateStatusNotInAsync(
                    new Domain.Entities.PolicyBatchConfigurationMail() {
                        PolicyBatchConfigurationMailId = id,
                        Status = RecordStatusEnum.Inactive,
                        LastChangeDate = DateTime.Now,
                        LastChangeUserId = item.LoggedUserId
                    },
                    item.Destinations.Select(x => x.UserId.Value).ToList());
                foreach (var user in item.Destinations) {
                    var userEntity = mapper.Map<Domain.Entities.PolicyBatchConfigurationMailDestination>(user);
                    userEntity.Status = RecordStatusEnum.Active;
                    userEntity.PolicyBatchConfigurationMailId = id;
                    if (userEntity.PolicyBatchConfigurationMailDestinationId == null) {
                        userEntity.InclusionDate = DateTime.Now;
                        userEntity.InclusionUserId = item.LoggedUserId;
                        await policyBatchConfigurationMailDestinationRepository.AddAsync(userEntity);
                    } else {
                        userEntity.LastChangeDate = DateTime.Now;
                        userEntity.LastChangeUserId = item.LoggedUserId;
                        await policyBatchConfigurationMailDestinationRepository.UpdateAsync(userEntity);
                    }
                }

                return id;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro salvando e-mail de alerta: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task DeleteMailAsync(PolicyBatchConfigurationMail item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.PolicyBatchConfigurationMail>(item);
                entity.Status = RecordStatusEnum.Inactive;
                entity.LastChangeDate = DateTime.Now;
                entity.LastChangeUserId = item.LoggedUserId;
                await policyBatchConfigurationMailRepository.UpdateStatusAsync(entity);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro excluindo e-mail de alerta: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}