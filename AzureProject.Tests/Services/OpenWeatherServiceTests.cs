namespace AzureProject.Tests.Services;

public class OpenWeatherServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public OpenWeatherServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public async Task GetWeatherTest()
    {
        var response = await OpenWeatherService.Get(44.34, 10.99);
        _testOutputHelper.WriteLine(response);
        Assert.NotNull(response);
    }
}