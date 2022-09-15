﻿namespace AzureProject.ConsoleApp;

public static class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        EnvironmentHelper.SetupValues("local.settings.json");
        KeyVaultHelper.SetEnvironment();
        services.AddLogging(loggerBuilder =>
        {
            loggerBuilder.ClearProviders();
            loggerBuilder.AddConsole();
            loggerBuilder.AddFile("Serilog\\debug.log");
        });
    }
}