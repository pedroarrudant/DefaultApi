using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Application.Entrypoint.Observer;
using Application.Shared.Configuration;
using Application.Shared.Repositories;
using Application.Shared.Services;
using Autofac;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleApplication : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            _ = builder.Register(container =>
            {
                var dataBaseOptions = container.Resolve<IOptionsSnapshot<DatabaseOptions>>();
                var logger = container.Resolve<ILogger<PetsRepository>>();

                return new PetsRepository(new SqlConnection(dataBaseOptions.Value.ConnectionString), logger);
            }).As<IPetsRepository>();

            _ = builder.Register(container =>
            {
                var logger = container.Resolve<ILogger<ReceiveObserver>>();
                var publish = container.Resolve<PublishService>();

                return new ReceiveObserver(logger, publish);
            }).As<IReceiveObserver>();
        }
    }
}

