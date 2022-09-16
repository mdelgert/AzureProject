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

    public static void SetupKeyVaultSettings(string settingsPath)
    {
        using var streamReader = new StreamReader(settingsPath);
        var json = streamReader.ReadToEnd();

        var keys = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

        if (keys == null) return;
        foreach (var key in keys)
            Environment.SetEnvironmentVariable(key.Key, key.Value);
    }
    
    public static string GetConfig()
    {
        var config = Enum.GetNames(typeof(EnvironmentEnum))
            .ToDictionary(name => name, Environment.GetEnvironmentVariable);

        foreach (var name in Enum.GetNames(typeof(KeyVaultEnum)))
            config.Add(name, Environment.GetEnvironmentVariable(name));

        var json = JsonConvert.SerializeObject(config);

        return json;
    }
}

//https://www.newtonsoft.com/json/help/html/DeserializeObject.htm
//https://stackoverflow.com/questions/13297563/read-and-parse-a-json-file-in-c-sharp
//https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net
//https://stackoverflow.com/questions/294138/merging-dictionaries-in-c-sharp
//https://www.newtonsoft.com/json/help/html/CreateJsonDynamic.htm