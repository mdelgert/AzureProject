using System.Net;
using Microsoft.Azure.Cosmos;

namespace AzureProject.Shared.Services;

public class CosmosService
{
    // private static readonly string EndpointUri =
    //     Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosEndpointUri.ToString()) ?? string.Empty;
    //
    // private static readonly string PrimaryKey =
    //     Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosPrimaryKey.ToString()) ?? string.Empty;

    private static readonly string EndpointUri = "https://localhost:8081";

    private static readonly string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

    private const string DatabaseId = "db";
    
    private const string ContainerId = "customers";

    private Container _container;
    private CosmosClient _cosmosClient;
    private Database _database;
    
    public async Task CreateCustomers(List<CustomerModel> customers)
    {
        // _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        // _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        // _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        
        foreach (var customer in customers)
        {
            await CreateCustomer(customer);
        }
    }
    
    public async Task CreateCustomer(CustomerModel customer)
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        
        try
        {
            // Read the item to see if it exists. 
            var customerResponse =
                await _container.ReadItemAsync<CustomerModel>(customer.Id.ToString(), new PartitionKey(customer.LastName));
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

    public async Task<List<CustomerModel>> FindCustomers(string lastName)
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        
        var sqlQueryText = $"SELECT * FROM c WHERE c.LastName = '{lastName}'";
        
        var queryDefinition = new QueryDefinition(sqlQueryText);
        
        var queryResultSetIterator = _container.GetItemQueryIterator<CustomerModel>(queryDefinition);
        
        var customers = new List<CustomerModel>();
        
        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();
            customers.AddRange(currentResultSet);
        }

        return customers;
    }

    public async Task<List<CustomerModel>> ReadCustomers()
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        
        var sqlQueryText = $"SELECT TOP 10 * FROM c";
        
        var queryDefinition = new QueryDefinition(sqlQueryText);
        
        var queryResultSetIterator = _container.GetItemQueryIterator<CustomerModel>(queryDefinition);
        
        var customers = new List<CustomerModel>();
        
        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();
            customers.AddRange(currentResultSet);
        }

        return customers;
    }

    public async Task UpdateCustomer(CustomerModel customer)
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        var customerResponse = await _container.ReadItemAsync<CustomerModel>(customer.Id.ToString(), new PartitionKey(customer.LastName));
        var itemBody = customerResponse.Resource;
        customerResponse = await _container.ReplaceItemAsync<CustomerModel>(customer, itemBody.Id.ToString(), new PartitionKey(itemBody.LastName));
    }
    
    public async Task DeleteCustomer(string id, string partitionKeyValue)
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
        _container = await _database.CreateContainerIfNotExistsAsync(ContainerId, "/LastName");
        var customerResponse = await _container.DeleteItemAsync<CustomerModel>(id,new PartitionKey(partitionKeyValue));
        Console.WriteLine("Deleted customer [{0},{1}]\n", partitionKeyValue, id);
    }
}

//https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-get-started
//https://github.com/Azure-Samples/cosmos-dotnet-getting-started