namespace AzureProject.FunctionApp.Helpers;

public static class GetConfigHelper
{
    private const string NamePrefix = "GetConfigHelper";
    
    [FunctionName(NamePrefix)]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Helpers" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;

        try
        {
            responseMessage = EnvironmentHelper.GetConfig();
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

