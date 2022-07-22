using System.Configuration;
using System.Reflection;
using MailKit;
using MediatR;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using UMS.Application.Entities.Roles.Commands.AddRole;
using UMS.Application.Entities.Roles.Queries.GetRoles;
using UMS.Application.Mappers;
using UMS.Domain.Models;
using UMS.Infrastructure.Abstraction.Services;
using UMS.Infrastructure.Services;
using UMS.WebApi;
using UMS.WebApi.Middleware;
using UniversitySystem.Pipelines;
using IMailService = UMS.Infrastructure.Abstraction.Services.IMailService;
using MailService = UMS.Infrastructure.Services.MailService;

//
//
// using (var log = new LoggerConfiguration()
//            .WriteTo.Console()
//            .CreateLogger())
// {
//     log.Information("Hello, Serilog!");
//     log.Warning("Goodbye, Serilog.");
// }

// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console(outputTemplate:"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
//     .Filter.ByExcluding(o=>o.Level.Equals(LogEventLevel.Information))
//     //.MinimumLevel.Warning()
//     .CreateBootstrapLogger();


var builder = WebApplication.CreateBuilder(args);
ConfigureLogging();
builder.Host.UseSerilog();
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<SessionTime>("SessionTimes");
    return builder.GetEdmModel();
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UmsContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=ums;Username=postgres;Password=123456"));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(typeof(GetRolesQuery).GetTypeInfo().Assembly);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("v1", GetEdmModel()).Filter().Select().Expand().Count().OrderBy());
builder.Host.UseSerilog();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AppLoggingBehaviour<,>));
builder.Services.AddTransient<IMailService,MailService>();
builder.Services.AddSingleton<IJwtService,JwtService>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<TenantDBContextMiddleware>();

app.MapHub<MessageHub>("/chatHub");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}