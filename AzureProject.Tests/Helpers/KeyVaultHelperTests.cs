namespace AzureProject.Tests.Helpers;

public class KeyVaultHelperTests
{
    // [Theory]
    // [InlineData("Test3", "12345")]
    // public async Task SetSecret(string secretName, string secretValue)
    // {
    //     await KeyVaultHelper.SetSecret(secretName, secretValue);
    //     var keyVaultValue = await KeyVaultHelper.GetSecret(secretName);
    //     Assert.Equal(keyVaultValue, secretValue);
    // }
    
    [Theory]
    [InlineData("Test3", "12345")]
    public async Task GetSecretTest(string secretName, string secretValue)
    {
        var keyVaultValue = await KeyVaultHelper.GetSecret(secretName);
        Assert.Equal(keyVaultValue, secretValue);
    }
}