using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TallerAPI.Core.Repositories;
using TallerAPI.Infrastructure;
using TallerAPI.Infrastructure.Implementations.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase(databaseName: "InMemoryDatabase"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<DataSeedingService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Taller Challenge API", Version = "v1" });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taller API v1");
    });

    using var scope = app.Services.CreateScope();
    await scope.ServiceProvider.GetService<DataSeedingService>()!.SeedAsync();
}

app.UseHttpsRedirection();
app.MapControllers();      
app.Run();