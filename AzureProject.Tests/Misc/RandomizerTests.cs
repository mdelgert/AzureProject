namespace AzureProject.Tests.Misc;

public class RandomizerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public RandomizerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private static Dictionary<string, object> GetProperties()
    {
        var properties = new Dictionary<string, object>
        {
            {"Min", 7},
            {"Max", 10},
            {"UseNumber", false},
            {"UseSpecial", false}
        };
        return properties;
    }

    [Fact]
    public async Task WriteSampleLeads()
    {
        const string folderPath = "TestData\\";

        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

        dynamic output = new List<dynamic>();
        var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
        var randomizerLastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());

        for (var i = 0; i < 10; i++)
        {
            dynamic row = new ExpandoObject();
            var firstname = randomizerFirstName.Generate();
            var lastname = randomizerLastName.Generate();
            if (firstname != null) row.fname = firstname;
            if (lastname != null) row.lname = lastname;
            output.Add(row);
        }

        string json = JsonConvert.SerializeObject(output, Formatting.Indented);
        await File.WriteAllTextAsync(folderPath + "\\SampleLeads.json", json);
    }

    [Fact]
    public void RegexWithSeed1Test()
    {
        var randomizerTextRegexWithSeed1 = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex
            {Seed = 12345, Pattern = @"^[1-9][0-9]{3}([A-RT-Z][A-Z]|[S][BCE-RT-Z])$"});
        var textRegexWithSeed1 = randomizerTextRegexWithSeed1.Generate();
        Write(randomizerTextRegexWithSeed1, textRegexWithSeed1);
        Assert.NotNull(textRegexWithSeed1);
    }

    [Fact]
    public void RegexWithSeed2Test()
    {
        var randomizerTextRegexWithSeed2 = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex
            {Seed = 12345, Pattern = @"^[1-9][0-9]{3}([A-RT-Z][A-Z]|[S][BCE-RT-Z])$"});
        var textRegexWithSeed2 = randomizerTextRegexWithSeed2.Generate();
        Write(randomizerTextRegexWithSeed2, textRegexWithSeed2);
        Assert.NotNull(textRegexWithSeed2);
    }

    [Fact]
    public void RandomizerBytesTest()
    {
        var randomizerBytes = RandomizerFactory.GetRandomizer(new FieldOptionsBytes {Min = 10, Max = 20});
        var base64 = randomizerBytes.GenerateAsBase64String();
        Write(randomizerBytes, base64);
        Assert.NotNull(base64);
    }

    [Fact]
    public void RandomizerTextRegexTest()
    {
        var randomizerTextRegex = RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex
            {Pattern = @"^[1-9][0-9]{3}([A-RT-Z][A-Z]|[S][BCE-RT-Z])$"});
        var textRegex = randomizerTextRegex.Generate();
        Write(randomizerTextRegex, textRegex);
        Assert.NotNull(textRegex);
    }

    [Fact]
    public void RandomizerIban1Test()
    {
        var randomizerIban1 = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN {Type = "BOTH"});
        var iban1 = randomizerIban1.Generate();
        Write(randomizerIban1, iban1);
        Assert.NotNull(iban1);
    }

    [Fact]
    public void RandomizerIban2Test()
    {
        var randomizerIban2 = RandomizerFactory.GetRandomizer(new FieldOptionsIBAN {CountryCode = "NL"});
        var iban2 = randomizerIban2.Generate();
        Write(randomizerIban2, iban2);
        Assert.NotNull(iban2);
    }

    [Fact]
    public void RandomizerIbanWithSeed1Test()
    {
        var randomizerIbanWithSeed1 =
            RandomizerFactory.GetRandomizer(new FieldOptionsIBAN {Seed = 123, CountryCode = "NL"});
        var ibanWithSeed1 = randomizerIbanWithSeed1.Generate();
        Write(randomizerIbanWithSeed1, ibanWithSeed1);
        Assert.NotNull(ibanWithSeed1);
    }

    [Fact]
    public void RandomizerIbanWithSeed2Test()
    {
        var randomizerIbanWithSeed2 =
            RandomizerFactory.GetRandomizer(new FieldOptionsIBAN {Seed = 123, CountryCode = "NL"});
        var ibanWithSeed2 = randomizerIbanWithSeed2.Generate();
        Write(randomizerIbanWithSeed2, ibanWithSeed2);
        Assert.NotNull(ibanWithSeed2);
    }

    [Fact]
    public void RandomizerCityTest()
    {
        var randomizerCity = RandomizerFactory.GetRandomizer(new FieldOptionsCity());
        var city = randomizerCity.Generate();
        Write(randomizerCity, city);
        var cityUpper = randomizerCity.Generate(true);
        Write(randomizerCity, cityUpper);
        Assert.NotNull(city);
    }

    [Fact]
    public void RandomizerCountryTest()
    {
        var randomizerCountry = RandomizerFactory.GetRandomizer(new FieldOptionsCountry());
        var country = randomizerCountry.Generate();
        Write(randomizerCountry, country);
        Assert.NotNull(country);
    }

    [Fact]
    public void RandomizerMacTest()
    {
        var randomizerMac = RandomizerFactory.GetRandomizer(new FieldOptionsMACAddress
            {Min = "00-11-22-33-44-55", Max = "aa-bb-cc-dd-ee-ff", Separator = "-"});
        var mac = randomizerMac.Generate();
        Write(randomizerMac, mac);
        Assert.NotNull(randomizerMac);
    }

    [Fact]
    public void RandomizerIPv4()
    {
        var randomizerIPv4 = RandomizerFactory.GetRandomizer(new FieldOptionsIPv4Address
            {Min = "127.0.2.233", Max = "128.190.255.244"});
        var ipv4 = randomizerIPv4.Generate();
        Write(randomizerIPv4, ipv4);
        Assert.NotNull(ipv4);
    }

    [Fact]
    public void RandomizerIPv6Test()
    {
        var randomizerIPv6 = RandomizerFactory.GetRandomizer(new FieldOptionsIPv6Address
            {Min = "0000:0001:0000:0000:0020:ff00:0042:8000", Max = "2001:0db8:0120:0000:0030:ff00:aa42:8329"});
        var ipv6 = randomizerIPv6.Generate();
        Write(randomizerIPv6, ipv6);
        var ipv6Lower = randomizerIPv6.Generate(false);
        Write(randomizerIPv6, ipv6Lower);
        Assert.NotNull(ipv6);
    }

    [Fact]
    public void RandomizerTimeSpanTest()
    {
        var randomizerTimeSpan = RandomizerFactory.GetRandomizer(new FieldOptionsTimeSpan
            {From = TimeSpan.FromDays(1), To = TimeSpan.FromDays(7)});
        var ts = randomizerTimeSpan.Generate();
        Write(randomizerTimeSpan, ts);
        Assert.NotNull(ts);
    }

    [Fact]
    public void RandomizerTimeSpanCTest()
    {
        var randomizerTimeSpanC = RandomizerFactory.GetRandomizer(new FieldOptionsTimeSpan
            {From = TimeSpan.FromDays(1), To = TimeSpan.FromDays(7), IncludeMilliseconds = false, Format = "c"});
        var tsC = randomizerTimeSpanC.GenerateAsString();
        Write(randomizerTimeSpanC, tsC);
        Assert.NotNull(tsC);
    }

    [Fact]
    public void RandomizerDateTimeTest()
    {
        var randomizerDateTime =
            RandomizerFactory.GetRandomizer(new FieldOptionsDateTime {From = DateTime.Today.AddYears(-1)});
        var date = randomizerDateTime.Generate();
        Write(randomizerDateTime, date);
        var dateAsString = randomizerDateTime.GenerateAsString();
        Write(randomizerDateTime, dateAsString);
        Assert.NotNull(dateAsString);
    }

    [Fact]
    public void RandomizerDateTimeNoTimeTest()
    {
        var randomizerDateTimeNoTime = RandomizerFactory.GetRandomizer(new FieldOptionsDateTime {IncludeTime = false});
        var dateNoTime = randomizerDateTimeNoTime.Generate();
        Write(randomizerDateTimeNoTime, dateNoTime);
        var randomizerDateTimeWithFormat =
            RandomizerFactory.GetRandomizer(new FieldOptionsDateTime {Format = "yyyy/MM/dd"});
        var dateWithFormat = randomizerDateTimeWithFormat.GenerateAsString();
        Write(randomizerDateTimeNoTime, dateWithFormat);
        Assert.NotNull(dateNoTime);
    }

    [Fact]
    public void RandomizerFirstNameTest()
    {
        var randomizerFirstName = RandomizerFactory.GetRandomizer(new FieldOptionsFirstName());
        var firstname = randomizerFirstName.Generate();
        Write(randomizerFirstName, firstname);
        Assert.NotNull(firstname);
    }

    [Fact]
    public void RandomizerLastNameTest()
    {
        var randomizerLastName = RandomizerFactory.GetRandomizer(new FieldOptionsLastName());
        var lastname = randomizerLastName.Generate();
        Write(randomizerLastName, lastname);
        Assert.NotNull(lastname);
    }

    [Fact]
    public void RandomizerBooleanTest()
    {
        var randomizerBoolean = RandomizerFactory.GetRandomizer(new FieldOptionsBoolean());
        var b = randomizerBoolean.Generate();
        Write(randomizerBoolean, b);
        Assert.NotNull(!b);
    }

    [Fact]
    public void RandomizerByteTest()
    {
        var randomizerByte = RandomizerFactory.GetRandomizer(new FieldOptionsByte());
        var bt = randomizerByte.Generate();
        Write(randomizerByte, bt);
        Assert.NotNull(bt);
    }

    [Fact]
    public void RandomizerShortTest()
    {
        var randomizerShort = RandomizerFactory.GetRandomizer(new FieldOptionsShort());
        var sh = randomizerShort.Generate();
        Write(randomizerShort, sh);
        Assert.NotNull(sh);
    }

    [Fact]
    public void RandomizerIntegerTest()
    {
        var randomizerInteger = RandomizerFactory.GetRandomizer(new FieldOptionsInteger());
        var integer = randomizerInteger.Generate();
        Write(randomizerInteger, integer);
        var randomizerLong = RandomizerFactory.GetRandomizer(new FieldOptionsLong());
        var @long = randomizerInteger.Generate();
        Write(randomizerLong, @long);
        Assert.NotNull(@long);
    }

    [Fact]
    public void RandomizerFloatTest()
    {
        var randomizerFloat = RandomizerFactory.GetRandomizer(new FieldOptionsFloat());
        var flt = randomizerFloat.Generate();
        Write(randomizerFloat, flt);
        Assert.NotNull(flt);
    }

    [Fact]
    public void RandomizerDoubleTest()
    {
        var randomizerDouble = RandomizerFactory.GetRandomizer(new FieldOptionsDouble());
        var dbl = randomizerDouble.Generate();
        Write(randomizerDouble, dbl);
        Assert.NotNull(dbl);
    }

    [Fact]
    public void RandomizerGuidTest()
    {
        var randomizerGuid = RandomizerFactory.GetRandomizer(new FieldOptionsGuid());
        var guid = randomizerGuid.Generate();
        Write(randomizerGuid, guid);
        var guidAsString = randomizerGuid.GenerateAsString();
        Write(randomizerGuid, guidAsString);
        Assert.NotNull(guidAsString);
    }

    [Fact]
    public void RandomizerStringListTest()
    {
        var randomizerStringList = RandomizerFactory.GetRandomizer(new FieldOptionsStringList
            {Values = new[] {"1", "b", "c"}.ToList()});
        var stringListValue = randomizerStringList.Generate();
        Write(randomizerStringList, stringListValue);
        Assert.NotNull(stringListValue);
    }

    [Fact]
    public void FieldOptionsTextTest()
    {
        var properties = GetProperties();
        var fieldOptionsText = FieldOptionsFactory.GetFieldOptions("Text", properties);
        var randomizerText = RandomizerFactory.GetRandomizerAsDynamic(fieldOptionsText);
        string text = randomizerText.Generate();
        Write(randomizerText, text);
        Assert.NotNull(text);
    }

    [Fact]
    public void RandomizerTextLipsumTest()
    {
        var randomizerTextLipsum = RandomizerFactory.GetRandomizer(new FieldOptionsTextLipsum {Paragraphs = 1});
        var lipsum = randomizerTextLipsum.Generate();
        Write(randomizerTextLipsum, lipsum);
        Assert.NotNull(randomizerTextLipsum);
    }

    [Fact]
    public void RandomizerTextPattern()
    {
        var randomizerTextPattern =
            RandomizerFactory.GetRandomizer(new FieldOptionsTextRegex {Pattern = "\\xLLnn_**ss\\x"});
        var textPattern = randomizerTextPattern.Generate();
        Write(randomizerTextPattern, textPattern);
        Assert.NotNull(textPattern);
    }

    [Fact]
    public void RandomizerTextWordsTest()
    {
        var randomizerTextWords = RandomizerFactory.GetRandomizer(new FieldOptionsTextWords {Min = 10, Max = 10});
        var words = randomizerTextWords.Generate();
        Write(randomizerTextWords, words);
        Assert.NotNull(words);
    }

    [Fact]
    public void RandomizerTimeSpan2Test()
    {
        var properties = GetProperties();
        var fieldOptionsTimeSpan2 = FieldOptionsFactory.GetFieldOptions("TimeSpan", properties);
        var randomizerTimeSpan2 = RandomizerFactory.GetRandomizerAsDynamic(fieldOptionsTimeSpan2);
        TimeSpan? ts2 = randomizerTimeSpan2.Generate();
        Write(randomizerTimeSpan2, ts2);
        Assert.NotNull(ts2);
    }

    [Fact]
    public void RandomizerNaughtyStringsTest()
    {
        var randomizerNaughtyStrings = RandomizerFactory.GetRandomizer(new FieldOptionsTextNaughtyStrings
            {Categories = "NumericStrings, TrickUnicode"});
        var naughtyString = randomizerNaughtyStrings.Generate();
        Write(randomizerNaughtyStrings, naughtyString);
        Assert.NotNull(naughtyString);
    }

    [Fact]
    public void RandomizerNaughtyStringsNullCategoryTest()
    {
        var randomizerNaughtyStringsNullCategory =
            RandomizerFactory.GetRandomizer(new FieldOptionsTextNaughtyStrings());
        var naughtyString2 = randomizerNaughtyStringsNullCategory.Generate();
        Write(randomizerNaughtyStringsNullCategory, naughtyString2);
        Assert.NotNull(naughtyString2);
    }

    [Fact]
    public void RandomizerEmailTest()
    {
        var randomizerEmail = RandomizerFactory.GetRandomizer(new FieldOptionsEmailAddress());
        var email = randomizerEmail.Generate();
        Write(randomizerEmail, email);
        Assert.NotNull(email);
    }

    [Fact]
    public void RandomizerCcnTest()
    {
        foreach (var cardIssuer in Enum.GetValues(typeof(CardIssuer)).Cast<CardIssuer>())
        {
            var randomizerCcn = RandomizerFactory.GetRandomizer(new FieldOptionsCCN {CardIssuer = cardIssuer});
            var ccn = randomizerCcn.Generate();
            Write(randomizerCcn, ccn, $" ({cardIssuer})");
            Assert.NotNull(ccn);
        }
    }

    private void Write<T>(object randomizer, T value, string? extra = null)
    {
        var genericType = randomizer.GetType().GetTypeInfo().GenericTypeArguments.FirstOrDefault();
        _testOutputHelper.WriteLine("{0}{1}{2} --> '{3}'",
            randomizer.GetType().Name,
            genericType != null ? $"[{genericType.Name}]" : string.Empty,
            extra ?? string.Empty,
            value
        );
    }
}

//https://www.nuget.org/packages/RandomDataGenerator.Net/
//https://github.com/StefH/RandomDataGenerator