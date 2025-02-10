using Microsoft.EntityFrameworkCore;
using TallerAPI.Core;
using TallerAPI.Core.Entities;

namespace TallerAPI.Infrastructure;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "InMemoryDatabase");
    }
}

