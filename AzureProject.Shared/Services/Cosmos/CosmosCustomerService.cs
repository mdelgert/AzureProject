using Bogus;
using Bogus.DataSets;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace AzureProject.Shared.Services.Cosmos;

public static class CosmosCustomerService
{
    public static async Task Demo()
    {
        await DeleteCustomers();
        //await SaveCustomer();
        await SaveCustomers();
        await GetCustomers();
    }

    private static async Task DeleteCustomers()
    {
        await using var context = new CosmosContext();
        
        var customers = context.Customers.ToList();

        foreach (var customer in customers)
        {
            context.Customers.Remove(customer);
        }
        
        await context.SaveChangesAsync();
        
        Console.WriteLine("Delete success!");
    }
    
    private static async Task GetCustomers()
    {
        await using var context = new CosmosContext();
        
        var customers = context.Customers.ToList();

        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        }
        
        //https://learn.microsoft.com/en-us/ef/core/querying/
    }
    
    private static async Task SaveCustomers()
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
    
    private static async Task SaveCustomer()
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

public class CosmosContext:DbContext  
{
    public DbSet<CustomerModel> Customers { get; set; }  
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)  
    {     
        var accountEndpoint = "https://localhost:8081";  
        var accountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";  
        var dbName = "efDemo";  
        optionsBuilder.UseCosmos(accountEndpoint, accountKey, dbName);  
    }
    
    protected override void OnModelCreating( ModelBuilder builder ) {
        builder.HasDefaultContainer("Test");
        builder.Entity<CustomerModel>().ToContainer("Customers");
    }
}  