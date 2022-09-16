IServiceCollection services = new ServiceCollection();
Startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();
await MongoGradesService.Demo();
//Console.ReadKey();

