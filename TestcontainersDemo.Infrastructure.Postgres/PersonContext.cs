using Microsoft.EntityFrameworkCore;
using TestcontainersDemo.Infrastructure.Postgres.Models;

namespace TestcontainersDemo.Infrastructure.Postgres;

public sealed class PersonContext : DbContext {
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(@"Host=0.0.0.0;Username=postgres;Password=password;Database=postgres");
    }
}