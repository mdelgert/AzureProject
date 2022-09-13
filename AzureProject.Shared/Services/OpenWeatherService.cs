namespace AzureProject.Shared.Services;

public static class OpenWeatherService
{
    private const string BaseUrl = "https://api.openweathermap.org";
    public static async Task<string?> Get(double lat, double lon)
    {
        var appid = await KeyVaultHelper.GetSecret("OpenWeatherKey");
        CancellationToken cancellationToken = default;
        var options = new RestClientOptions($"{BaseUrl}/data/2.5/weather?lat={lat}&lon={lon}&appid={appid}")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);
        var request = new RestRequest();
        var response = await client.GetAsync(request, cancellationToken);
        return response.Content;
    }
}

//https://openweathermap.org/api/geocoding-api
//https://openweathermap.org/current
//https://www.latlong.net/