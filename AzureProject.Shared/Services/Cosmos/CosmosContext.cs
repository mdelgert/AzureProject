using Microsoft.EntityFrameworkCore;

namespace AzureProject.Shared.Services.Cosmos;

public class CosmosContext : DbContext
{
    public DbSet<CustomerModel> Customer { get; set; }
    public DbSet<NoteModel> Note { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // var accountEndpoint = "https://localhost:8081";
        // var accountKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

        var accountEndpoint =
            Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosEndpointUri.ToString()) ?? string.Empty;
        
        var accountKey =
            Environment.GetEnvironmentVariable(KeyVaultEnum.CosmosPrimaryKey.ToString()) ?? string.Empty;

        var dbName = "efDemo";
        
        optionsBuilder.UseCosmos(accountEndpoint, accountKey, dbName);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultContainer("Demo");
        builder.Entity<CustomerModel>().ToContainer("Customers");
        builder.Entity<NoteModel>().ToContainer("Notes");
    }
}