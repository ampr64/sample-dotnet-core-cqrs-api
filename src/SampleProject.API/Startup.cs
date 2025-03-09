using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration.UserSecrets;
using SampleProject.API.Configuration;
using SampleProject.API.SeedWork;
using SampleProject.Application.Configuration.Emails;
using SampleProject.Application.Configuration.Validation;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Caching;
using Serilog;
using Serilog.Core;
using Serilog.Formatting.Compact;

[assembly: UserSecretsId("54e8eb06-aaa1-4fff-9f05-3ced1cb623c2")]
namespace SampleProject.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    private const string OrdersConnectionString = "OrdersConnectionString";

    private static Logger? _logger;

    public Startup(IWebHostEnvironment env)
    {
        _logger = ConfigureLogger();
        _logger.Information("Logger configured");

        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
            .AddJsonFile($"hosting.{env.EnvironmentName}.json")
            .AddUserSecrets<Startup>()
            .Build();
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        
        services.AddMemoryCache();

        services.AddSwaggerDocumentation();

        services.AddProblemDetails(x =>
        {
            x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
            x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
        });
        

        services.AddHttpContextAccessor();
        var serviceProvider = services.BuildServiceProvider();

        var children = _configuration.GetSection("Caching").GetChildren();
        var cachingConfiguration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value!));
        var emailsSettings = _configuration.GetRequiredSection(nameof(EmailsSettings)).Get<EmailsSettings>()!;
        var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
        return ApplicationStartup.Initialize(
            services, 
            _configuration.GetValue<string>(OrdersConnectionString)!,
            new MemoryCacheStore(memoryCache, cachingConfiguration),
            null,
            emailsSettings,
            _logger!,
            new ExecutionContextAccessor(serviceProvider.GetRequiredService<IHttpContextAccessor>()));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<CorrelationMiddleware>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseProblemDetails();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.UseSwaggerDocumentation();
    }

    private static Logger ConfigureLogger()
    {
        return new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{Context}] {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(new CompactJsonFormatter(), "logs/logs")
            .CreateLogger();
    }
}
