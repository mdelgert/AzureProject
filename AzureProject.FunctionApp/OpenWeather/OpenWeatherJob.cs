namespace AzureProject.FunctionApp.OpenWeather;

public static class OpenWeatherJob
{
    private const string NamePrefix = "OpenWeatherJob";
    
    [FunctionName(NamePrefix)]
    public static async Task Run([TimerTrigger("%CronOpenWeatherJob%")]TimerInfo myTimer, ILogger log)
    {
        try
        {
            log.LogInformation("{Name} function executed at: {Now}", NamePrefix, DateTime.Now);
            var openWeatherLatitude = double.Parse(Environment.GetEnvironmentVariable("OpenWeatherLatitude") ?? "0");
            var openWeatherLongitude = double.Parse(Environment.GetEnvironmentVariable("OpenWeatherLongitude") ?? "0");
            var response = await OpenWeatherService.Get(openWeatherLatitude, openWeatherLongitude);
            log.LogInformation("Response:{Response}", response);
        }
        catch (Exception exception)
        {
            log.LogCritical("{NamePrefix} Error: {Exception}", NamePrefix, exception.ToString());
        }
    }
}