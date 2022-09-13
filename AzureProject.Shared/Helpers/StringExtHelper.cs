namespace AzureProject.Shared.Helpers;

public static class StringExtHelper
{
    public static string? Truncate(this string? value, int maxLength, string truncationSuffix = "…")
    {
        return value?.Length > maxLength ? value[..maxLength] + truncationSuffix : value;
    }
}

//https://stackoverflow.com/questions/2776673/how-do-i-truncate-a-net-string