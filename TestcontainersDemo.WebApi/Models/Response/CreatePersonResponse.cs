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

    public Guid Id { get; private init; }

    public string FirstName { get; private init; }

    public string LastName { get; private init; }
}