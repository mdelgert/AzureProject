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
