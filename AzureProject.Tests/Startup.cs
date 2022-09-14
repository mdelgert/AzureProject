namespace AzureProject.Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
    {
        EnvironmentHelper.SetupValues("local.settings.json");
        KeyVaultHelper.SetEnvironment();
        services.AddSingleton<IOpenWeatherWorkflow, OpenWeatherWorkflow>();
        services.AddLogging(loggerBuilder =>
        {
            loggerBuilder.ClearProviders();
            loggerBuilder.AddConsole();
            loggerBuilder.AddFile("Serilog\\debug.log");
        });
    }
}

//https://stackoverflow.com/questions/32257640/how-do-i-handle-async-operations-in-startup-configure
//https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-3/