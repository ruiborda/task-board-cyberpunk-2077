using Microsoft.EntityFrameworkCore;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Context;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository.impl;

public class UserRepository(TaskBoardDbContext dbContext) : IUserRepository
{
    public Task<UserData?> GetUserByIdAsync(Guid id)
    {
        return dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<UserData?> GetUserByEmailAsync(string email)
    {
        return dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<UserData?> CreateUserAsync(UserData user)
    {
        dbContext.Users.Add(user);
        return dbContext.SaveChangesAsync().ContinueWith(t => t.IsCompletedSuccessfully ? user : null);
    }

    public async Task<UserData?> UpdateUserAsync(UserData user)
    {
        var local = dbContext.Set<UserData>()
            .Local
            .FirstOrDefault(e => e.Id == user.Id);
        if (local != null) dbContext.Entry(local).State = EntityState.Detached;
        dbContext.Entry(user).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return false;
        dbContext.Users.Remove(user);
        var result = await dbContext.SaveChangesAsync();
        return result > 0;
    }

    public Task<bool> UserExistsAsync(Guid id)
    {
        return dbContext.Users.AnyAsync(u => u.Id == id);
    }

    public Task<bool> UserExistsByEmailAsync(string email)
    {
        return dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public Task<List<UserData>> GetAllUsersAsync()
    {
        return dbContext.Users.ToListAsync();
    }
}