using Bogus;

namespace AzureProject.Tests.Misc;

public class BogusTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BogusTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void AddressTest()
    {
        var faker = new Faker();
        var t = faker.Address;
        var address = $"{t.BuildingNumber()} {t.StreetName()} {t.City()}, {t.State()} {t.ZipCode()}";
        _testOutputHelper.WriteLine(address);
        Assert.NotNull(address);
    }

    [Fact]
    public void PhoneNumberTest()
    {
        var faker = new Faker();
        var p = faker.Phone;
        var phoneNumber = $"{p.PhoneNumber()}";
        _testOutputHelper.WriteLine(phoneNumber);
        Assert.NotNull(phoneNumber);
    }
}

//https://www.nuget.org/packages/Bogus
//https://github.com/bchavez/Bogus