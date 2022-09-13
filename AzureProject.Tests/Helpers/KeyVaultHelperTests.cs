namespace AzureProject.Tests.Helpers;

public class KeyVaultHelperTests
{
    [Theory]
    [InlineData("Test", "12345")]
    public async Task KeyVaultHelperTest(string secretName, string secretValue)
    {
        var keyVaultValue = await KeyVaultHelper.GetSecret(secretName);
        Assert.Equal(keyVaultValue, secretValue);
    }
}