using System;
using System.Diagnostics.CodeAnalysis;
using Application.Shared.Configuration;
using Application.Shared.Services;
using Autofac;

namespace Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleInternalServices : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PublishService>();

            //builder.RegisterType<InternalService>().As<IINternalService>.SingleInstance();
            //builder.RegisterType<Options>();
        }
    }
}

