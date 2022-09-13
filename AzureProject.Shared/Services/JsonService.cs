namespace AzureProject.Shared.Services;

public static class JsonService
{
    private const string BaseUrl = "https://my-json-server.typicode.com/mdelgert/AzureProject";

    public static async Task<string?> GetComments()
    {
        CancellationToken cancellationToken = default;
        var options = new RestClientOptions($"{BaseUrl}/comments")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);
        var request = new RestRequest();
        var response = await client.GetAsync(request, cancellationToken);
        return response.Content;
    }

    public static async Task<string?> GetPosts()
    {
        CancellationToken cancellationToken = default;
        var options = new RestClientOptions($"{BaseUrl}/posts")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);
        var request = new RestRequest();
        var response = await client.GetAsync(request, cancellationToken);
        return response.Content;
    }
}

//https://restsharp.dev/v107/#restsharp-v107