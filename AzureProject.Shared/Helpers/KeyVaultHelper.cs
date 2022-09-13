namespace AzureProject.Shared.Helpers;

public static class KeyVaultHelper
{
    public static async Task SetSecret(string keyVaultName, string secretName, string secretValue)
    {
        var kvUri = $"https://{keyVaultName}.vault.azure.net";
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        await client.SetSecretAsync(secretName, secretValue);
    }
    
    public static async Task<string> GetSecret(string keyVaultName, string secretName)
    {
        var kvUri = $"https://{keyVaultName}.vault.azure.net";
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync(secretName);
        return secret.Value.Value;
    }
    
    public static async Task DeleteSecret(string keyVaultName, string secretName)
    {
        var kvUri = $"https://{keyVaultName}.vault.azure.net";
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        await client.StartDeleteSecretAsync(secretName);
    }
}
