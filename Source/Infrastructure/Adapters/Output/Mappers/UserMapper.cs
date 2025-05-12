using TaskBoardDemo.Source.Domain.Entity;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Data;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

public static class UserMapper
{
    public static User ToEntity(UserData data)
    {
        return new User
        {
            Id = data.Id,
            Name = data.Name,
            Email = data.Email,
            ProfileImage = data.ProfileImage,
            PasswordHash = data.PasswordHash
        };
    }

    public static UserData ToData(User entity)
    {
        return new UserData
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            ProfileImage = entity.ProfileImage,
            PasswordHash = entity.PasswordHash
        };
    }

    public static GetUserByIdResponse ToGetUserByIdResponse(User entity)
    {
        return new GetUserByIdResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfileImage = entity.ProfileImage,
            Email = entity.Email
        };
    }

    public static CreateUserResponse ToCreateUserResponse(User entity)
    {
        return new CreateUserResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfileImage = entity.ProfileImage,
            Email = entity.Email
        };
    }

    public static UpdateUserResponse ToUpdateUserResponse(User entity)
    {
        return new UpdateUserResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            ProfileImage = entity.ProfileImage,
            Email = entity.Email
        };
    }

    public static User ToEntity(CreateUserRequest request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email,
            ProfileImage = request.ProfileImage,
            PasswordHash = request.Password
        };
    }

    public static User ToEntity(UpdateUserRequest request)
    {
        return new User
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            ProfileImage = request.ProfileImage,
            PasswordHash = request.Password
        };
    }

    public static List<GetUserByIdResponse> ToGetUserByIdResponseList(List<User> users)
    {
        return users.Select(ToGetUserByIdResponse).ToList();
    }

    public static GetAllUsersResponse ToGetAllUsersResponse(User entity)
    {
        return new GetAllUsersResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            ProfileImage = entity.ProfileImage
        };
    }

    public static List<GetAllUsersResponse> ToGetAllUsersResponseList(List<User> users)
    {
        return users.Select(ToGetAllUsersResponse).ToList();
    }
}