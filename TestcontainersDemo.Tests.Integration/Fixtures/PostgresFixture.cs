using Testcontainers.PostgreSql;

namespace TestcontainersDemo.Tests.Integration.Fixtures; 

public sealed class PostgresFixture : IAsyncLifetime {
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithCleanUp(true)
        .WithImage("postgres") //default for this version of Testcontainers is to use postgres:15.1, but I think it's better to ensure we're using the latest.
        .WithPortBinding(12345, 5432)
        .WithEnvironment("POSTGRES_PASSWORD", "password")
        .Build();
    
    public async Task InitializeAsync() {
        await _postgresContainer.StartAsync();
    }

    public async Task DisposeAsync() {
        await _postgresContainer.StopAsync();
    }
}