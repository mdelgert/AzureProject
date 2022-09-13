namespace AzureProject.Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
    {
        EnvironmentHelper.SetupValues("local.settings.json");
        var configuration = ConfigurationHelper.Get();
        services.AddSingleton(_ => configuration);
    }
}

//https://stackoverflow.com/questions/32257640/how-do-i-handle-async-operations-in-startup-configure
//https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-3/