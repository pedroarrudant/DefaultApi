using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Application.Shared.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace DefaultAPI.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CustomStartupExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Default API",
                    Description = "Default API Description",
                    License = new OpenApiLicense {
                        Name = "Data da geracao da versao " + $"{File.GetCreationTime(Assembly.GetExecutingAssembly().Location): dd/MM/yyyy}"
                        },
                    Contact = new OpenApiContact {
                        Name = "Pedro Arruda",
                        Email = "pedro.arruda@outlook.com.br"
                        }
                    })
                );

            return services;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
        //    services.AddHttpClient("apiteste", (sp, ctx) =>
        //    {
        //        var options = sp.GetRequiredService<IOptions<ExternalApiOptions>>();

        //        ctx.BaseAddress = options.Value;
        //        ctx.Timeout = options.Value;
        //    })
        //    .AddPolicyHandler(x => 
        //    {
        //        return HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(1, retryAttempt => TimeSpan.FromSeconds(1));
        //    });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<ExternalApiOptions>(configuration.GetSection("ExternalApis"));

            return services;
        }
    }
}

