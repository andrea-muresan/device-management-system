using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserRepository(DeviceDbContext context) : IUserRepository

{
    public async Task<bool> CreateUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        
        var result = await context.SaveChangesAsync();
        
        return result > 0;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

}
