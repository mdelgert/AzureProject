namespace AzureProject.Shared.Workflow;

public interface IOpenWeatherWorkflow
{
    Task Process();
}

public class OpenWeatherWorkflow : IOpenWeatherWorkflow
{
    private readonly ILogger<OpenWeatherWorkflow> _log;

    public OpenWeatherWorkflow(ILogger<OpenWeatherWorkflow> log)
    {
        _log = log;
    }

    public async Task Process()
    {
        const string namePrefix = "OpenWeatherWorkflow";

        try
        {
            _log.LogInformation("{Name} executed at: {Now}", namePrefix, DateTime.Now);
            var openWeatherLatitude = double.Parse(Environment.GetEnvironmentVariable("OpenWeatherLatitude") ?? "0");
            var openWeatherLongitude = double.Parse(Environment.GetEnvironmentVariable("OpenWeatherLongitude") ?? "0");
            var response = await OpenWeatherService.Get(openWeatherLatitude, openWeatherLongitude);
            _log.LogInformation("{Name} Response:{Response}", namePrefix, response);
        }
        catch (Exception exception)
        {
            _log.LogCritical("{Name} Error: {Exception}", namePrefix, exception.ToString());
        }
    }
}

//https://www.domstamand.com/common-azure-functions-in-csharp-quick-faqs-and-tips/

//Example host.json
// "logLevel": {
//     "{AzureProject.Shared.Workflow}.{OpenWeatherWorkflow}": "Information",
//     "default": "Information"
// }

//Or
//_log = loggerFactory.CreateLogger(LogCategories.CreateFunctionUserCategory(GetType().FullName));