namespace AzureProject.Shared.Helpers;

public static class ConfigurationHelper
{
    public static ConfigurationModel Get()
    {
        var configuration = new ConfigurationModel
        {
            AzureKeyVault = Environment.GetEnvironmentVariable("AzureKeyVault") ?? string.Empty
        };

        configuration = KeyVaultHelper.MapValues(configuration);
            
        return configuration;
    }
}
