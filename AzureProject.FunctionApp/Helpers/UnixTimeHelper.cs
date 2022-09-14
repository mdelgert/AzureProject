namespace AzureProject.FunctionApp.Helpers;

public static class UnixTimeHelper
{
    private const string NamePrefix = "UnixTimeHelper";
    
    [FunctionName("UnixTimeHelper")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Helpers" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;

        try
        {
            var unixTime = new { unixTime = $"{FormatHelper.UnixTime()}"};
            responseMessage = JsonConvert.SerializeObject(unixTime, Formatting.Indented);
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

