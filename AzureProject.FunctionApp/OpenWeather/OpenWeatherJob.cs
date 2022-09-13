namespace AzureProject.FunctionApp.OpenWeather;

public class OpenWeatherJob
{
    private readonly IOpenWeatherWorkflow _openWeatherWorkflow;

    public OpenWeatherJob(IOpenWeatherWorkflow openWeatherWorkflow)
    {
        _openWeatherWorkflow = openWeatherWorkflow;
    }

    [FunctionName("OpenWeatherJob")]
    public async Task Run([TimerTrigger("%CronOpenWeatherJob%")] TimerInfo myTimer, ILogger log)
    {
        await _openWeatherWorkflow.Process();
    }
}