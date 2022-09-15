namespace AzureProject.Tests.Helpers;

public class CryptoHelperTests
{
    [Theory]
    [InlineData("Test", "&uJo61kLMHeMKTx56hXpD5O%0Tz#gkXZ", "0FOdlXk&Bt5z8MrF")]
    public Task CryptoHelperTest1(string plainText, string secret, string salt)
    {
        var encryptedMessage = CryptoHelper.EncryptAesString(plainText, secret, salt);
        var decryptedMessage = CryptoHelper.DecryptAesString(encryptedMessage, secret, salt);
        Assert.NotEqual(plainText, encryptedMessage);
        Assert.Equal(plainText, decryptedMessage);
        return Task.CompletedTask;
    }
}