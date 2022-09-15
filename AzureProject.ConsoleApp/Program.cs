IServiceCollection services = new ServiceCollection();
Startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();
var cosmosService = new CosmosService();
await cosmosService.Demo();

