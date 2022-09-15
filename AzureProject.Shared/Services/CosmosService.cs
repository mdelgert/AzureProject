using System.Net;
using Microsoft.Azure.Cosmos;

namespace AzureProject.Shared.Services;

public class CosmosService
{
    // The Azure Cosmos DB endpoint for running this sample.
    private static string _endpointUri = null!;

    // The primary key for the Azure Cosmos account.
    private static string _primaryKey = null!;
    private readonly string containerId = "FamilyContainer";

    // The name of the database and container we will create
    private readonly string databaseId = "FamilyDatabase";

    // The container we will create.
    private Container container;

    // The Cosmos client instance
    private CosmosClient cosmosClient;

    // The database we will create
    private Database database;

    public async Task Demo()
    {
        // _endpointUri = await KeyVaultHelper.GetSecret("CosmosEndpointUri");
        // _primaryKey = await KeyVaultHelper.GetSecret("CosmosPrimaryKey");
        
        _endpointUri =
            Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosEndpointUri.ToString()) ?? string.Empty;
        _primaryKey =
            Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosPrimaryKey.ToString()) ?? string.Empty;
        
        await GetStartedDemoAsync();
    }

    // <GetStartedDemoAsync>
    /// <summary>
    ///     Entry point to call methods that operate on Azure Cosmos DB resources in this sample
    /// </summary>
    public async Task GetStartedDemoAsync()
    {
        // Create a new instance of the Cosmos Client
        cosmosClient = new CosmosClient(_endpointUri, _primaryKey);
        await CreateDatabaseAsync();
        await CreateContainerAsync();
        await AddItemsToContainerAsync();
        await QueryItemsAsync();
        await ReplaceFamilyItemAsync();
        //await DeleteFamilyItemAsync();
        //await DeleteDatabaseAndCleanupAsync();
    }
    // </GetStartedDemoAsync>

    // <CreateDatabaseAsync>
    /// <summary>
    ///     Create the database if it does not exist
    /// </summary>
    private async Task CreateDatabaseAsync()
    {
        // Create a new database
        database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", database.Id);
    }
    // </CreateDatabaseAsync>

    // <CreateContainerAsync>
    /// <summary>
    ///     Create the container if it does not exist.
    ///     Specifiy "/LastName" as the partition key since we're storing family information, to ensure good distribution of
    ///     requests and storage.
    /// </summary>
    /// <returns></returns>
    private async Task CreateContainerAsync()
    {
        // Create a new container
        container = await database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", container.Id);
    }
    // </CreateContainerAsync>

    // <AddItemsToContainerAsync>
    /// <summary>
    ///     Add Family items to the container
    /// </summary>
    private async Task AddItemsToContainerAsync()
    {
        // Create a family object for the Andersen family
        var andersenFamily = new Family
        {
            Id = "Andersen.1",
            LastName = "Andersen",
            Parents = new[]
            {
                new() {FirstName = "Thomas"},
                new Parent {FirstName = "Mary Kay"}
            },
            Children = new[]
            {
                new Child
                {
                    FirstName = "Henriette Thaulow",
                    Gender = "female",
                    Grade = 5,
                    Pets = new[]
                    {
                        new Pet {GivenName = "Fluffy"}
                    }
                }
            },
            Address = new Address {State = "WA", County = "King", City = "Seattle"},
            IsRegistered = false
        };

        try
        {
            // Read the item to see if it exists.  
            var andersenFamilyResponse =
                await container.ReadItemAsync<Family>(andersenFamily.Id, new PartitionKey(andersenFamily.LastName));
            Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
            var andersenFamilyResponse =
                await container.CreateItemAsync(andersenFamily, new PartitionKey(andersenFamily.LastName));

            // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n",
                andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
        }

        // Create a family object for the Wakefield family
        var wakefieldFamily = new Family
        {
            Id = "Wakefield.7",
            LastName = "Wakefield",
            Parents = new[]
            {
                new() {FamilyName = "Wakefield", FirstName = "Robin"},
                new Parent {FamilyName = "Miller", FirstName = "Ben"}
            },
            Children = new[]
            {
                new Child
                {
                    FamilyName = "Merriam",
                    FirstName = "Jesse",
                    Gender = "female",
                    Grade = 8,
                    Pets = new[]
                    {
                        new Pet {GivenName = "Goofy"},
                        new Pet {GivenName = "Shadow"}
                    }
                },
                new Child
                {
                    FamilyName = "Miller",
                    FirstName = "Lisa",
                    Gender = "female",
                    Grade = 1
                }
            },
            Address = new Address {State = "NY", County = "Manhattan", City = "NY"},
            IsRegistered = true
        };

        try
        {
            // Read the item to see if it exists
            var wakefieldFamilyResponse =
                await container.ReadItemAsync<Family>(wakefieldFamily.Id, new PartitionKey(wakefieldFamily.LastName));
            Console.WriteLine("Item in database with id: {0} already exists\n", wakefieldFamilyResponse.Resource.Id);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
            var wakefieldFamilyResponse =
                await container.CreateItemAsync(wakefieldFamily, new PartitionKey(wakefieldFamily.LastName));

            // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n",
                wakefieldFamilyResponse.Resource.Id, wakefieldFamilyResponse.RequestCharge);
        }
    }
    // </AddItemsToContainerAsync>

    // <QueryItemsAsync>
    /// <summary>
    ///     Run a query (using Azure Cosmos DB SQL syntax) against the container
    /// </summary>
    private async Task QueryItemsAsync()
    {
        var sqlQueryText = "SELECT * FROM c WHERE c.LastName = 'Andersen'";

        Console.WriteLine("Running query: {0}\n", sqlQueryText);

        var queryDefinition = new QueryDefinition(sqlQueryText);
        using var queryResultSetIterator = container.GetItemQueryIterator<Family>(queryDefinition);

        var families = new List<Family>();

        while (queryResultSetIterator.HasMoreResults)
        {
            var currentResultSet = await queryResultSetIterator.ReadNextAsync();
            foreach (var family in currentResultSet)
            {
                families.Add(family);
                Console.WriteLine("\tRead {0}\n", family);
            }
        }
    }
    // </QueryItemsAsync>

    // <ReplaceFamilyItemAsync>
    /// <summary>
    ///     Replace an item in the container
    /// </summary>
    private async Task ReplaceFamilyItemAsync()
    {
        var wakefieldFamilyResponse =
            await container.ReadItemAsync<Family>("Wakefield.7", new PartitionKey("Wakefield"));
        var itemBody = wakefieldFamilyResponse.Resource;

        // update registration status from false to true
        itemBody.IsRegistered = true;
        // update grade of child
        itemBody.Children[0].Grade = 6;

        // replace the item with the updated content
        wakefieldFamilyResponse =
            await container.ReplaceItemAsync(itemBody, itemBody.Id, new PartitionKey(itemBody.LastName));
        Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", itemBody.LastName, itemBody.Id,
            wakefieldFamilyResponse.Resource);
    }
    // </ReplaceFamilyItemAsync>

    // <DeleteFamilyItemAsync>
    /// <summary>
    ///     Delete an item in the container
    /// </summary>
    private async Task DeleteFamilyItemAsync()
    {
        var partitionKeyValue = "Wakefield";
        var familyId = "Wakefield.7";

        // Delete an item. Note we must provide the partition key value and id of the item to delete
        var wakefieldFamilyResponse =
            await container.DeleteItemAsync<Family>(familyId, new PartitionKey(partitionKeyValue));
        Console.WriteLine("Deleted Family [{0},{1}]\n", partitionKeyValue, familyId);
    }
    // </DeleteFamilyItemAsync>

    // <DeleteDatabaseAndCleanupAsync>
    /// <summary>
    ///     Delete the database and dispose of the Cosmos Client instance
    /// </summary>
    private async Task DeleteDatabaseAndCleanupAsync()
    {
        var databaseResourceResponse = await database.DeleteAsync();
        // Also valid: await this.cosmosClient.Databases["FamilyDatabase"].DeleteAsync();

        Console.WriteLine("Deleted Database: {0}\n", databaseId);

        //Dispose of CosmosClient
        cosmosClient.Dispose();
    }
    // </DeleteDatabaseAndCleanupAsync>
}

public class Family
{
    [JsonProperty(PropertyName = "id")] public string Id { get; set; }

    public string LastName { get; set; }
    public Parent[] Parents { get; set; }
    public Child[] Children { get; set; }
    public Address Address { get; set; }
    public bool IsRegistered { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class Parent
{
    public string FamilyName { get; set; }
    public string FirstName { get; set; }
}

public class Child
{
    public string FamilyName { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
    public int Grade { get; set; }
    public Pet[] Pets { get; set; }
}

public class Pet
{
    public string GivenName { get; set; }
}

public class Address
{
    public string State { get; set; }
    public string County { get; set; }
    public string City { get; set; }
}

//https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-get-started
//https://github.com/Azure-Samples/cosmos-dotnet-getting-started