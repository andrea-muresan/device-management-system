using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(int id);
    Task<bool> CreateUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);

}
