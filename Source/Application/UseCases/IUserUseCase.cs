using TaskBoardDemo.Source.Domain.Entity;

namespace TaskBoardDemo.Source.Application.UseCases;

public interface IUserUseCase
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> CreateUserAsync(User user);
    Task<User?> UpdateUserAsync(User user);
    Task<User?> DeleteUserAsync(Guid id);
    Task<List<User>> GetAllUsersAsync();
}