using TaskBoardDemo.Source.Domain.Entity;

namespace TaskBoardDemo.Source.Application.Ports.Output;

public interface IUserOutputPort
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<List<User>> GetAllUsersAsync();
}