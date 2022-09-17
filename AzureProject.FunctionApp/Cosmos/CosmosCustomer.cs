namespace AzureProject.FunctionApp.Cosmos;

public static class CosmosCustomer
{
    private const string NamePrefix = "CosmosCustomer";

    [FunctionName(NamePrefix)]
    [OpenApiOperation("Run", "Cosmos")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string), Description = "The OK response")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest req, ILogger log)
    {
        string responseMessage;
        
        try
        {
            responseMessage = await CosmosCustomerService.GetAllCustomers();
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