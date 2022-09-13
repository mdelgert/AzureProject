namespace AzureProject.Tests.Helpers;

public class KeyVaultHelperTests
{
    private readonly ConfigurationModel _configuration;
    private readonly ITestOutputHelper _testOutputHelper;
    private const string SecretName = "Test";
    private string _secretValue = "12345";

    public KeyVaultHelperTests(ConfigurationModel configurationModel, ITestOutputHelper testOutputHelper)
    {
        _configuration = configurationModel;
        _testOutputHelper = testOutputHelper;
    }
    
    // [Fact]
    // public async Task SetKeyTest()
    // {
    //     await KeyVaultHelper.SetSecret(_configuration.AzureKeyVault, SecretName, _secretValue);
    // }
    
    [Fact]
    public async Task GetKeyTest()
    {
        _secretValue = await KeyVaultHelper.GetSecret(_configuration.AzureKeyVault, SecretName);
        Assert.NotNull(_secretValue);
        _testOutputHelper.WriteLine(_secretValue);
    }
}
