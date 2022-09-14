namespace AzureProject.Tests.Services;

public class CosmosServiceTests
{
    [Fact]
    public async Task Test1()
    {
        await CosmosService.Demo();
    }
}
