namespace AzureProject.Tests.Helpers;

public class KeyVaultHelperTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public KeyVaultHelperTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    // [Theory]
    // [InlineData("Test3", "12345")]
    // public async Task SetSecret(string secretName, string secretValue)
    // {
    //     await KeyVaultHelper.SetSecret(secretName, secretValue);
    //     var keyVaultValue = await KeyVaultHelper.GetSecret(secretName);
    //     Assert.Equal(keyVaultValue, secretValue);
    // }
    
    // [Theory]
    // [InlineData("Test3", "12345")]
    // public async Task GetSecretTest(string secretName, string secretValue)
    // {
    //     var keyVaultValue = await KeyVaultHelper.GetSecret(secretName);
    //     Assert.Equal(keyVaultValue, secretValue);
    // }
    
    [Fact]
    public void ReadKeysJsonTest()
    {
        //https://www.newtonsoft.com/json/help/html/DeserializeObject.htm
        //https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
        //https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
        
        using var streamReader = new StreamReader("keyvault.settings.json");
        
        var json = streamReader.ReadToEnd();

        var keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        
        foreach (var key in keys)
        {
            _testOutputHelper.WriteLine($"{key.Key} {key.Value}");
        }
    }
}