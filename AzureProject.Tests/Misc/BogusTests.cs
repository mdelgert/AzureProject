//Nuget: https://www.nuget.org/packages/Bogus

using System.Globalization;
using Bogus;

namespace AzureProject.Tests.Misc;

public class BogusTests
{
    private readonly Faker _faker = new Faker();
    private readonly ITestOutputHelper _testOutputHelper;

    public BogusTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void RandomArrayStringTest()
    {
        string[] cars = {"Volvo", "BMW", "Ford", "Mazda"};
        _testOutputHelper.WriteLine(_faker.Random.ArrayElement(cars));
    }
    
    [Fact]
    public void RandomAlphaNumericTest()
    {
        _testOutputHelper.WriteLine(_faker.Random.AlphaNumeric(10));
    }
    
    [Fact]
    public void LoremTest()
    {
        _testOutputHelper.WriteLine(_faker.Lorem.Paragraph());
    }

    [Fact]
    public void CatchPhraseTest()
    {
        _testOutputHelper.WriteLine(_faker.Company.CatchPhrase());
    }

    [Fact]
    public void DateTest()
    {
        _testOutputHelper.WriteLine(_faker.Date.Past().ToString(CultureInfo.InvariantCulture));
    }

    [Fact]
    public void PasswordTest()
    {
        _testOutputHelper.WriteLine(_faker.Internet.Password(length:50));
    }
    
    [Fact]
    public void AddressTest()
    {
        var t = _faker.Address;
        var address = $"{t.BuildingNumber()} {t.StreetName()} {t.City()}, {t.State()} {t.ZipCode()}";
        _testOutputHelper.WriteLine(address);
        Assert.NotNull(address);
    }

    [Fact]
    public void PhoneNumberTest()
    {
        var p = _faker.Phone;
        var phoneNumber = $"{p.PhoneNumber()}";
        _testOutputHelper.WriteLine(phoneNumber);
        Assert.NotNull(phoneNumber);
    }
    
}

//https://www.nuget.org/packages/Bogus
//https://github.com/bchavez/Bogus