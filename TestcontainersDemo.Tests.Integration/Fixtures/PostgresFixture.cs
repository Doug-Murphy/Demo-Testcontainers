using Testcontainers.PostgreSql;

namespace TestcontainersDemo.Tests.Integration.Fixtures; 

public sealed class PostgresFixture : IAsyncLifetime {
    private readonly PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
        .WithCleanUp(true)
        .Build();
    
    public async Task InitializeAsync() {
        await _postgresContainer.StartAsync();
    }

    public async Task DisposeAsync() {
        await _postgresContainer.StopAsync();
    }
}