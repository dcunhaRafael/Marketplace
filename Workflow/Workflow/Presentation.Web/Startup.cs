using Domain.Payload;
using Domain.Util.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Web.Models;
using Presentation.Web.ServiceConfiguration;
using Presentation.Web.Services.Proxy;

namespace Presentation.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();

            // Caching
            services.AddLazyCache();

            services.AddScoped<IServiceFactory, ServiceFactory>();
            services.AddScoped<IComissionStatementService, ComissionStatementService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<IRenewalService, RenewalService>();

            services.Configure<EmailSettings>(Configuration.GetSection("EmailService"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
