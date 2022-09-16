using MongoDB.Bson;
using MongoDB.Driver;

namespace AzureProject.Shared.Services.Mongo;

public static class MongoGeneralService
{
    private static readonly string MongoDbConnectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.MongoDBConnectionString.ToString()) ?? string.Empty;

    private static readonly MongoClient DbClient = new(MongoDbConnectionString);

    public static Task<string> GetDatabases()
    {
        var dbList = DbClient.ListDatabases().ToList();
        var dotNetObjList = dbList.ConvertAll(BsonTypeMapper.MapToDotNetValue);
        var json = JsonConvert.SerializeObject(dotNetObjList, Formatting.Indented);
        Console.WriteLine(json);
        return Task.FromResult(json);
    }
}
