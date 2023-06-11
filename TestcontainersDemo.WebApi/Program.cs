using Microsoft.EntityFrameworkCore;
using TestcontainersDemo.Infrastructure.Postgres;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PersonContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope()) {
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<PersonContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();