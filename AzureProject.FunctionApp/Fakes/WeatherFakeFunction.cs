namespace AzureProject.FunctionApp.Fakes;

public class WeatherFakeFunction
{
    private const string NamePrefix = "WeatherFakeFunction";
    
    [FunctionName(NamePrefix)]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Fakes" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;
        
        try
        {
            log.LogInformation("{NamePrefix} function processed a request", NamePrefix);
            responseMessage = JsonConvert.SerializeObject(WeatherFake.Get(10), Formatting.Indented);
        }
        catch (Exception exception)
        {
            var errorResponse = new {name = $"{NamePrefix}", error = $"{exception}"};
            responseMessage = JsonConvert.SerializeObject(errorResponse);
            log.LogCritical("{NamePrefix} Error: {Exception}", NamePrefix, exception.ToString());
        }
        
        return Task.FromResult<IActionResult>(new OkObjectResult(responseMessage));
    }
}