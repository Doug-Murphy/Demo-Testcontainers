namespace TestcontainersDemo.Infrastructure.Postgres.Models;

public record Person {
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }
}