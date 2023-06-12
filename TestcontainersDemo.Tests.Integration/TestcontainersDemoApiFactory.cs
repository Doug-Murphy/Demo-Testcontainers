using Microsoft.AspNetCore.Mvc.Testing;

namespace TestcontainersDemo.Tests.Integration;

public sealed class TestcontainersDemoApiFactory : WebApplicationFactory<Program> {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureAppConfiguration(config => {
            config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Tests.json"), false, false); 
        });
    }
}