[assembly: FunctionsStartup(typeof(Startup))]

namespace AzureProject.FunctionApp;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<IOpenWeatherWorkflow, OpenWeatherWorkflow>();
    }
}