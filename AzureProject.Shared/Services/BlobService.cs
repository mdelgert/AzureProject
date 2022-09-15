namespace AzureProject.Shared.Services;

public static class BlobService
{
    private static readonly string StorageConnectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.StorageConnectionString.ToString()) ?? string.Empty;

    public static async Task SaveFile(string fileName, MemoryStream file, string container)
    {
        var containerClient = new BlobContainerClient(
            StorageConnectionString,
            container);

        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        // Get a reference to a blob
        var blobClient = containerClient.GetBlobClient(fileName);

        // Upload MemoryStream
        await blobClient.UploadAsync(file, true);
    }
}

//https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
//https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/storage/Azure.Storage.Blobs/samples