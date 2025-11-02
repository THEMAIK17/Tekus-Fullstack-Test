using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;

namespace Tekus.Infraestructure.Data;

public class TekusDbContext : DbContext
{
    public TekusDbContext(DbContextOptions<TekusDbContext> options) : base(options)
    {
    }

    // Map entities to database tables
    public DbSet<Provider> Providers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ProviderCustomField> ProviderCustomFields { get; set; }
    public DbSet<ServiceCountry> ServiceCountries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        // Applies all configurations defined in this assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Manually map the 'Key' property to the '[Key]' column,
        // as 'Key' is a reserved keyword in SQL.
        modelBuilder.Entity<ProviderCustomField>()
            .Property(p => p.Key)
            .HasColumnName("Key");
    }
}