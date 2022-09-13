namespace AzureProject.Tests.Services;

public class JsonServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public JsonServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task GetCommentsTest()
    {
        var response = await JsonService.GetComments();
        _testOutputHelper.WriteLine(response);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task GetPostsTest()
    {
        var response = await JsonService.GetPosts();
        _testOutputHelper.WriteLine(response);
        Assert.NotNull(response);
    }
}