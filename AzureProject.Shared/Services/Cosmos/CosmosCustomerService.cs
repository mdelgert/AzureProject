using Bogus;
using Bogus.DataSets;

namespace AzureProject.Shared.Services.Cosmos;

public static class CosmosCustomerService
{
    public static async Task Demo()
    {
        //await DeleteCustomers();
        //await CreateCustomer();
        await CreateCustomers();
        await GetCustomers();
    }

    private static async Task DeleteCustomers()
    {
        await using var context = new CosmosContext();

        var customers = context.Customer.ToList();

        foreach (var customer in customers) context.Customer.Remove(customer);

        await context.SaveChangesAsync();

        Console.WriteLine("Delete success!");
    }

    private static async Task GetCustomers()
    {
        await using var context = new CosmosContext();

        var customers = context.Customer.ToList();

        foreach (var customer in customers) Console.WriteLine($"{customer.FirstName} {customer.LastName}");

        //https://learn.microsoft.com/en-us/ef/core/querying/
    }

    private static async Task CreateCustomers()
    {
        await using var context = new CosmosContext();

        for (var i = 1; i <= 1000; i++)
        {
            var faker = new Faker();
            var phone = new PhoneNumbers();

            context.Add(
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

        await context.SaveChangesAsync();

        Console.WriteLine("Save success!");
    }

    private static async Task CreateCustomer()
    {
        await using var context = new CosmosContext();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        context.Add(
            new CustomerModel
            {
                FirstName = "Bob",
                LastName = "Smith"
            });

        await context.SaveChangesAsync();

        Console.WriteLine("Save success!");
    }
}