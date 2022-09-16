using MongoDB.Bson;
using MongoDB.Driver;

namespace AzureProject.Shared.Services.Mongo;

public static class MongoPlanetsService
{
    private static readonly string MongoDbConnectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.MongoDBConnectionString.ToString()) ?? string.Empty;

    private static readonly MongoClient DbClient = new(MongoDbConnectionString);

    private static readonly IMongoDatabase Database = DbClient.GetDatabase("sample_guides");

    private static readonly IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>("planets");

    public static Task GetPlanets()
    {
        var documents = Collection.FindAsync(new BsonDocument()).Result.ToList();
        
        foreach (var doc in documents)
        {
            //Console.WriteLine(doc.ToString());
            Console.WriteLine($"name={doc.GetElement("name")} orderFromSun={doc.GetElement("orderFromSun")}");
        }

        return Task.CompletedTask;
    }

    public static Task<string> GetPlanetsJson()
    {
        var documents = Collection.FindAsync(new BsonDocument()).Result.ToList();
        var dotNetObjList = documents.ConvertAll(BsonTypeMapper.MapToDotNetValue);
        var json = JsonConvert.SerializeObject(dotNetObjList, Formatting.Indented);
        Console.WriteLine(json);
        return Task.FromResult(json);
    }
}

//https://zetcode.com/csharp/mongodb/
//https://stackoverflow.com/questions/18068772/mongodb-c-sharp-how-to-work-with-bson-document
//https://stackoverflow.com/questions/27132968/convert-mongodb-bsondocument-to-valid-json-in-c-sharp