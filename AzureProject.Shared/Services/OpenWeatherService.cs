namespace AzureProject.Shared.Services;

public class OpenWeatherService
{
    public async Task Get()
    {
        var options = new RestClientOptions("https://api.myorg.com")
        {
            ThrowOnAnyError = true,
            Timeout = 1000
        };
        var client = new RestClient(options);
    }
}

//https://openweathermap.org/api/geocoding-api
//https://openweathermap.org/current
//https://www.latlong.net/