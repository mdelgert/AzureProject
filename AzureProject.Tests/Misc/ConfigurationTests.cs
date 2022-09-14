namespace AzureProject.Tests.Misc;

public class ConfigurationTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ConfigurationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void PrintValues()
    {
        _testOutputHelper.WriteLine($"AzureKeyVault={Environment.GetEnvironmentVariable("AzureKeyVault")}");
        _testOutputHelper.WriteLine($"OpenWeatherKey={Environment.GetEnvironmentVariable("OpenWeatherKey")}");
        _testOutputHelper.WriteLine($"StorageConnectionString={Environment.GetEnvironmentVariable("StorageConnectionString")}");
        _testOutputHelper.WriteLine($"CosmosEndpointUri={Environment.GetEnvironmentVariable("CosmosEndpointUri")}");
        _testOutputHelper.WriteLine($"CosmosPrimaryKey={Environment.GetEnvironmentVariable("CosmosPrimaryKey")}");
    }
}