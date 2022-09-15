namespace AzureProject.Shared.Services;

public static class OpenWeatherService
{
    // For temperature in Fahrenheit use units=imperial
    // For temperature in Celsius use units=metric
    // Temperature in Kelvin is used by default, no need to use units parameter in API call
    private const string Units = "imperial";
    private const string BaseUrl = "https://api.openweathermap.org";

    private static readonly string OpenWeatherKey =
        Environment.GetEnvironmentVariable(KeyVaultEnum.OpenWeatherKey.ToString()) ?? string.Empty;

    public static async Task<string?> Get(double lat, double lon)
    {
        CancellationToken cancellationToken = default;
        var options =
            new RestClientOptions(
                $"{BaseUrl}/data/2.5/weather?lat={lat}&lon={lon}&units={Units}&appid={OpenWeatherKey}")
            {
                ThrowOnAnyError = true
            };
        var client = new RestClient(options);
        var request = new RestRequest();
        var response = await client.GetAsync(request, cancellationToken);
        return response.Content;
    }
}

//https://openweathermap.org/current#data
//https://openweathermap.org/api/geocoding-api
//https://openweathermap.org/current
//https://www.latlong.net/