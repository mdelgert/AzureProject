namespace AzureProject.Tests.Services;

public class CosmosServiceTests
{
    [Fact]
    public async Task DemoTest()
    {
        var cosmosService = new CosmosService();
        await cosmosService.Demo();
    }
}

//https://github.com/Azure-Samples/cosmos-dotnet-getting-started