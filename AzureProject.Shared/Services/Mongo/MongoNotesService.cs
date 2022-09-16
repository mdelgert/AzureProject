using MongoDB.Bson;
using MongoDB.Driver;

namespace AzureProject.Shared.Services.Mongo;

public class MongoNotesService
{
    private static readonly string MongoDbConnectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.MongoDBConnectionString.ToString()) ?? string.Empty;

    private static readonly MongoClient DbClient = new(MongoDbConnectionString);

    private static readonly IMongoDatabase Database = DbClient.GetDatabase("sample_notes");

    private static readonly IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>("items");

    public static void Demo()
    {
        //DeleteAllNotes();
        //CreateNote("TestTitle", "TestNote");
        //CreateNotes();
        //UpdateNote("TestTitle", "TestTitleUpdate");
        ReadNote("TestTitleUpdate");
    }
    
    public static void UpdateNote(string oldTitle, string newTitle)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Title", oldTitle);
        var update = Builders<BsonDocument>.Update.Set("Title", newTitle);
        var results = Collection.UpdateOne(filter, update);
        Console.WriteLine(results);
    }
    
    public static void CreateNote(string title, string note)
    {
        var doc = new BsonDocument
        {
            {"Title", title},
            {"Note", note}
        };
        Collection.InsertOne(doc);
        Console.WriteLine("Insert Note Success!");
    }

    public static void CreateNotes()
    {
        for (var i = 1; i <= 3; i++)
        {
            var doc = new BsonDocument
            {
                {"Title", $"Title{i}"},
                {"Note", $"Note{i}"}
            };
            Collection.InsertOne(doc);
            Console.WriteLine($"Insert Note {i} Success!");
        }
    }

    public static void ReadNote(string title)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("Title", title);
        var result = Collection.Find(filter);
        Console.WriteLine(result);
    }
    
    public static void DeleteAllNotes()
    {
        //var filter = new BsonDocument("Title", "Title1");
        var filter = new BsonDocument();
        var deleteManyResults = Collection.DeleteMany(filter);
        Console.WriteLine(deleteManyResults);
        
    }
}
