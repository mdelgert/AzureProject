namespace AzureProject.Tests.Workflow;

public class OpenWeatherWorkflowTests
{
    private readonly IOpenWeatherWorkflow _openWeatherWorkflow;

    public OpenWeatherWorkflowTests(IOpenWeatherWorkflow openWeatherWorkflow)
    {
        _openWeatherWorkflow = openWeatherWorkflow;
    }

    [Fact]
    public async Task ProcessTest()
    {
        await _openWeatherWorkflow.Process();
    }
}