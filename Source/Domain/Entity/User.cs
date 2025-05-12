using static BCrypt.Net.BCrypt;

namespace TaskBoardDemo.Source.Domain.Entity;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string? PasswordHash { get; set; }
    
    public string ProfileImage { get; set; } = string.Empty;

    public void UpdateUser(User newUser)
    {
        Name = newUser.Name;
        Email = newUser.Email;
        ProfileImage = newUser.ProfileImage;
        if (!string.IsNullOrEmpty(newUser.PasswordHash)) PasswordHash = HashPassword(newUser.PasswordHash);
    }
    public static User CreateUser(User user)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Email = user.Email,
            ProfileImage = user.ProfileImage,
            PasswordHash = HashPassword(user.PasswordHash)
        };
    }
}