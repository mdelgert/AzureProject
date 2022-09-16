using MongoDB.Bson;
using MongoDB.Driver;

namespace AzureProject.FunctionApp.Mongo;

public static class MongoCustomers
{
    private const string NamePrefix = "MongoCustomers";

    private static readonly string MongoDbConnectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.MongoDBConnectionString.ToString()) ?? string.Empty;
    private static readonly MongoClient DbClient = new(MongoDbConnectionString);
    private static readonly IMongoDatabase Database = DbClient.GetDatabase("sample_analytics");
    private static readonly IMongoCollection<BsonDocument> Collection = Database.GetCollection<BsonDocument>("customers");
    
    [FunctionName(NamePrefix)]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Mongo" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;
        
        try
        {
            var documents = Collection.FindAsync(new BsonDocument()).Result.ToList();
            var dotNetObjList = documents.ConvertAll(BsonTypeMapper.MapToDotNetValue);
            responseMessage = JsonConvert.SerializeObject(dotNetObjList, Formatting.Indented);
        }
        catch (Exception exception)
        {
            var errorResponse = new {name = $"{NamePrefix}", error = $"{exception}"};
            responseMessage = JsonConvert.SerializeObject(errorResponse);
            log.LogCritical("{NamePrefix} Error: {Exception}", NamePrefix, exception.ToString());
        }

        return await Task.FromResult<IActionResult>(new OkObjectResult(responseMessage));
    }
}
