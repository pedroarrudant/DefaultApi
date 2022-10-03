using System;
using System.Diagnostics.CodeAnalysis;
using Autofac;

namespace Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleApplication : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //_ = builder.Register(container =>
            //{
            //    var dataBaseOptions = container.Resolve<IOptionsSnapshot<DatabaseOptions>>();
            //    var logger = container.Resolve<ILogger<Repository>>();

            //    return new GetPetsByNameRepository(dataBaseOptions.Connection, logger);
            //}).As<IGetPetsByNameRepository>();
        }
    }
}

