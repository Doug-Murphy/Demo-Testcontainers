using TestcontainersDemo.Infrastructure.Postgres.Models;

namespace TestcontainersDemo.WebApi.Models.Response;

/// <summary>
/// The API response when a person is created.
/// </summary>
public sealed record CreatePersonResponse {
    public CreatePersonResponse(Person dbPerson) {
        Id = dbPerson.Id;
        FirstName = dbPerson.FirstName;
        LastName = dbPerson.LastName;
    }

    public CreatePersonResponse() {
        
    }

    public Guid Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }
}