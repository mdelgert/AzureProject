namespace AzureProject.Tests.Misc;

public class SerilogTests
{
    private readonly ILogger<SerilogTests> _log;

    public SerilogTests(ILogger<SerilogTests> log)
    {
        _log = log;
    }

    [Fact]
    public void LogTest()
    {
        _log.LogInformation("Hello logger!");
    }
}

//https://stackify.com/net-core-loggerfactory-use-correctly/