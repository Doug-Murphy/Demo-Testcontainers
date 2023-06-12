using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TestcontainersDemo.WebApi.Models.Request;
using TestcontainersDemo.WebApi.Models.Response;

namespace TestcontainersDemo.Tests.Integration;

public sealed class PersonControllerTests : IClassFixture<TestcontainersDemoApiFactory> {
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public PersonControllerTests(TestcontainersDemoApiFactory webApplicationFactory) {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task CreatePerson_ReturnsCreated_ForValidCreate() {
        var httpClient = _webApplicationFactory.CreateClient();
        var createPersonRequestFaker = new Faker<CreatePersonRequest>()
            .RuleFor(person => person.FirstName, faker => faker.Person.FirstName)
            .RuleFor(person => person.LastName, faker => faker.Person.LastName);

        var fakePerson = createPersonRequestFaker.Generate();
        var response = await httpClient.PostAsJsonAsync("/person", fakePerson);

        response.Headers.Should().ContainKey("Location");
        var parsedContent = await response.Content.ReadFromJsonAsync<CreatePersonResponse>();
        parsedContent.Should().NotBeNull();
        parsedContent!.FirstName.Should().Be(fakePerson.FirstName);
        parsedContent.LastName.Should().Be(fakePerson.LastName);
    }
}