using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository;

public interface IUserRepository
{
    Task<UserData?> GetUserByIdAsync(Guid id);
    Task<UserData?> GetUserByEmailAsync(string email);
    Task<UserData?> CreateUserAsync(UserData user);
    Task<UserData?> UpdateUserAsync(UserData user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> UserExistsAsync(Guid id);
    Task<bool> UserExistsByEmailAsync(string email);
    Task<List<UserData>> GetAllUsersAsync();
}