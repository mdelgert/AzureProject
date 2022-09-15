[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureProject.FunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        KeyVaultHelper.SetEnvironment();
        builder.Services.AddSingleton<IOpenWeatherWorkflow, OpenWeatherWorkflow>();
    }
}