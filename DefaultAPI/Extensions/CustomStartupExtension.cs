using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Application.Entrypoint.Consumer;
using Application.Shared.Configuration;
using MassTransit;
using MassTransit.Metadata;
using MassTransit.Util;
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
            services.Configure<DatabaseOptions>(configuration.GetSection("SqlServer"));

            return services;
        }

        public static IServiceCollection AddRabbitMqServices(this IServiceCollection services, IConfiguration configuration)
        {
            var types = AssemblyTypeCache.FindTypes(new[] { Assembly.Load(new AssemblyName("Application")) },
                                                            TypeMetadataCache.HasConsumerInterfaces).GetAwaiter().GetResult();

            var consumers = types.FindTypes(TypeClassification.Concrete | TypeClassification.Closed).ToList();

            var useInMemory = configuration.GetValue<bool>("RabbitMQ:UseInMemory");
            var port = configuration.GetValue<string>("RabbitMQ:Port");
            var server = configuration.GetValue<string>("RabbitMQ:Server");
            var vHost = configuration.GetValue<string>("RabbitMQ:VHost");
            var Uri = configuration.GetValue<string>("RabbitMQ:URI");
            var user = configuration.GetValue<string>("RabbitMQ:User");
            var password = configuration.GetValue<string>("RabbitMQ:Password");

            if (useInMemory)
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumers(consumers.ToArray());
                    x.UsingInMemory((context, cfg) =>
                    {
                        //cfg.TransportCurrencyLimit = 10;
                        cfg.ConfigureEndpoints(context);
                    });
                });
            }
            else
            {
                services.AddMassTransit(x =>
                {
                    x.AddConsumer<InsertPetConsumer>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(new Uri("amqp://localhost:5672"), h =>
                        {
                            h.Username(user);
                            h.Password(password);
                        });

                        cfg.ReceiveEndpoint("insertPetQueue", e =>
                        {
                            e.ConfigureConsumer<InsertPetConsumer>(context);
                        });
                    });
                });
            }

            return services;
        }
    }
}

