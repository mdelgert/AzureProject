namespace AzureProject.Tests.Fakes;

public class WeatherFakeTests
{
    [Fact]
    public void GetTest()
    {
        var weather = WeatherFake.Get(10);
        Assert.NotEmpty(weather);
    }
}
