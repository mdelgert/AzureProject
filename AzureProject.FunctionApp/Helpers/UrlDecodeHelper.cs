namespace AzureProject.FunctionApp.Helpers;

public static class UrlDecodeHelper
{
    private const string NamePrefix = "UrlDecodeHelper";

    [FunctionName(NamePrefix)]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Helpers" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "Input", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Text to decode.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;
        string input = req.Query["Input"];
        
        try
        {
            var output = new { Output = FormatHelper.UrlDecodeString(input)};
            responseMessage = JsonConvert.SerializeObject(output, Formatting.Indented);
        }
        catch (Exception exception)
        {
            var errorResponse = new { name = $"{NamePrefix}", error = $"{exception}" };
            responseMessage = JsonConvert.SerializeObject(errorResponse);
            log.LogCritical("{NamePrefix} Error: {Exception}", NamePrefix, exception.ToString());
        }

        return Task.FromResult<IActionResult>(new OkObjectResult(responseMessage));
    }
}