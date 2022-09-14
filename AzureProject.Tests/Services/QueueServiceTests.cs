namespace AzureProject.Tests.Services;

public class QueueServiceTests
{
    private const string QueueName = "test-queue";
    private const string QueueMessage = "Hello Queue!";
    private readonly QueueService _queueService = new();
    
    [Fact]
    public async Task CreateQueueTest()
    {
        await _queueService.CreateQueue(QueueName);
    }
    
    [Fact]
    public async Task InsertMessageTest()
    {
        await _queueService.InsertMessage(QueueName, QueueMessage);
    }
    
    [Fact]
    public async Task PeekMessageMessageTest()
    {
        await _queueService.PeekMessage(QueueName);
    }
    
    [Fact]
    public async Task UpdateMessageTest()
    {
        await _queueService.UpdateMessage(QueueName);
    }
}