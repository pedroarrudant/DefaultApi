using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleInternalServices : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<InternalService>().As<IINternalService>.SingleInstance();
            //builder.RegisterType<Options>();
        }
    }
}

