using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using InsuranceApi.Domain.BusinessObjects.AppSettings;
using InsuranceApi.Infra.CrossCutting.Identity.Configuration;
using InsuranceApi.Infra.CrossCutting.IoC;
using InsuranceApi.Web.Configuration;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace InsuranceApi.Web {
    public class Startup {
        public IConfiguration Configuration { get; }
        public Startup() {
            Configuration = AppsettingConfig.BuilderConfig().Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });
            services.AddSwaggerConfig();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());         

            services.AddIdentityConfiguration(Configuration);
                        
            services.AddServiceSettingConfigureDictionary<ServiceSettings>(Configuration);

            services.ResolveDependencias();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(Log.Logger);

            services.AddControllers();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger) { 
            if (env.IsDevelopment()) {

                logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHttpsRedirection();               
            }

            app.UseStaticFiles();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseSwaggerConfig();        

            app.UseMvcConfiguration();
        }
    }
}
