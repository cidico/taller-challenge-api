#pragma warning disable CS8603 // Possible null reference return.
using Microsoft.EntityFrameworkCore;
using TallerAPI.Core.Entities;
using TallerAPI.Core.Repositories;

namespace TallerAPI.Infrastructure.Implementations.Repositories;

public class UserRepository(UserDbContext context) : IUserRepository
{
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await context.Users.ToListAsync();
    }
}