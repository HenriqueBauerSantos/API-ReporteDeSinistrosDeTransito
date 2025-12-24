using Business_InfoTransito.Models.Events;
using Business_InfoTransito.Models.Location;
using Business_InfoTransito.Models.People;
using Microsoft.EntityFrameworkCore;

namespace Data_InfoTransito.Context;

public class InfoTransitoDbContext : DbContext
{
    public InfoTransitoDbContext(DbContextOptions options) : base(options) { }

    public DbSet<PersonAddress> PersonsAddresses { get; set; }
    public DbSet<SinistroAddress> SinistrosAddresses { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Sinistro> Sinistros { get; set; }
    public DbSet<SinistroExcludeSolicitation> SinistroExcludeSolicitation { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties()
            .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfoTransitoDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        base.OnModelCreating(modelBuilder);
    }
}
