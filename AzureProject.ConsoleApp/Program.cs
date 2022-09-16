IServiceCollection services = new ServiceCollection();
Startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();
//await MongoPlanetsService.GetPlanets();
//await MongoPlanetsService.GetPlanetsJson();
//await MongoGeneralService.GetDatabases();
MongoNotesService.Demo();
