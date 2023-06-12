# Introduction
Now that you've read through the [repo's readme](../README.md) and gotten a feel for the project, let's talk in depth about what is going on here.

Before we begin, we need to realize that we're leveraging two different technologies here to accomplish our integration tests: WebApplicationFactory and Testcontainers.

## WebApplicationFactory
WebApplicationFactory is a system developed by Microsoft to run an ASP.NET Core WebApi in memory.
We use this so that we don't have to worry about deploying our web API to somewhere and subsequently connecting to it.
You can read all about this awesome system in the [official Microsoft docs](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-7.0).

## Testcontainers
For our tests, we're leveraging Testcontainers to automatically start a Postgres Docker container when our tests start running, and tear it down afterwards.
I have used a [class fixture](https://xunit.net/docs/shared-context#class-fixture) to share a single container across all tests in a single class.
It doesn't necessarily need to be shared across all tests, and some people may see that as a bad design, but ultimately it's up to you.
Without the class fixture, Testcontainers would spin up a separate instance for each test in the class. Either solution is perfectly acceptable provided that your tests are written appropriately!

# The Process
Now that we've been introduced to the different technologies we're using here, let's walk through how the code executes step-by-step.

1. We begin executing our test(s).
2. Our `PostgresFixture` is instantiated and the `InitializeAsync()` method is called.
   1. Our Postgres DB container for our tests is started. 
3. An HTTP client is created for our custom WebApplicationFactory.
   1. The `ConfigureWebHost()` in our `TestcontainersDemoApiFactory` is called.
   2. We add our `appsettings.Tests.json` file to the configuration providers to override our Postgres connection string.
4. The `Program.cs` file from our WebApi now executes and starts up our API in memory.
   1. This uses our overrides in our `TestcontainersDemoApiFactory` for our configuration.
   2. An important note here is that it also executes our EF Core Migrations against the DB container that was started by Testcontainers.
5. The API call is made from the test method.
6. The test performs the assertions.
7. Any subsequent tests will now run against the same Postgres DB container started by Testcontainers.
8. After all tests in the class have finished executing, the `DisposeAsync()` method in our `PostgresFixture` is called, destroying the container created by Testcontainers.
