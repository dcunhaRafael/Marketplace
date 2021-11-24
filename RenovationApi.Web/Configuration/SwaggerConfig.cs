using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RenewalApi.Web.Configuration {
    public static class SwaggerConfig {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services) {


            services.AddSwaggerGen(c => {
             
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Renovação Seguro Garantia", Version = "v1" });

                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    In = ParameterLocation.Header,
                //    Description = "Insira o token JWT desta maneira: Bearer seu token"
                //});

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "Bearer"
                //                }
                //            },
                //            new string[] {}
                //    }
                //});
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app) {

            app.UseSwagger();

            app.UseSwaggerUI(
                 options => {
                     
                     options.SwaggerEndpoint("../swagger/v1/swagger.json", "v1");
                     options.InjectStylesheet("../swagger-ui/css/custom.css");
                     options.InjectJavascript("../swagger-ui/js/custom.js");
                 });

            return app;
        }
    }
}
