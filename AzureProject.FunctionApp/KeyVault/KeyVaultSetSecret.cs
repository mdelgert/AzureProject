namespace AzureProject.FunctionApp.KeyVault;

public class KeyVaultSetSecret
{
    private const string NamePrefix = "KeyVaultSetSecret";

    [FunctionName(NamePrefix)]
    [OpenApiOperation(operationId: "Run", tags: new[] { "KeyVault" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "SecretName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The keyVault secret name.")]
    [OpenApiParameter(name: "SecretValue", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The keyVault secret value.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;
        string secretName = req.Query["SecretName"];
        string secretValue = req.Query["SecretValue"];
        
        try
        {
            log.LogInformation("{NamePrefix} function processed a request", NamePrefix);
            await KeyVaultHelper.SetSecret(secretName, secretValue);
            var keyValue = await KeyVaultHelper.GetSecret(secretName);
            var keyResponse = new { keyName = $"{secretName}", keyValue = $"{keyValue}" };
            responseMessage = JsonConvert.SerializeObject(keyResponse, Formatting.Indented);
        }
        catch (Exception exception)
        {
            var errorResponse = new { name = $"{NamePrefix}", error = $"{exception}" };
            responseMessage = JsonConvert.SerializeObject(errorResponse);
            log.LogCritical("{NamePrefix} Error: {Exception}", NamePrefix, exception.ToString());
        }

        return await Task.FromResult<IActionResult>(new OkObjectResult(responseMessage));
    }
}

