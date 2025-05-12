using TaskBoardDemo.Source.Application.Ports.Output;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Domain.Entity;

namespace TaskBoardDemo.Source.Application.Ports.Input;

public class UserInputPort(IUserOutputPort userOutputPort) : IUserUseCase
{
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await userOutputPort.GetUserByIdAsync(id).ConfigureAwait(false);
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        var existingUser = await userOutputPort.GetUserByEmailAsync(user.Email).ConfigureAwait(false);
        if (existingUser != null) throw new InvalidOperationException("User already exists.");

        var userForAdd = User.CreateUser(user);

        var createdUser = await userOutputPort.CreateUserAsync(userForAdd).ConfigureAwait(false);
        return createdUser ?? throw new InvalidOperationException("User could not be created.");
    }

    public async Task<User?> UpdateUserAsync(User user)
    {
        var existingUser = await userOutputPort.GetUserByIdAsync(user.Id);
        Console.WriteLine($"Updating user with ID {user.Id}");
        if (existingUser == null) throw new InvalidOperationException("User not found.");
        existingUser.UpdateUser(user);
        var updatedUser = await userOutputPort.UpdateUserAsync(existingUser).ConfigureAwait(false);
        return updatedUser;
    }

    public async Task<User?> DeleteUserAsync(Guid id)
    {
        var existingUser = await userOutputPort.GetUserByIdAsync(id).ConfigureAwait(false);
        if (existingUser == null) throw new InvalidOperationException("User not found.");

        var deleted = await userOutputPort.DeleteUserAsync(id).ConfigureAwait(false);
        if (!deleted) throw new InvalidOperationException($"Failed to delete user with ID {id}.");

        return existingUser;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await userOutputPort.GetAllUsersAsync();
    }
}