using TallerAPI.Core.Entities;

namespace TallerAPI.Core.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    
    Task<List<User>> GetAllUsersAsync();
}