namespace AzureProject.Tests.Services;

public class CosmosServiceTests
{
    [Fact]
    public async Task Test1()
    {
        var cosmosService = new CosmosService();
        await cosmosService.Demo();
    }
}

//https://github.com/Azure-Samples/cosmos-dotnet-getting-started