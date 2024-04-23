using AutomationReport.Controllers;
using AutomationReport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepoDb;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        configuration.Sources.Clear();
        var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        configuration.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
        configuration.AddConfiguration(configuration.Build());

    })
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IExcelReaderService, EPPlusReaderService>();
        services.AddTransient<IFileWatcherService, ExcelFileWatcherService>();
        services.AddTransient<IReaderAppController, ExcelReaderAppController>();
        GlobalConfiguration.Setup().UseSqlServer();
    })
    .Build();

var startup = host.Services.GetRequiredService<IReaderAppController>();
await startup!.RunProgramAsync();
