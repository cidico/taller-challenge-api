using Bogus;
using Microsoft.EntityFrameworkCore;
using TallerAPI.Core.Entities;

namespace TallerAPI.Infrastructure;

public class DataSeedingService(UserDbContext context)
{
    public async Task SeedAsync()
    {
        if (!await context.Users.AnyAsync())
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Name, f => f.Name.FullName());

            var users = userFaker.Generate(10);
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}