using Flurl.Http.Configuration;
using InsuranceApi.Application;
using InsuranceApi.Database.Dapper.Ebix.Implementations;
using InsuranceApi.Domain.Common.HttpClient;
using InsuranceApi.Domain.Common.Log;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Common;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Infra.CrossCutting.Identity.Extensions;
using InsuranceApi.Services.Rest.Implementations;
using InsuranceApi.Services.Soap.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InsuranceApi.Infra.CrossCutting.IoC {
    public static class DependencyInjectionConfig {

        public static IServiceCollection ResolveDependencias(this IServiceCollection services) {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IFlurlClientFactory, PerBaseUrlFlurlClientFactory>();

            services.AddScoped<IHttpServiceClientFactory, HttpServiceClientFactory>();

            services.AddScoped<ILogger, Log>();

            services.AddScoped<IUser, AspNetUser>();

            #region Application

            services.AddScoped<IAppParameterApplication, AppParameterApplication>();
            services.AddScoped<IAuditApplication, AuditApplication>();
            services.AddScoped<IBrokerApplication, BrokerApplication>();
            services.AddScoped<ICivilCourtApplication, CivilCourtApplication>();
            services.AddScoped<ICoverageApplication, CoverageApplication>();
            services.AddScoped<ICrivoApplication, CrivoApplication>();
            services.AddScoped<IDigitalSignatureApplication, DigitalSignatureApplication>();
            services.AddScoped<IInsuredApplication, InsuredApplication>();
            services.AddScoped<IInsuredObjectApplication, InsuredObjectApplication>();
            services.AddScoped<ILaborCourtApplication, LaborCourtApplication>();
            services.AddScoped<ILegalRecourseTypeApplication, LegalRecourseTypeApplication>();
            services.AddScoped<ILoadedTakerApplication, LoadedTakerApplication>();
            services.AddScoped<IPersonApplication, PersonApplication>();
            services.AddScoped<IPolicyApplication, PolicyApplication>();
            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<IProposalAppealApplication, ProposalAppealApplication>();
            services.AddScoped<IProposalApplication, ProposalApplication>();
            services.AddScoped<IProposalBiddingApplication, ProposalBiddingApplication>();
            services.AddScoped<IProposalContractApplication, ProposalContractApplication>();
            services.AddScoped<ISalesChannelApplication, SalesChannelApplication>();
            services.AddScoped<ITakerApplication, TakerApplication>();
            services.AddScoped<ITermTypeApplication, TermTypeApplication>();
            services.AddScoped<IZipCodeApplication, ZipCodeApplication>();

            #endregion

            #region serviços

            services.AddScoped<IBrokerService, BrokerService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICoverageService, CoverageService>();
            services.AddScoped<ICrivoService, CrivoService>();
            services.AddScoped<IInsuredService, InsuredService>();
            services.AddScoped<IPolicyService, PolicyService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<ISignatureDigitalService, ProposalSignatureService>();
            services.AddScoped<ITakerService, TakerService>();
            services.AddScoped<IZipCodeService, ZipCodeService>();
            services.AddScoped<IBCDataService, BCDataService>();

            #endregion

            #region Dao Ebix

            services.AddScoped<IAppAuditorshipDao, AppAuditorshipDao>();
            services.AddScoped<IAppParameterDao, AppParameterDao>();
            services.AddScoped<IBrokerDao, BrokerDao>();
            services.AddScoped<ICivilCourtDao, CivilCourtDao>();
            services.AddScoped<ICoverageDao, CoverageDao>();
            services.AddScoped<IInsuredObjectDao, InsuredObjectDao>();
            services.AddScoped<ILaborCourtDao, LaborCourtDao>();
            services.AddScoped<ILegalRecourseTypeDao, LegalRecourseTypeDao>();
            services.AddScoped<ILoadedTakerDao, LoadedTakerDao>();
            services.AddScoped<IPersonDao, PersonDao>();
            services.AddScoped<IProductDao, ProductDao>();
            services.AddScoped<IProposalDao, ProposalDao>();
            services.AddScoped<IProposalSignatureDao, ProposalSignatureDao>();
            services.AddScoped<IRateDao, RateDao>();
            services.AddScoped<ISalesChannelDao, SalesChannelDao>();
            services.AddScoped<ITakerDao, TakerDao>();
            services.AddScoped<ITakerAppealFeeDao, TakerAppealFeeDao>();
            services.AddScoped<ITermTypeDao, TermTypeDao>();

            #endregion

            #region Dao Marketplace

            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IAuditoriaDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.AuditoriaDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IBrokerDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.BrokerDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IBureauDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.BureauDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IClaimantsDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ClaimantsDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.ICommercialEntityStructureDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.CommercialEntityStructureDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.ICoverageDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.CoverageDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IInsuredContactDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.InsuredContactDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IInsuredDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.InsuredDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.ILegalRecourseTypeParameterDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.LegalRecourseTypeParameterDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IPaymentInstallmentsDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.PaymentInstallmentsDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProductDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ProductDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ProposalDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalInsuredObjectDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ProposalInsuredObjectDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalParcDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ProposalParcDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalStatusDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ProposalStatusDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IProposalTypeDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.ProposalTypeDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IRecursalPolicyExpDateDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.RecursalPolicyExpDateDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IRegistryBureauDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.RegistryBureauDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.ITakerDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.TakerDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IUserDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.UserDao>();
            services.AddScoped<InsuranceApi.Domain.Interfaces.Dao.Marketplace.IWarrantyOptionsDao, InsuranceApi.Database.Dapper.Marketplace.Implementations.WarrantyOptionsDao>();

            #endregion

            return services;
        }

    }
}
