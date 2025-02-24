using FunctionAppTest.DAL;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("template.local.settings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
    })
    .ConfigureServices((config, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(config.Configuration.GetValue<string>("Values:SqlConnectionString"))
            );
    })
    .Build();

host.Run();
