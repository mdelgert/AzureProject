namespace AzureProject.Shared.Fakes;

public static class WeatherFake
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public static List<WeatherModel> Get(int batchSize)
    {
        var weathers = new List<WeatherModel>();
        var startDate = DateTime.UtcNow;
        
        for (var i = 1; i <= batchSize; i++)
        {
            var weather = new WeatherModel
            {
                Date = startDate.AddDays(-i),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            weathers.Add(weather);
        }
        
        return weathers;
    }
}