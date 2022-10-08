﻿using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using Application.Features.GetPetsByName.Repositories;
using Application.Shared.Configuration;
using Autofac;
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
        }
    }
}

