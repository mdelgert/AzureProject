namespace AzureProject.Tests.Helpers;

public class ConfigurationHelperTests
{
    private readonly ConfigurationModel _configuration;
    private readonly ITestOutputHelper _testOutputHelper;

    public ConfigurationHelperTests(ConfigurationModel configurationModel, ITestOutputHelper testOutputHelper)
    {
        _configuration = configurationModel;
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void TestValidEnvironment()
    {
        _testOutputHelper.WriteLine(_configuration.AzureKeyVault);
        Assert.NotNull(_configuration.AzureKeyVault);   
    }
}
