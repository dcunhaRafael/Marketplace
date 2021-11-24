using Application.Interfaces.Services;
using Application.Services;
using Domain.Util.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IoC.Services {
    public static class ServicesIoC {
        public static void ApplicationServicesIoC(this IServiceCollection services) {
            services.AddScoped<IAppServiceService, AppServiceService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IBrokerService, BrokerService>();
            services.AddScoped<IComissionStatementService, ComissionStatementService>();
            services.AddScoped<ICoverageService, CoverageService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IInsuredService, InsuredService>();
            services.AddScoped<IOccurrenceTypeService, OccurrenceTypeService>();
            services.AddScoped<IOccurrenceValidationRuleService, OccurrenceValidationRuleService>();
            services.AddScoped<IPolicyBatchService, PolicyBatchService>();
            services.AddScoped<IPolicyBatchConfigurationService, PolicyBatchConfigurationService>();
            services.AddScoped<IPolicyRenovationService, PolicyRenovationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProposalOccurrenceService, ProposalOccurrenceService>();
            services.AddScoped<IRefusalReasonService, RefusalReasonService>();
            services.AddScoped<ISelicService, SelicService>();
            services.AddScoped<ITakerService, TakerService>();
            services.AddScoped<IUpdateIndexService, UpdateIndexService>();
            services.AddScoped<IUserService, UserService>();

            services.AddTransient<Domain.Util.Mail.IEmailSender, EmailSender>();
        }
    }
}
