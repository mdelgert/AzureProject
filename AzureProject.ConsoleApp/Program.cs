IServiceCollection services = new ServiceCollection();
Startup.ConfigureServices(services);
IServiceProvider serviceProvider = services.BuildServiceProvider();

var customers = CustomerFake.Get(100);

foreach (var customer in customers) Console.WriteLine($"{customer.FirstName} {customer.LastName}");

Console.ReadKey();

//https://stackoverflow.com/questions/60688112/logging-in-console-application-net-core-with-di
//https://thecodeblogger.com/2021/05/11/how-to-enable-logging-in-net-console-applications/