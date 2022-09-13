namespace AzureProject.Tests.Helpers;

public class StringExtHelperTests
{
    [Fact]
    public void TruncateTest()
    {
        const string fullMessage = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        var totalCount = fullMessage.Length;
        var truncatedMessage = fullMessage.Truncate(1, ".");
        var truncateCount = truncatedMessage!.Length;
        Assert.Equal(2, truncateCount);
        Assert.NotEqual(totalCount, truncateCount);
    }
}
