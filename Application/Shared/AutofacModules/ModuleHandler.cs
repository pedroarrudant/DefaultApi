using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Application.Features.GetPetsByName.UseCase;
using Application.Shared.Middlewares;
using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace Application.Shared.AutofacModules
{
    [ExcludeFromCodeCoverage]
    public class ModuleHandler : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ExceptionHandler<>)).As(typeof(IRequestExceptionAction<,>));

            builder.RegisterAssemblyTypes(typeof(GetPetsByNameUseCaseHandler).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}

