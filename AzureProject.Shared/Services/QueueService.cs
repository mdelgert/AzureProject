namespace AzureProject.Shared.Services;

public class QueueService
{
    private readonly string _connectionString =
        Environment.GetEnvironmentVariable(KeyVaultEnum.StorageConnectionString.ToString()) ?? string.Empty;
    public async Task<bool> CreateQueue(string queueName)
    {
        try
        {
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            var queueClient = new QueueClient(_connectionString, queueName);

            // Create the queue
            await queueClient.CreateIfNotExistsAsync();

            if (await queueClient.ExistsAsync())
            {
                Console.WriteLine($"Queue created: '{queueClient.Name}'");
                return true;
            }

            Console.WriteLine("Make sure the Azurite storage emulator running and try again.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}\n\n");
            Console.WriteLine("Make sure the Azurite storage emulator running and try again.");
            return false;
        }
    }

    public async Task InsertMessage(string queueName, string message)
    {
        // Instantiate a QueueClient which will be used to create and manipulate the queue
        var queueClient = new QueueClient(_connectionString, queueName);

        // Create the queue if it doesn't already exist
        await queueClient.CreateIfNotExistsAsync();

        if (await queueClient.ExistsAsync())
            // Send a message to the queue
            await queueClient.SendMessageAsync(message);

        Console.WriteLine($"Inserted: {message}");
    }

    public async Task PeekMessage(string queueName)
    {
        // Instantiate a QueueClient which will be used to manipulate the queue
        var queueClient = new QueueClient(_connectionString, queueName);

        if (queueClient.Exists())
        {
            // Peek at the next message
            PeekedMessage[] peekedMessage = queueClient.PeekMessages();

            // Display the message
            Console.WriteLine($"Peeked message: '{peekedMessage[0].Body}'");
        }
    }

    public async Task UpdateMessage(string queueName)
    {
        // Instantiate a QueueClient which will be used to manipulate the queue
        var queueClient = new QueueClient(_connectionString, queueName);

        if (await queueClient.ExistsAsync())
        {
            // Get the message from the queue
            QueueMessage[] message = await queueClient.ReceiveMessagesAsync();

            // Update the message contents
            await queueClient.UpdateMessageAsync(message[0].MessageId,
                message[0].PopReceipt,
                "Updated contents",
                TimeSpan.FromSeconds(60.0) // Make it invisible for another 60 seconds
            );
        }
    }
}

//https://docs.microsoft.com/en-us/rest/api/storageservices/get-queue-metadata
//https://docs.microsoft.com/en-us/azure/storage/queues/storage-dotnet-how-to-use-queues?tabs=dotnet