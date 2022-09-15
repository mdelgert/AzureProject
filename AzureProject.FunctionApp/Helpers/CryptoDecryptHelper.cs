namespace AzureProject.FunctionApp.Helpers;

public static class CryptoDecryptHelper
{
    private const string NamePrefix = "CryptoDecryptHelper";
    private static readonly string CryptoSecret =
        Environment.GetEnvironmentVariable(KeyVaultEnum.CryptoSecret.ToString()) ?? string.Empty;
    private static readonly string CryptoSalt =
        Environment.GetEnvironmentVariable(KeyVaultEnum.CryptoSalt.ToString()) ?? string.Empty;
    
    [FunctionName(NamePrefix)]
    [OpenApiOperation(operationId: "Run", tags: new[] { "Helpers" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "CipherText", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Text to decrypt.")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public static Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
    {
        string responseMessage;
        string cipherText = req.Query["CipherText"];
        
        try
        {
            var cryptoResponse = new { PlainText = CryptoHelper.DecryptAesString(cipherText, CryptoSecret, CryptoSalt)};
            responseMessage = JsonConvert.SerializeObject(cryptoResponse, Formatting.Indented);
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

