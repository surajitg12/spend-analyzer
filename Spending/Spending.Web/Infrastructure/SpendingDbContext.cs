using Microsoft.EntityFrameworkCore;
using Spending.Web.Models;

namespace Spending.Web.Infrastructure;

public class SpendingDbContext: DbContext
{
    public SpendingDbContext(DbContextOptions<SpendingDbContext> options) 
        : base(options) 
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SpendingModel>()
            .ToContainer("spending")
            .HasPartitionKey(p => p.Period) // Specify the partition key
            .HasNoDiscriminator(); 
     }

    public DbSet<SpendingModel> spendings { get; set; }
}
