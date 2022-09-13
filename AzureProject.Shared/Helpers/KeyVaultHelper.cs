namespace AzureProject.Shared.Helpers;

public static class KeyVaultHelper
{
    private static readonly string AzureKeyVault = Environment.GetEnvironmentVariable("AzureKeyVault") ?? string.Empty;

    public static async Task SetSecret(string secretName, string secretValue)
    {
        var kvUri = $"https://{AzureKeyVault}.vault.azure.net";
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        await client.SetSecretAsync(secretName, secretValue);
    }

    public static async Task<string> GetSecret(string secretName)
    {
        var kvUri = $"https://{AzureKeyVault}.vault.azure.net";
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync(secretName);
        return secret.Value.Value;
    }

    public static async Task DeleteSecret(string secretName)
    {
        var kvUri = $"https://{AzureKeyVault}.vault.azure.net";
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        await client.StartDeleteSecretAsync(secretName);
    }
}