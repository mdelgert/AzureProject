namespace AzureProject.Tests.Services;

public class BlobServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BlobServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task SaveFileTest()
    {
        dynamic product = new JObject();
        product.ProductName = "Elbow Grease";
        product.Enabled = true;
        product.Price = 4.90m;
        product.StockCount = 9000;
        product.StockValue = 44100;
        product.Tags = new JArray("Real", "OnSale");
        _testOutputHelper.WriteLine(product.ToString());
        
        var fileName = FormatHelper.DateIso8601() + ".json";
        var file = StreamHelper.Convert(product.ToString());
        const string container = "test";
        
        await BlobService.SaveFile(fileName, file, container);
    }
}


//https://www.newtonsoft.com/json/help/html/CreateJsonDynamic.htm