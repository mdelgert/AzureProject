namespace AzureProject.Tests.Services;

public class CosmosServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CosmosServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task CreateCustomerTest()
    {
        var cosmosService = new CosmosService();
        var customer = new CustomerModel
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Smith"
        };
        
        await cosmosService.CreateCustomer(customer);
    }
    
    [Fact]
    public async Task FindCustomersTest()
    {
        var cosmosService = new CosmosService();
        var customers = await cosmosService.FindCustomers("Smith");
        foreach (var customer in customers)
        {
            _testOutputHelper.WriteLine(customer.FirstName);
        }
    }

    [Fact]
    public async Task DeleteCustomerTest()
    {
        var cosmosService = new CosmosService();
        await cosmosService.DeleteCustomer("5895766f-e76a-4c22-ae29-8f0e7cf0f6cc", "Smith");
    }
    
    // [Fact]
    // public async Task CreateCustomersTest()
    // {
    //     var cosmosService = new CosmosService();
    //     var customers = CustomerFake.Get(10);
    //     await cosmosService.CreateCustomers(customers);
    // }
}

//https://github.com/Azure-Samples/cosmos-dotnet-getting-started