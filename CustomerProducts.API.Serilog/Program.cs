using CustomerProducts.API;
using Serilog;
using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

ILogger logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

builder.Services.AddSingleton(logger);

var app = builder
       .ConfigureServices()
       .ConfigurePipeline();

await app.ResetDatabaseAsync();

// run the app
app.Run();
