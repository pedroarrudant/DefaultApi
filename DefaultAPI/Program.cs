using System.Reflection;
using Application.Shared.AutofacModules;
using Application.Shared.Filters;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DefaultAPI.Extensions;
using MassTransit;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => {
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();
builder.Services.AddCustomConfiguration(builder.Configuration);
builder.Services.AddHttpClientServices();
builder.Services.AddAutofac();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), Assembly.Load(new AssemblyName("Application")));
builder.Services.AddRabbitMqServices(builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleApplication()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleHandler()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleInternalServices()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ModuleMediator()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
