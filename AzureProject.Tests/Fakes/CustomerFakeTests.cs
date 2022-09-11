namespace AzureProject.Tests.Fakes;

public class CustomerFakeTests
{
    [Fact]
    public void GetTest()
    {
        var customers = CustomerFake.Get(10);
    }
}