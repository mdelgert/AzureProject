namespace AzureProject.Tests.Helpers;

public class FormatHelperTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public FormatHelperTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("c@t2022!", "c%40t2022!")]
    public void UrlEncodeStringTest(string input, string output)
    {
        var outputEncoded = FormatHelper.UrlEncodeString(input);
        _testOutputHelper.WriteLine(output);
        Assert.Equal(output, outputEncoded);
    }
    
    [Theory]
    [InlineData("c@t2022!", "c%40t2022!")]
    public void UrlDecodeStringTest(string input, string output)
    {
        var outputDecoded = FormatHelper.UrlDecodeString(input);
        _testOutputHelper.WriteLine(output);
        Assert.Equal(input, outputDecoded);
    }
    
    [Fact]
    public void UnixTimeTest()
    {
        var unixTime = FormatHelper.UnixTime();
        _testOutputHelper.WriteLine(unixTime.ToString());
    }

    [Fact]
    public void DateIso8601Test()
    {
        var dateIso = FormatHelper.DateIso8601();
        Assert.NotNull(dateIso);
        _testOutputHelper.WriteLine(dateIso);
    }

    [Theory]
    [InlineData("Hello World!", "SGVsbG8gV29ybGQh")]
    public void Base64EncodeTest(string plainText, string encodedText)
    {
        var base64Encode = FormatHelper.Base64Encode(plainText);
        _testOutputHelper.WriteLine(base64Encode);
        Assert.Equal(encodedText, base64Encode);
    }

    [Theory]
    [InlineData("Hello World!", "SGVsbG8gV29ybGQh")]
    public void Base64DecodeTest(string plainText, string encodedText)
    {
        var base64Decode = FormatHelper.Base64Decode(encodedText);
        _testOutputHelper.WriteLine(base64Decode);
        Assert.Equal(plainText, base64Decode);
    }
}