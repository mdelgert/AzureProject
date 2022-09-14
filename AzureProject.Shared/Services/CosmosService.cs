using System;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Cosmos;
using Database = Microsoft.Azure.Cosmos.Database;

namespace AzureProject.Shared.Services;

public static class CosmosService
{
    public static async Task Demo()
    {
        // The Azure Cosmos DB endpoint for running this sample.
        var cosmosEndpointUri = await KeyVaultHelper.GetSecret("CosmosEndpointUri");
        
        // The primary key for the Azure Cosmos account.
        var cosmosPrimaryKey = await KeyVaultHelper.GetSecret("CosmosPrimaryKey");
        
        // The name of the database and container we will create
        var databaseId = "FamilyDatabase";
        
        var containerId = "FamilyContainer";

        var cosmosClient = new CosmosClient(cosmosEndpointUri, cosmosPrimaryKey);

        var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        
    }
}

//https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-get-started