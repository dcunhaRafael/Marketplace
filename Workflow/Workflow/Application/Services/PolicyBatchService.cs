using Application.Interfaces.Services;
using AutoMapper;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Extensions;
using Domain.Util.Log;
using Domain.Util.Mail;
using Infrastructure.Interfaces.Repositories;
using Integration.Interfaces.Legacy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services {
    public class PolicyBatchService : BaseLogger, IPolicyBatchService {
        private readonly IMapper mapper;
        private readonly IPolicyBatchRepository policyBatchRepository;
        private readonly IPolicyBatchConfigurationRepository policyBatchConfigurationRepository;
        private readonly IPolicyBatchConfigurationMailRepository policyBatchConfigurationMailRepository;
        private readonly IPolicyBatchConfigurationMailDestinationRepository policyBatchConfigurationMailDestinationRepository;
        private readonly IPolicyRenovationRepository policyRenovationRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailSender emailSender;
        private readonly AppSettings appSettings;
        private readonly ISelicService selicService;
        private readonly IRenewalApiService renewalApiService;

        public PolicyBatchService(ILogger<PolicyBatchService> logger,
            IPolicyBatchRepository policyBatchRepository,
            IPolicyBatchConfigurationRepository policyBatchConfigurationRepository,
            IPolicyBatchConfigurationMailRepository policyBatchConfigurationMailRepository,
            IPolicyBatchConfigurationMailDestinationRepository policyBatchConfigurationMailDestinationRepository,
            IPolicyRenovationRepository policyRenovationRepository,
            IUserRepository userRepository,
            IEmailSender emailSender, IOptions<AppSettings> appSettings,
            ISelicService selicService,
            IRenewalApiService renewalApiService) : base(logger) {
            this.policyBatchRepository = policyBatchRepository;
            this.policyBatchConfigurationRepository = policyBatchConfigurationRepository;
            this.policyBatchConfigurationMailRepository = policyBatchConfigurationMailRepository;
            this.policyBatchConfigurationMailDestinationRepository = policyBatchConfigurationMailDestinationRepository;
            this.policyRenovationRepository = policyRenovationRepository;
            this.userRepository = userRepository;
            this.emailSender = emailSender;
            this.appSettings = appSettings.Value;
            this.selicService = selicService;
            this.renewalApiService = renewalApiService;

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

                cfg.CreateMap<Domain.Payload.PolicyBatch, Domain.Entities.PolicyBatch>();
                cfg.CreateMap<Domain.Entities.PolicyBatch, Domain.Payload.PolicyBatch>();

                cfg.CreateMap<Domain.Payload.PolicyBatchItem, Domain.Entities.PolicyBatchItem>();
                cfg.CreateMap<Domain.Entities.PolicyBatchItem, Domain.Payload.PolicyBatchItem>();

                cfg.CreateMap<Domain.Entities.PolicyBatchItem, Domain.Entities.PolicyRenovation>();
                cfg.CreateMap<Domain.Entities.PolicyRenovation, Domain.Entities.PolicyBatchItem>();

                cfg.CreateMap<Domain.Payload.PolicyRenovation, Domain.Entities.PolicyRenovation>();
                cfg.CreateMap<Domain.Entities.PolicyRenovation, Domain.Payload.PolicyRenovation>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        public async Task<IList<PolicyBatch>> ListAsync(PolicyBatch filters) {
            var methodParameters = new { filters };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyBatchRepository.ListAsync(mapper.Map<Domain.Entities.PolicyBatch>(filters));
                var payloads = from a in items select mapper.Map<Domain.Payload.PolicyBatch>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos lotes: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<IList<PolicyBatch>> ListNewAsync(PolicyBatchConfiguration filters) {
            var methodParameters = new { filters };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyBatchRepository.ListNewAsync(mapper.Map<Domain.Entities.PolicyBatchConfiguration>(filters));
                var payloads = from a in items select mapper.Map<Domain.Payload.PolicyBatch>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos lotes disponíveis para criação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<PolicyBatch> GetAsync(int policyBatchId) {
            var methodParameters = new { policyBatchId };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var item = await policyBatchRepository.GetAsync(policyBatchId);
                return mapper.Map<Domain.Payload.PolicyBatch>(item);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro obtendo configuração de lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task<int> SaveAsync(PolicyBatch item, int processDays) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.PolicyBatch>(item);
                entity.Status = RecordStatusEnum.Active;

                if (item.PolicyBatchId == null) {
                    // Cria o lote
                    entity.BatchStatus = PolicyBatchStatusEnum.Pending;
                    entity.InclusionDate = DateTime.Now;
                    entity.InclusionUserId = item.LoggedUserId;
                    entity.PolicyBatchId = await policyBatchRepository.AddAsync(entity);
                    // Amarra os registros de apólice com o lote
                    await policyBatchRepository.LinkBatchPoliciesAsync(entity, processDays);
                    // Gera os pedidos de renovação para cada apólice do lote
                    var policies = await policyBatchRepository.ListItemsAsync(entity.PolicyBatchId.Value, null);
                    foreach (var policy in policies) {
                        var policyRenovation = mapper.Map<Domain.Entities.PolicyRenovation>(policy);

                        var termDays = (policyRenovation.EndOfTerm.Value - policyRenovation.StartOfTerm.Value).TotalDays;
                        policyRenovation.NewStartOfTerm = policyRenovation.EndOfTerm.Value.AddDays(1);
                        policyRenovation.NewEndOfTerm = policyRenovation.NewStartOfTerm.Value.AddDays(termDays);
                        policyRenovation.NewInsuredAmount = policyRenovation.InsuredAmount;
                        if (policyRenovation.RenovationUpdateIndexBcCode != null) {
                            policyRenovation.NewInsuredAmount = await selicService.ApplyCorrectionAsync(policyRenovation.InsuredAmount.Value, policyRenovation.StartOfTerm.Value, policyRenovation.EndOfTerm.Value);
                        }
                        policyRenovation.NewInsuredObject = policyRenovation.InsuredObject;
                        policyRenovation.NewPolicyCode = null;
                        policyRenovation.RenovationStatusId = RenovationStatusEnum.PreApproved;

                        await policyRenovationRepository.AddAsync(policyRenovation);
                    }
                } else {
                    entity.LastChangeDate = DateTime.Now;
                    entity.LastChangeUserId = item.LoggedUserId;
                    await policyBatchRepository.UpdateAsync(entity);
                }

                return entity.PolicyBatchId.Value;

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro salvando lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task DeleteAsync(PolicyBatch item) {
            var methodParameters = new { item };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var entity = mapper.Map<Domain.Entities.PolicyBatch>(item);
                entity.Status = RecordStatusEnum.Inactive;
                entity.LastChangeDate = DateTime.Now;
                entity.LastChangeUserId = item.LoggedUserId;
                await policyBatchRepository.UpdateStatusAsync(entity);

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro excluindo lote: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task SendAlertMailAsync(PolicyBatchConfigurationMail config, PolicyBatchItem item) {

            // Monta a lista de destinatários
            var emails = new List<string>();
            if (config.SendToBroker && !string.IsNullOrWhiteSpace(item.BrokerEmails)) {
                //var items = from a in item.BrokerEmails.Trim().Split(" ") where !string.IsNullOrWhiteSpace(a) select a;
                //emails.AddRange(items.ToList());
                emails.Add("ledimage@gmail.com");
            }
            if (config.SendToTaker && !string.IsNullOrWhiteSpace(item.TakerEmails)) {
                //var items = from a in item.TakerEmails.Trim().Split(" ") where !string.IsNullOrWhiteSpace(a) select a;
                //emails.AddRange(items.ToList());
                emails.Add("ledimage@hotmail.com");
            }
            if (config.Destinations != null && config.Destinations.Count > 0) {
                //emails.AddRange(config.Destinations.Where(x => !string.IsNullOrEmpty(x.User.Email)).Select(x => x.User.Email).ToList());
                emails.Add("sumiya@uol.com.br");
            }
            if (emails.Count == 0) {
                throw new Exception("Nenhum destinatário localizado para envio da mensagem.");
            }

            // Monta os textos
            var subject = config.Subject.Replace("@PolicyCode", item.PolicyCode)
                .Replace("@EndorsementNumber", item.EndorsementNumber.ToString())
                .Replace("@IssueDate", item.IssueDate?.FormatDate())
                .Replace("@StartOfTerm", item.StartOfTerm?.FormatDate())
                .Replace("@EndOfTerm", item.EndOfTerm?.FormatDate())
                .Replace("@InsuredAmount", item.InsuredAmount?.FormatCurrency())
                .Replace("@BrokerName", item.BrokerName)
                .Replace("@BrokerDocument", item.BrokerDocument?.FormatCpfCnpj())
                .Replace("@InsuredName", item.InsuredName)
                .Replace("@InsuredDocument", item.InsuredDocument?.FormatCpfCnpj())
                .Replace("@TakerName", item.TakerName)
                .Replace("@TakerDocument", item.TakerDocument?.FormatCpfCnpj())
                .Replace("@ProductName", item.ProductName)
                .Replace("@CoverageName", item.CoverageName);
            var body = config.Body.Replace("@PolicyCode", item.PolicyCode)
                .Replace("@EndorsementNumber", item.EndorsementNumber.ToString())
                .Replace("@IssueDate", item.IssueDate?.FormatDate())
                .Replace("@StartOfTerm", item.StartOfTerm?.FormatDate())
                .Replace("@EndOfTerm", item.EndOfTerm?.FormatDate())
                .Replace("@InsuredAmount", item.InsuredAmount?.FormatCurrency())
                .Replace("@BrokerName", item.BrokerName)
                .Replace("@BrokerDocument", item.BrokerDocument?.FormatCpfCnpj())
                .Replace("@InsuredName", item.InsuredName)
                .Replace("@InsuredDocument", item.InsuredDocument?.FormatCpfCnpj())
                .Replace("@TakerName", item.TakerName)
                .Replace("@TakerDocument", item.TakerDocument?.FormatCpfCnpj())
                .Replace("@ProductName", item.ProductName)
                .Replace("@CoverageName", item.CoverageName);

            //-- Envia o e-mail
            await emailSender.SendEmailAsync(string.Join(";", emails), subject, body);

        }

        public async Task SendAlertMailFinishedAsync(int success, int errors) {

            // Monta a lista de destinatários
            var emails = new List<string>();
            emails.Add("sumiya@uol.com.br");
            //var technologyUsers = await userRepository.ListAsync(appSettings.TechnologyProfileId, RecordStatusEnum.Active);
            //emails.AddRange(technologyUsers.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => x.Email).ToList());

            // Monta os textos
            var subject = appSettings.SendAlertMailFinishedSubject;
            var body = appSettings.SendAlertMailFinishedBody.Replace("@SUCCESS", success.ToString()).Replace("@ERRORS", errors.ToString());

            //-- Envia o e-mail
            await emailSender.SendEmailAsync(string.Join(";", emails), subject, body);

        }


        public async Task SendBatchSuccessMailAsync(IList<PolicyBatch> batches) {

            // Monta a lista de destinatários
            var emails = new List<string>();
            //var subscriptionUsers = await userRepository.ListAsync(appSettings.SubscriptionProfileId, RecordStatusEnum.Active);
            //emails.AddRange(subscriptionUsers.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => x.Email).ToList());
            emails.Add("sumiya@uol.com.br");
            //var technologyUsers = await userRepository.ListAsync(appSettings.TechnologyProfileId, RecordStatusEnum.Active);
            //emails.AddRange(technologyUsers.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => x.Email).ToList());
            emails.Add("ledimage@gmail.com");

            // Monta a lista de lotes para substituição
            var table = new StringBuilder("<table>");
            table.AppendLine(appSettings.PolicyBatchTableHeader);
            foreach (var batch in batches) {
                table.AppendLine(appSettings.PolicyBatchTableRow
                                    .Replace("@BatchType", batch.BatchType?.GetDescription())
                                    .Replace("@Competency", batch.Competency)
                                    .Replace("@PolicyCount", batch.PolicyCount.ToString())
                                    .Replace("@PolicyTotal", batch.PolicyTotal.FormatCurrency()));
            }
            table.AppendLine("</table>");

            // Monta os textos
            var subject = appSettings.PolicyBatchMailSubject;
            var body = appSettings.PolicyBatchMailBody.Replace("@BATCHES", table.ToString());

            //-- Envia o e-mail
            await emailSender.SendEmailAsync(string.Join(";", emails), subject, body);

        }

        public async Task SendBatchErrorMailAsync(IList<PolicyBatch> batches) {

            // Monta a lista de destinatários
            var emails = new List<string>();
            emails.Add("sumiya@uol.com.br");
            //var technologyUsers = await userRepository.ListAsync(appSettings.TechnologyProfileId, RecordStatusEnum.Active);
            //emails.AddRange(technologyUsers.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => x.Email).ToList());

            // Monta a lista de lotes para substituição
            var table = new StringBuilder("<table>");
            table.AppendLine(appSettings.PolicyBatchErrorTableHeader);
            foreach (var batch in batches) {
                table.AppendLine(appSettings.PolicyBatchErrorTableRow
                                    .Replace("@BatchType", batch.BatchType?.GetDescription())
                                    .Replace("@Competency", batch.Competency)
                                    .Replace("@PolicyCount", batch.PolicyCount.ToString())
                                    .Replace("@PolicyTotal", batch.PolicyTotal.FormatCurrency())
                                    .Replace("@Error", batch.Exception.Message));
            }
            table.AppendLine("</table>");

            // Monta os textos
            var subject = appSettings.PolicyBatchErrorMailSubject;
            var body = appSettings.PolicyBatchErrorMailBody.Replace("@ERRORS", table.ToString());

            //-- Envia o e-mail
            await emailSender.SendEmailAsync(string.Join(";", emails), subject, body);

        }

        public async Task<IList<PolicyBatchItem>> ListItemsAsync(int policyBatchId, int? expirationDays) {
            var methodParameters = new { policyBatchId, expirationDays };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyBatchRepository.ListItemsAsync(policyBatchId, expirationDays);
                var payloads = from a in items select mapper.Map<Domain.Payload.PolicyBatchItem>(a);
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

        public async Task<IList<PolicyRenovation>> ListProposalCreationPendingAsync() {
            var methodParameters = new { };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await policyRenovationRepository.ListProposalCreationPendingAsync();
                var payloads = from a in items select mapper.Map<Domain.Payload.PolicyRenovation>(a);
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem das apólices pendentes de criação de proposta: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

        public async Task SavePolicyRenovationAsync(PolicyRenovation renewal) {
            var methodParameters = new { renewal };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                //var termDays = (renewal.EndOfTerm.Value - renewal.StartOfTerm.Value).TotalDays;
                //renewal.NewStartOfTerm = renewal.EndOfTerm.Value.AddDays(1);
                //renewal.NewEndOfTerm = renewal.NewStartOfTerm.Value.AddDays(termDays);
                //renewal.NewInsuredAmount = renewal.InsuredAmount;
                //if (renewal.RenovationUpdateIndexBcCode != null) {
                //    renewal.NewInsuredAmount = await selicService.ApplyCorrectionAsync(renewal.InsuredAmount.Value, renewal.StartOfTerm.Value, renewal.EndOfTerm.Value);
                //}
                //renewal.NewInsuredObject = renewal.InsuredObject;
                //renewal.NewPolicyCode = null;

                // Chama o serviço que gera a proposta na i4pro
                var proposal = renewalApiService.SaveRenewalJudicial(renewal);
                renewal.ProposalId = proposal.ProposalId;
                renewal.NewPremiumValue = proposal.NewPremiumValue;
                renewal.NewProposalStatusId = proposal.NewProposalStatusId;

                // Atualiza o registro da renovação
                await policyRenovationRepository.UpdateAsync(mapper.Map<Domain.Entities.PolicyRenovation>(renewal));

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na criação da proposta de renovação: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }
    }
}