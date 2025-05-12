using System.Diagnostics;
using TaskBoardDemo.Source.Application.Ports.Output;
using TaskBoardDemo.Source.Domain.Entity;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Repository;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output;

public class UserOutputAdapter(IUserRepository userRepository) : IUserOutputPort
{
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user != null) return UserMapper.ToEntity(user);
        Debug.WriteLine($"User with ID {id} not found.");
        return null;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user != null) return UserMapper.ToEntity(user);
        Debug.WriteLine($"User with email {email} not found.");
        return null;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        var userData = UserMapper.ToData(user);
        var userDataSaved = await userRepository.CreateUserAsync(userData);
        if (userDataSaved != null) return UserMapper.ToEntity(userDataSaved);
        Debug.WriteLine($"User with email {user.Email} could not be created.");
        return null;
    }

    public Task<User?> UpdateUserAsync(User user)
    {
        var userData = UserMapper.ToData(user);
        return userRepository.UpdateUserAsync(userData)
            .ContinueWith(t => t.Result != null ? UserMapper.ToEntity(t.Result) : null);
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var result = await userRepository.DeleteUserAsync(id);
        if (!result) Debug.WriteLine($"User with ID {id} could not be deleted.");
        return result;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await userRepository.GetAllUsersAsync();
        return users.Select(UserMapper.ToEntity).ToList();
    }
}