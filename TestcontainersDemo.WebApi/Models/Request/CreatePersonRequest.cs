using System.ComponentModel.DataAnnotations;
using TestcontainersDemo.Infrastructure.Postgres.Models;

namespace TestcontainersDemo.WebApi.Models.Request;

/// <summary>
/// The request structure for creating a person record.
/// </summary>
public sealed record CreatePersonRequest {
    [Required]
    public required string FirstName { get; set; }

    [Required]
    public required string LastName { get; set; }

    public Person ToDatabasePerson() {
        return new Person {
            FirstName = FirstName,
            LastName = LastName
        };
    }
}