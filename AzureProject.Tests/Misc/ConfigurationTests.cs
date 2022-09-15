namespace AzureProject.Tests.Misc;

public class ConfigurationTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ConfigurationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void PrintConfig()
    {
        var config = EnvironmentHelper.GetConfig();
        _testOutputHelper.WriteLine(config);
    }
    
    // [Fact]
    // public void PrintValues()
    // {
    //     foreach (var name in Enum.GetNames(typeof(EnvironmentEnum)))
    //     {
    //         _testOutputHelper.WriteLine($"{name}={Environment.GetEnvironmentVariable(name)}");
    //     }
    //     
    //     foreach (var name in Enum.GetNames(typeof(KeyVaultEnum)))  
    //     {
    //         _testOutputHelper.WriteLine($"{name}={Environment.GetEnvironmentVariable(name)}");
    //     }
    // }
}

//https://www.c-sharpcorner.com/article/loop-through-enum-values-in-c-sharp/