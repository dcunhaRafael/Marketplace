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
    public class OccurrenceTypeService : BaseLogger, IOccurrenceTypeService {
        private readonly IMapper mapper;
        private readonly IOccurrenceTypeRepository occurrenceTypeRepository;
        private readonly IOccurrenceTypeDocumentRepository occurrenceTypeDocumentRepository;
        private readonly IOccurrenceTypeLiberationUserRepository occurrenceTypeLiberationUserRepository;

        public OccurrenceTypeService(ILogger<OccurrenceTypeService> logger,
            IOccurrenceTypeRepository occurrenceTypeRepository, IOccurrenceTypeDocumentRepository occurrenceTypeDocumentRepository, IOccurrenceTypeLiberationUserRepository occurrenceTypeLiberationUserRepository) : base(logger) {
            this.occurrenceTypeRepository = occurrenceTypeRepository;
            this.occurrenceTypeDocumentRepository = occurrenceTypeDocumentRepository;
            this.occurrenceTypeLiberationUserRepository = occurrenceTypeLiberationUserRepository;

            var mapperConfig = new MapperConfiguration(cfg => {

                cfg.CreateMap<Domain.Entities.Product, Domain.Payload.Product>();
                cfg.CreateMap<Domain.Payload.Product, Domain.Entities.Product>();

                cfg.CreateMap<Domain.Entities.Coverage, Domain.Payload.Coverage>();
                cfg.CreateMap<Domain.Payload.Coverage, Domain.Entities.Coverage>();

                cfg.CreateMap<Domain.Entities.Profile, Domain.Payload.Profile>();
                cfg.CreateMap<Domain.Payload.Profile, Domain.Entities.Profile>();

                cfg.CreateMap<Domain.Entities.User, Domain.Payload.User>();
                cfg.CreateMap<Domain.Payload.User, Domain.Entities.User>();

                cfg.CreateMap<Domain.Entities.DocumentType, Domain.Payload.DocumentType>();
                cfg.CreateMap<Domain.Payload.DocumentType, Domain.Entities.DocumentType>();

                cfg.CreateMap<Domain.Entities.OccurrenceTypeLiberationUser, Domain.Payload.OccurrenceTypeLiberationUser>();
                cfg.CreateMap<Domain.Payload.OccurrenceTypeLiberationUser, Domain.Entities.OccurrenceTypeLiberationUser>();

                cfg.CreateMap<Domain.Entities.OccurrenceTypeDocument, Domain.Payload.OccurrenceTypeDocument>();
                cfg.CreateMap<Domain.Payload.OccurrenceTypeDocument, Domain.Entities.OccurrenceTypeDocument>();

                cfg.CreateMap<Domain.Entities.OccurrenceType, Domain.Payload.OccurrenceType>()
                    .ForMember(dto => dto.LoggedUserId, map => map.MapFrom(source => source.LastChangeUserId ?? source.InclusionUserId));

                cfg.CreateMap<Domain.Payload.OccurrenceType, Domain.Entities.OccurrenceType>()
                    .ForMember(entity => entity.Product, map => map.MapFrom(source => new Product() { Name = source.Name, ProductId = source.ProductId }));

                cfg.CreateMap<Domain.Payload.OccurrenceTypeFilters, Domain.Entities.OccurrenceType>();

            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task<IList<OccurrenceType>> ListAsync(OccurrenceTypeFilters filters) {
            var methodParameters = new { filters };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await occurrenceTypeRepository.ListAsync(mapper.Map<Domain.Entities.OccurrenceType>(filters));
                var payloads = from a in items select mapper.Map<Domain.Payload.OccurrenceType>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos tipos de ocorrências: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<OccurrenceType> GetAsync(int occurrenceTypeId) {
            var methodParameters = new { occurrenceTypeId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await occurrenceTypeRepository.GetAsync(occurrenceTypeId);
                return mapper.Map<Domain.Payload.OccurrenceType>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo tipo de ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<int> SaveAsync(OccurrenceType item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.OccurrenceType>(item);
                entity.Status = RecordStatusEnum.Active;

                int id = item.OccurrenceTypeId ?? 0;
                if (item.OccurrenceTypeId == null) {
                    entity.InclusionDate = DateTime.Now;
                    entity.InclusionUserId = item.LoggedUserId;
                    id = await occurrenceTypeRepository.AddAsync(entity);
                } else {
                    entity.LastChangeDate = DateTime.Now;
                    entity.LastChangeUserId = item.LoggedUserId;
                    await occurrenceTypeRepository.UpdateAsync(entity);
                }

                // Documents
                await occurrenceTypeDocumentRepository.UpdateStatusNotInAsync(
                    new Domain.Entities.OccurrenceType() {
                        OccurrenceTypeId = id,
                        Status = RecordStatusEnum.Inactive,
                        LastChangeDate = DateTime.Now,
                        LastChangeUserId = item.LoggedUserId
                    },
                    item.Documents.Select(x => x.DocumentTypeId.Value).ToList());
                foreach (var document in item.Documents) {
                    var documentEntity = mapper.Map<Domain.Entities.OccurrenceTypeDocument>(document);
                    documentEntity.Status = RecordStatusEnum.Active;
                    documentEntity.OccurrenceTypeId = id;
                    if (documentEntity.OccurrenceTypeDocumentId == null) {
                        documentEntity.InclusionDate = DateTime.Now;
                        documentEntity.InclusionUserId = item.LoggedUserId;
                        await occurrenceTypeDocumentRepository.AddAsync(documentEntity);
                    } else {
                        documentEntity.LastChangeDate = DateTime.Now;
                        documentEntity.LastChangeUserId = item.LoggedUserId;
                        await occurrenceTypeDocumentRepository.UpdateAsync(documentEntity);
                    }
                }

                // Liberation users
                await occurrenceTypeLiberationUserRepository.UpdateStatusNotInAsync(
                    new Domain.Entities.OccurrenceType() {
                        OccurrenceTypeId = id,
                        Status = RecordStatusEnum.Inactive,
                        LastChangeDate = DateTime.Now,
                        LastChangeUserId = item.LoggedUserId
                    },
                    item.LiberationUsers.Select(x => x.UserId.Value).ToList());
                foreach (var user in item.LiberationUsers) {
                    var userEntity = mapper.Map<Domain.Entities.OccurrenceTypeLiberationUser>(user);
                    userEntity.Status = RecordStatusEnum.Active;
                    userEntity.OccurrenceTypeId = id;
                    if (userEntity.OccurrenceTypeLiberationUserId == null) {
                        userEntity.InclusionDate = DateTime.Now;
                        userEntity.InclusionUserId = item.LoggedUserId;
                        await occurrenceTypeLiberationUserRepository.AddAsync(userEntity);
                    } else {
                        userEntity.LastChangeDate = DateTime.Now;
                        userEntity.LastChangeUserId = item.LoggedUserId;
                        await occurrenceTypeLiberationUserRepository.UpdateAsync(userEntity);
                    }
                }

                return id;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro salvando tipo de ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task DeleteAsync(OccurrenceType item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.OccurrenceType>(item);
                entity.Status = RecordStatusEnum.Inactive;
                entity.LastChangeDate = DateTime.Now;
                entity.LastChangeUserId = item.LoggedUserId;
                await occurrenceTypeRepository.UpdateStatusAsync(entity);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro excluindo tipo de ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }


    }
}