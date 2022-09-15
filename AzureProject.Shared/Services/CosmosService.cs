using System.Net;
using Microsoft.Azure.Cosmos;

namespace AzureProject.Shared.Services;

public class CosmosService
{
    private const string ContainerId = "TestContainer";
    private const string DatabaseId = "TestDatabase";

    private static readonly string EndpointUri =
        Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosEndpointUri.ToString()) ?? string.Empty;

    private static readonly string PrimaryKey =
        Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosPrimaryKey.ToString()) ?? string.Empty;

    private Container _container;
    private CosmosClient _cosmosClient;
    private Database _database;

    public async Task Demo()
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        await CreateDatabaseAsync();
        await CreateContainerAsync();
        await AddItemsToContainerAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
    }
    
    private async Task CreateContainerAsync()
    {
        _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
    }

    private async Task AddItemsToContainerAsync()
    {
        var customer = new CustomerModel
        {
            Id = "Smith.1",
            FirstName = "Bob",
            LastName = "Smith"
        };
        
        try
        {
            // Read the item to see if it exists. 
            var customerResponse =
                await _container.ReadItemAsync<CustomerModel>(customer.Id, new PartitionKey(customer.LastName));
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            // Create an item in the container representing the customer. Note we provide the value of the partition key for this item, which is "Smith"
            var customerResponse =
                await _container.CreateItemAsync(customer, new PartitionKey(customer.LastName));
            
            // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse.
            // We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n",
                customerResponse.Resource.Id, customerResponse.RequestCharge);
        }
        
    }
}

//https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-get-started
//https://github.com/Azure-Samples/cosmos-dotnet-getting-started