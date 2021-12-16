using Infrastructure.Dapper.MSSQL.DBConfiguration;
using Infrastructure.Dapper.MSSQL.Repositories;
using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC.Repository {
    public static class RepositoryIoC {
        public static void InfrastructureRepositoryIoC(this IServiceCollection services) {
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();

            services.AddScoped<IAppServiceRepository, AppServiceRepository>();
            services.AddScoped<IAppServiceLogRepository, AppServiceLogRepository>();
            services.AddScoped<IAuditRepository, AuditRepository>();
            services.AddScoped<IBrokerRepository, BrokerRepository>();
            services.AddScoped<IComissionStatementStatusRepository, ComissionStatementStatusRepository>();
            services.AddScoped<ICoverageRepository, CoverageRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IFixedDomainRepository, FixedDomainRepository>();
            services.AddScoped<IInsuredRepository, InsuredRepository>();
            services.AddScoped<IOccurrenceTypeRepository, OccurrenceTypeRepository>();
            services.AddScoped<IOccurrenceTypeDocumentRepository, OccurrenceTypeDocumentRepository>();
            services.AddScoped<IOccurrenceTypeLiberationUserRepository, OccurrenceTypeLiberationUserRepository>();
            services.AddScoped<ILatePaymentRepository, LatePaymentRepository>();
            services.AddScoped<IPendingInstallmentRepository, PendingInstallmentRepository>();
            services.AddScoped<IPolicyBatchRepository, PolicyBatchRepository>();
            services.AddScoped<IPolicyBatchConfigurationRepository, PolicyBatchConfigurationRepository>();
            services.AddScoped<IPolicyBatchConfigurationMailDestinationRepository, PolicyBatchConfigurationMailDestinationRepository>();
            services.AddScoped<IPolicyBatchConfigurationMailRepository, PolicyBatchConfigurationMailRepository>();
            services.AddScoped<IPolicyRenovationRepository, PolicyRenovationRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IProposalRepository, ProposalRepository>();
            services.AddScoped<IProposalOccurrenceRepository, ProposalOccurrenceRepository>();
            services.AddScoped<IProposalOccurrenceDocumentRepository, ProposalOccurrenceDocumentRepository>();
            services.AddScoped<IProposalOccurrenceHistoryRepository, ProposalOccurrenceHistoryRepository>();
            services.AddScoped<IRefusalReasonRepository, RefusalReasonRepository>();
            services.AddScoped<ISelicDailyRepository, SelicDailyRepository>();
            services.AddScoped<ISelicMonthlyRepository, SelicMonthlyRepository>();
            services.AddScoped<ITakerRepository, TakerRepository>();
            services.AddScoped<IUpdateIndexRepository, UpdateIndexRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
