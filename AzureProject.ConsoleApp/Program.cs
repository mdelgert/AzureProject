IServiceCollection services = new ServiceCollection();
Startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();
Console.ReadKey();
