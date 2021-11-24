using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Domain.Util.Mail;
using Infrastructure.Interfaces.Repositories;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class ProposalOccurrenceService : BaseLogger, IProposalOccurrenceService {
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;
        private readonly IOccurrenceTypeRepository occurrenceTypeRepository;
        private readonly IOccurrenceValidationRuleService occurrenceValidationRuleService;
        private readonly IProposalRepository proposalRepository;
        private readonly IProposalOccurrenceRepository proposalOccurrenceRepository;
        private readonly IProposalOccurrenceDocumentRepository proposalOccurrenceDocumentRepository;
        private readonly IProposalOccurrenceHistoryRepository proposalOccurrenceHistoryRepository;
        private readonly IRefusalReasonRepository refusalReasonRepository;
        private readonly IUserRepository userRepository;
        private readonly IDocumentTypeRepository documentTypeRepository;
        private readonly ILegacyTakerService legacyTakerService;

        public ProposalOccurrenceService(ILogger<ProposalOccurrenceService> logger, IEmailSender emailSender,
            IOccurrenceTypeRepository occurrenceTypeRepository, IOccurrenceValidationRuleService occurrenceValidationRuleService,
            IProposalRepository proposalRepository, IProposalOccurrenceRepository proposalOccurrenceRepository,
            IProposalOccurrenceDocumentRepository proposalOccurrenceDocumentRepository, IProposalOccurrenceHistoryRepository proposalOccurrenceHistoryRepository,
            IRefusalReasonRepository refusalReasonRepository, IUserRepository userRepository, IDocumentTypeRepository documentTypeRepository,
            ILegacyTakerService legacyTakerService) : base(logger) {
            this.emailSender = emailSender;
            this.occurrenceTypeRepository = occurrenceTypeRepository;
            this.occurrenceValidationRuleService = occurrenceValidationRuleService;
            this.proposalRepository = proposalRepository;
            this.proposalOccurrenceRepository = proposalOccurrenceRepository;
            this.proposalOccurrenceDocumentRepository = proposalOccurrenceDocumentRepository;
            this.proposalOccurrenceHistoryRepository = proposalOccurrenceHistoryRepository;
            this.refusalReasonRepository = refusalReasonRepository;
            this.userRepository = userRepository;
            this.documentTypeRepository = documentTypeRepository;
            this.legacyTakerService = legacyTakerService;

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

                cfg.CreateMap<Domain.Entities.RefusalReason, Domain.Payload.RefusalReason>();
                cfg.CreateMap<Domain.Payload.RefusalReason, Domain.Entities.RefusalReason>();

                cfg.CreateMap<Domain.Payload.ProposalHub, Domain.Entities.Proposal>();
                cfg.CreateMap<Domain.Entities.Proposal, Domain.Payload.ProposalHub>();

                cfg.CreateMap<Domain.Entities.ProposalType, Domain.Payload.ProposalType>();
                cfg.CreateMap<Domain.Payload.ProposalType, Domain.Entities.ProposalType>();

                cfg.CreateMap<Domain.Entities.Broker, Domain.Payload.Broker>();
                cfg.CreateMap<Domain.Payload.Broker, Domain.Entities.Broker>();

                cfg.CreateMap<Domain.Entities.Taker, Domain.Payload.Taker>();
                cfg.CreateMap<Domain.Payload.Taker, Domain.Entities.Taker>();

                cfg.CreateMap<Domain.Entities.Insured, Domain.Payload.Insured>();
                cfg.CreateMap<Domain.Payload.Insured, Domain.Entities.Insured>();

                cfg.CreateMap<Domain.Payload.ProposalOccurrence, Domain.Entities.ProposalOccurrence>();
                cfg.CreateMap<Domain.Entities.ProposalOccurrence, Domain.Payload.ProposalOccurrence>();

                cfg.CreateMap<Domain.Payload.ProposalOccurrenceDocument, Domain.Entities.ProposalOccurrenceDocument>();
                cfg.CreateMap<Domain.Entities.ProposalOccurrenceDocument, Domain.Payload.ProposalOccurrenceDocument>();

                cfg.CreateMap<Domain.Payload.ProposalOccurrenceHistory, Domain.Entities.ProposalOccurrenceHistory>();
                cfg.CreateMap<Domain.Entities.ProposalOccurrenceHistory, Domain.Payload.ProposalOccurrenceHistory>();


            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task<IList<ProposalOccurrence>> ListAsync(ProposalOccurrenceFilters filters) {
            var methodParameters = new { filters };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await proposalOccurrenceRepository.ListAsync(filters);
                var payloads = from a in items select mapper.Map<Domain.Payload.ProposalOccurrence>(a);

                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem das ocorrências: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<ProposalOccurrence> GetAsync(long proposalOccurrenceId) {
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await proposalOccurrenceRepository.GetAsync(proposalOccurrenceId);

                return mapper.Map<Domain.Payload.ProposalOccurrence>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<AnalyzeProposal> AnalyzeProposal(int proposalNumber, int loggedUserId) {
            var methodParameters = new { proposalNumber };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var logggedUser = await userRepository.GetAsync(loggedUserId);
                if (logggedUser == null) {
                    throw new ApplicationException("Usuário não localizado.");
                }

                var proposal = await proposalRepository.GetAsync(proposalNumber);
                if (proposal == null) {
                    throw new ApplicationException("Proposta não localizada.");
                }

                var proposalOccurrences = await ListAsync(new ProposalOccurrenceFilters() {
                    ProposalNumber = proposalNumber,
                    LoggedUserId = null
                });
                if (proposalOccurrences.Count == 0) {
                    var occurrences = await occurrenceTypeRepository.ListAsync(new Domain.Entities.OccurrenceType() {
                        ProductId = proposal.Product.ProductId.Value,
                        CoverageId = proposal.Coverage.CoverageId.Value
                    });
                    if (occurrences.Count > 0) {
                        bool hasNewOccurrences = false;
                        TakerData takerData = null;
                        TakerCreditLimit takerCreditLimit = null;
                        foreach (var item in occurrences) {
                            switch (item.ValidationRule) {
                                case ValidationRuleEnum.AlwaysGenerate:
                                    if (await occurrenceValidationRuleService.CheckAlwaysGenerate(proposal)) {
                                        await Add(item, proposal.ProposalId.Value, logggedUser.UserId.Value);
                                        hasNewOccurrences = true;
                                    }
                                    break;
                                case ValidationRuleEnum.IsInsuredBlocked:   //TODO: Depende de cadastro a ser criado
                                    if (await occurrenceValidationRuleService.CheckIsInsuredBlocked(proposal)) {
                                        await Add(item, proposal.ProposalId.Value, logggedUser.UserId.Value);
                                        hasNewOccurrences = true;
                                    }
                                    break;
                                case ValidationRuleEnum.HasCoverageCreditSubLimit:
                                    if (takerCreditLimit == null) {
                                        takerCreditLimit = legacyTakerService.GetCreditLimit(proposal.Taker.LegacyCode, new LoggerComplement());
                                    }
                                    if (!(await occurrenceValidationRuleService.CheckHasCoverageCreditSubLimit(proposal, takerCreditLimit))) {
                                        await Add(item, proposal.ProposalId.Value, logggedUser.UserId.Value);
                                        hasNewOccurrences = true;
                                    }
                                    break;
                                case ValidationRuleEnum.HasFinancialPending:
                                    if (await occurrenceValidationRuleService.CheckHasFinancialPending(proposal)) {
                                        await Add(item, proposal.ProposalId.Value, logggedUser.UserId.Value);
                                        hasNewOccurrences = true;
                                    }
                                    break;
                                case ValidationRuleEnum.HasCreditLimit:
                                    if (takerCreditLimit == null) {
                                        takerCreditLimit = legacyTakerService.GetCreditLimit(proposal.Taker.LegacyCode, new LoggerComplement());
                                    }
                                    if (!(await occurrenceValidationRuleService.CheckHasCreditLimit(proposal, takerCreditLimit))) {
                                        await Add(item, proposal.ProposalId.Value, logggedUser.UserId.Value);
                                        hasNewOccurrences = true;
                                    }
                                    break;
                                case ValidationRuleEnum.IsCCGSigned:
                                    if (takerData == null) {
                                        takerData = legacyTakerService.Get(proposal.Taker.LegacyCode, proposal.Broker.LegacyUserId, new LoggerComplement());
                                    }
                                    if (!(await occurrenceValidationRuleService.CheckIsCCGSigned(takerData))) {
                                        await Add(item, proposal.ProposalId.Value, logggedUser.UserId.Value);
                                        hasNewOccurrences = true;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        if (hasNewOccurrences) {
                            proposalOccurrences = await ListAsync(new ProposalOccurrenceFilters() {
                                ProposalNumber = proposalNumber,
                                LoggedUserId = null
                            });
                        }
                    }
                }

                var response = new AnalyzeProposal() {
                    IsRefused = proposalOccurrences.Any(x => x.OccurrenceStatus == OccurrenceStatusEnum.Refused),
                    IsApproved = !proposalOccurrences.Any(x => x.OccurrenceStatus != OccurrenceStatusEnum.Approved),
                    Occurrences = proposalOccurrences
                };
                return response;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro analizando proposta: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        private async Task Add(Domain.Entities.OccurrenceType occurrence, int proposalId, int loggedUserId) {

            var now = DateTime.Now;

            //-- Ocorrência
            var proposalOccurrenceId = await proposalOccurrenceRepository.AddAsync(new Domain.Entities.ProposalOccurrence() {
                ProposalId = proposalId,
                OccurrenceTypeId = occurrence.OccurrenceTypeId,
                OccurrenceStatus = ((occurrence.AutomaticRefusal ?? false) ? OccurrenceStatusEnum.Refused : OccurrenceStatusEnum.Pending),
                ApprovalRefusalDate = ((occurrence.AutomaticRefusal ?? false) ? now : null),
                RefusalReasonId = null,
                UserComments = ((occurrence.AutomaticRefusal ?? false) ? "Reprovação automática" : null),
                Status = RecordStatusEnum.Active,
                InclusionUserId = loggedUserId,
                InclusionDate = now
            });

            //-- Histórico
            await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                ProposalOccurrenceId = proposalOccurrenceId,
                ActionType = OccurrenceActionTypeEnum.Creation,
                Description = "",
                InclusionUserId = loggedUserId,
                InclusionDate = now
            });
            if ((occurrence.AutomaticRefusal ?? false)) {
                await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                    ProposalOccurrenceId = proposalOccurrenceId,
                    ActionType = OccurrenceActionTypeEnum.Refusal,
                    Description = "Reprovação automática.",
                    InclusionUserId = loggedUserId,
                    InclusionDate = now
                });
            }

        }

        public async Task ApproveAsync(ProposalOccurrenceApprove item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var logggedUser = await userRepository.GetAsync(item.LoggedUserId);
                if (logggedUser == null) {
                    throw new ApplicationException("Usuário não localizado.");
                }

                var now = DateTime.Now;
                var description = string.IsNullOrWhiteSpace(item.UserComments) ? "Ocorrência aprovada." : item.UserComments;

                //-- Ocorrência
                await proposalOccurrenceRepository.UpdateAsync(new Domain.Entities.ProposalOccurrence() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    OccurrenceStatus = OccurrenceStatusEnum.Approved,
                    ApprovalRefusalDate = now,
                    RefusalReasonId = null,
                    UserComments = description,
                    Status = RecordStatusEnum.Active,
                    LastChangeUserId = item.LoggedUserId,
                    LastChangeDate = now
                });

                //-- Histórico
                await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    ActionType = OccurrenceActionTypeEnum.Approval,
                    Description = description,
                    InclusionUserId = item.LoggedUserId,
                    InclusionDate = now
                });

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro aprovando ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }
        public async Task RefuseAsync(ProposalOccurrenceRefuse item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var logggedUser = await userRepository.GetAsync(item.LoggedUserId);
                if (logggedUser == null) {
                    throw new ApplicationException("Usuário não localizado.");
                }
                var refusalReason = refusalReasonRepository.GetAsync(item.RefusalReasonId ?? 0);
                if (refusalReason == null) {
                    throw new ApplicationException("Motivo de recusa não localizado.");
                }

                var now = DateTime.Now;
                var description = string.IsNullOrWhiteSpace(item.UserComments) ? "Ocorrência reprovada." : item.UserComments;

                //-- Ocorrência
                await proposalOccurrenceRepository.UpdateAsync(new Domain.Entities.ProposalOccurrence() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    OccurrenceStatus = OccurrenceStatusEnum.Refused,
                    ApprovalRefusalDate = now,
                    RefusalReasonId = item.RefusalReasonId,
                    UserComments = description,
                    Status = RecordStatusEnum.Active,
                    LastChangeUserId = item.LoggedUserId,
                    LastChangeDate = now
                });

                //-- Histórico
                await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    ActionType = OccurrenceActionTypeEnum.Refusal,
                    Description = description,
                    InclusionUserId = item.LoggedUserId,
                    InclusionDate = now
                });

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro recusando ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task ForwardAsync(ProposalOccurrenceForward item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var proposal = await proposalRepository.GetAsync(item.ProposalNumber);
                if (proposal == null) {
                    throw new ApplicationException("Proposta não localizada.");
                }
                var proposalOccurrence = await proposalOccurrenceRepository.GetAsync(item.ProposalOccurrenceId ?? 0);
                if (proposalOccurrence == null) {
                    throw new ApplicationException("Ocorrência não localizada.");
                }
                var logggedUser = await userRepository.GetAsync(item.LoggedUserId);
                if (logggedUser == null) {
                    throw new ApplicationException("Usuário não localizado.");
                }
                var forwardUser = await userRepository.GetAsync(item.ForwardUserId);
                if (forwardUser == null) {
                    throw new ApplicationException("Usuário para encaminhamento não localizado.");
                }
                if (string.IsNullOrWhiteSpace(forwardUser.Email)) {
                    throw new ApplicationException("Usuário para encaminhamento não possui e-mail parametrizado.");
                }

                var now = DateTime.Now;

                //-- Envia o e-mail
                var subject = $"Proposta {item.ProposalNumber} - {proposalOccurrence.OccurrenceType.Name}";
                var body = string.IsNullOrWhiteSpace(item.UserComments) ? $"Ocorrência encaminhada para: {forwardUser.Name} ({forwardUser.Email})." : item.UserComments;
                await emailSender.SendEmailAsync(forwardUser.Email, subject, body);

                //-- Histórico
                await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    ActionType = OccurrenceActionTypeEnum.Forwarding,
                    Description = body,
                    InclusionUserId = item.LoggedUserId,
                    InclusionDate = now
                });

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro encaminhando ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<IList<ProposalOccurrenceDocument>> ListDocumentsAsync(long proposalOccurrenceId) {
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var occurrence = await proposalOccurrenceRepository.GetAsync(proposalOccurrenceId);
                var occurrenceType = await occurrenceTypeRepository.GetAsync(occurrence.OccurrenceTypeId.Value);
                var loadedDocuments = await proposalOccurrenceDocumentRepository.ListAsync(proposalOccurrenceId);

                var payloads = new List<ProposalOccurrenceDocument>();
                foreach (var item in occurrenceType.Documents) {
                    var loadedDocument = loadedDocuments.FirstOrDefault(x => x.DocumentTypeId == item.DocumentTypeId);
                    payloads.Add(new ProposalOccurrenceDocument() {
                        ProposalOccurrenceId = proposalOccurrenceId,
                        ProposalOccurrenceDocumentId = loadedDocument?.ProposalOccurrenceDocumentId,
                        DocumentTypeId = item.DocumentTypeId.Value,
                        DocumentType = new DocumentType() {
                            DocumentTypeId = item.DocumentTypeId,
                            Name = item.DocumentType.Name,
                            IsRequired = item.IsRequired
                        },
                        FileName = loadedDocument?.FileName,
                        FileContents = null
                    });
                }
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro listando documentos da ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<long> AddDocumentAsync(ProposalOccurrenceDocument item, int loggedUserId) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var logggedUser = await userRepository.GetAsync(loggedUserId);
                if (logggedUser == null) {
                    throw new ApplicationException("Usuário não localizado.");
                }
                var documentType = await documentTypeRepository.GetAsync(item.DocumentTypeId);
                if (documentType == null) {
                    throw new ApplicationException("Tipo de documento não localizado.");
                }

                var now = DateTime.Now;

                var id = await proposalOccurrenceDocumentRepository.AddAsync(new Domain.Entities.ProposalOccurrenceDocument() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    DocumentTypeId = item.DocumentTypeId,
                    FileName = item.FileName,
                    FileContents = item.FileContents,
                    Status = RecordStatusEnum.Active,
                    InclusionUserId = loggedUserId,
                    InclusionDate = now
                });

                //-- Histórico
                await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                    ProposalOccurrenceId = item.ProposalOccurrenceId,
                    ActionType = OccurrenceActionTypeEnum.DocumentAttach,
                    Description = $"Anexado documento: {documentType.Name} ({item.FileName})",
                    InclusionUserId = loggedUserId,
                    InclusionDate = now
                });

                return id;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro adicionando documento na ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<ProposalOccurrenceDocument> GetDocumentAsync(long proposalOccurrenceDocumentId) {
            var methodParameters = new { proposalOccurrenceDocumentId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var document = await proposalOccurrenceDocumentRepository.GetAsync(proposalOccurrenceDocumentId, true);
                return mapper.Map<Domain.Payload.ProposalOccurrenceDocument>(document);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo documento da ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task DeleteDocumentAsync(long proposalOccurrenceDocumentId, int loggedUserId) {
            var methodParameters = new { proposalOccurrenceDocumentId, loggedUserId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var document = await proposalOccurrenceDocumentRepository.GetAsync(proposalOccurrenceDocumentId, false);
                if (document == null) {
                    throw new ApplicationException("Documento não localizado.");
                }
                var logggedUser = await userRepository.GetAsync(loggedUserId);
                if (logggedUser == null) {
                    throw new ApplicationException("Usuário não localizado.");
                }
                var documentType = await documentTypeRepository.GetAsync(document.DocumentTypeId);
                if (documentType == null) {
                    throw new ApplicationException("Tipo de documento não localizado.");
                }

                var now = DateTime.Now;

                await proposalOccurrenceDocumentRepository.UpdateStatusAsync(new Domain.Entities.ProposalOccurrenceDocument() {
                    ProposalOccurrenceDocumentId = proposalOccurrenceDocumentId,
                    Status = RecordStatusEnum.Inactive,
                    LastChangeUserId = loggedUserId,
                    LastChangeDate = now
                });

                //-- Histórico
                await proposalOccurrenceHistoryRepository.AddAsync(new Domain.Entities.ProposalOccurrenceHistory() {
                    ProposalOccurrenceId = document.ProposalOccurrenceId,
                    ActionType = OccurrenceActionTypeEnum.DocumentRemoved,
                    Description = $"Removido documento: {documentType.Name} ({document.FileName})",
                    InclusionUserId = loggedUserId,
                    InclusionDate = now
                });

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro excluíndo documento da ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<IList<ProposalOccurrenceHistory>> ListHistoriesAsync(long proposalOccurrenceId) {
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await proposalOccurrenceHistoryRepository.ListAsync(proposalOccurrenceId);
                var payloads = from a in items select mapper.Map<Domain.Payload.ProposalOccurrenceHistory>(a);

                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro listando histórico da ocorrência: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<IList<User>> ListLiberationUsersAsync(long proposalOccurrenceId) {
            var methodParameters = new { proposalOccurrenceId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await proposalOccurrenceRepository.ListLiberationusersAsync(proposalOccurrenceId);
                var payloads = from a in items select mapper.Map<Domain.Payload.User>(a);

                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro listando usuários para liberação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
