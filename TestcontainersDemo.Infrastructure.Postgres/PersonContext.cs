using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestcontainersDemo.Infrastructure.Postgres.Models;

namespace TestcontainersDemo.Infrastructure.Postgres;

public sealed class PersonContext : DbContext {
    private readonly IConfiguration _configuration;

    public PersonContext(IConfiguration configuration) {
        _configuration = configuration;
    }
    public DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres"));
    }
}