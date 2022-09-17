using Bogus;
using Bogus.DataSets;

namespace AzureProject.Shared.Services.Cosmos;

public static class CosmosCustomerService
{
    private static readonly CosmosContext Context = new();
    public static Task<string> GetAllCustomers()
    {
        var customers = Context.Customer.ToList();
        var response = JsonConvert.SerializeObject(customers, Formatting.Indented);
        return Task.FromResult(response);
    }
    
    public static async Task Demo()
    {
        //await DeleteCustomers();
        //await CreateCustomer();
        await CreateCustomers();
        //await GetCustomers();
    }

    private static async Task DeleteCustomers()
    {
        var customers = Context.Customer.ToList();

        foreach (var customer in customers) Context.Customer.Remove(customer);

        await Context.SaveChangesAsync();

        Console.WriteLine("Delete success!");
    }

    private static async Task GetCustomers()
    {
        var customers = Context.Customer.ToList();

        foreach (var customer in customers) Console.WriteLine($"{customer.FirstName} {customer.LastName}");

        //https://learn.microsoft.com/en-us/ef/core/querying/
    }

    private static async Task CreateCustomers()
    {
        for (var i = 1; i <= 1000; i++)
        {
            var faker = new Faker();
            var phone = new PhoneNumbers();

            Context.Add(
                new CustomerModel
                {
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Address = faker.Person.Address.Street,
                    City = faker.Person.Address.City,
                    State = faker.Person.Address.State,
                    ZipCode = faker.Person.Address.ZipCode,
                    PhoneNumber = phone.PhoneNumberFormat(1)
                });
        }

        await Context.SaveChangesAsync();

        Console.WriteLine("Save success!");
    }

    private static async Task CreateCustomer()
    {
        await Context.Database.EnsureDeletedAsync();
        await Context.Database.EnsureCreatedAsync();

        Context.Add(
            new CustomerModel
            {
                FirstName = "Bob",
                LastName = "Smith"
            });

        await Context.SaveChangesAsync();

        Console.WriteLine("Save success!");
    }
}