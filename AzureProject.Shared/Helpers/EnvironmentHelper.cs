namespace AzureProject.Shared.Helpers;

public static class EnvironmentHelper
{
    public static void SetupProfiles(string settingsPath)
    {
        using var file = File.OpenText(settingsPath);
        var reader = new JsonTextReader(file);
        var jObject = JObject.Load(reader);
        var variables = (jObject
                .GetValue("profiles") ?? throw new InvalidOperationException())
            .SelectMany(profiles => profiles.Children())
            .SelectMany(profile => profile.Children<JProperty>())
            .Where(prop => prop.Name == "environmentVariables")
            .SelectMany(prop => prop.Value.Children<JProperty>())
            .ToList();

        foreach (var variable in variables)
            Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
    }

    public static void SetupValues(string settingsPath)
    {
        using var file = File.OpenText(settingsPath);
        var reader = new JsonTextReader(file);
        var jObject =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(
                JObject.Load(reader).GetValue("Values")!.ToString());
        if (jObject == null) return;
        foreach (var key in jObject.Keys) Environment.SetEnvironmentVariable(key, jObject[key]);
    }
}