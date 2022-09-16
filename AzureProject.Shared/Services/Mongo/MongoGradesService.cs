using MongoDB.Bson;
using MongoDB.Driver;

namespace AzureProject.Shared.Services.Mongo;

public static class MongoGradesService
{
    private static readonly string MongoDbConnectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.MongoDBConnectionString.ToString()) ?? string.Empty;

    private static readonly MongoClient DbClient = new(MongoDbConnectionString);

    private static readonly IMongoDatabase Database = DbClient.GetDatabase("sample_training");

    private static readonly IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>("grades");

    public static async Task Demo()
    {
        //await GetDatabases();
        //await CreateExample();
        await ReadExample();
    }

    private static Task ReadExample()
    {
        // *********************************
        // Read Operations
        // *********************************

        // Find first record in the database
        var firstDocument = Collection.FindAsync(new BsonDocument()).Result.FirstOrDefault();

        Console.WriteLine("\n**********\n");
        Console.WriteLine(firstDocument.ToString());

        // Find a specific document with a filter
        var filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
        var studentDocument = Collection.Find(filter).FirstOrDefault();
        Console.WriteLine("\n**********\n");
        Console.WriteLine(studentDocument.ToString());

        // Find all documents with an exam score equal or above 95 as a list
        var highExamScoreFilter = Builders<BsonDocument>.Filter
            .ElemMatch<BsonValue>("scores",
                new BsonDocument
                {
                    {"type", "exam"},
                    {"score", new BsonDocument {{"$gte", 95}}}
                });
        var highExamScores = Collection.Find(highExamScoreFilter).ToList();
        Console.WriteLine("\n**********\n");
        Console.WriteLine(highExamScores);

        // Find all documents with an exam score equal
        // or above 95 as an iterable

        var cursor = Collection.Find(highExamScoreFilter).ToCursor();
        Console.WriteLine("\n**********\n");
        Console.WriteLine("\nHigh Scores Iterable\n");
        Console.WriteLine("\n**********\n");
        foreach (var cursorDocument in cursor.ToEnumerable()) Console.WriteLine(cursorDocument);

        // Sort the exam scores by student_id
        var sort = Builders<BsonDocument>.Sort.Descending("student_id");
        var highestScore = Collection.Find(highExamScoreFilter).Sort(sort).First();
        Console.WriteLine("\n**********\n");
        Console.WriteLine("\nHigh Score\n");
        Console.WriteLine("\n**********\n");
        Console.WriteLine(highestScore);

        return Task.CompletedTask;
    }

    public static async Task CreateExample()
    {
        // var dbClient = new MongoClient(MongoDbConnectionString);
        // var database = dbClient.GetDatabase("sample_training");
        // var collection = database.GetCollection<BsonDocument>("grades");

        // Define a new student for the grade book.

        var document = new BsonDocument
        {
            {"student_id", 10000},
            {
                "scores", new BsonArray
                {
                    new BsonDocument {{"type", "exam"}, {"score", 88.12334193287023}},
                    new BsonDocument {{"type", "quiz"}, {"score", 74.92381029342834}},
                    new BsonDocument {{"type", "homework"}, {"score", 89.97929384290324}},
                    new BsonDocument {{"type", "homework"}, {"score", 82.12931030513218}}
                }
            },
            {"class_id", 480}
        };

        // *********************************
        // Create Operations
        // *********************************

        // Insert the new student grade records into the database.

        Console.WriteLine("Inserting Document");
        await Collection.InsertOneAsync(document);
        Console.WriteLine("Document Inserted.\n");
    }

    public static async Task<List<BsonDocument>> GetDatabases()
    {
        //var dbClient = new MongoClient(MongoDbConnectionString);

        List<BsonDocument> dbList = (await DbClient.ListDatabasesAsync()).ToList();

        Console.WriteLine("The list of databases on this server is: ");

        foreach (var db in dbList) Console.WriteLine(db);

        return dbList;
    }
}

//https://gist.github.com/kenwalger/4a3da771b8471c43d190327556ebc3ab
//https://zetcode.com/csharp/mongodb/
//https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio
//https://mongodb.github.io/mongo-csharp-driver/1.11/getting_started/