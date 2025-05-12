using Microsoft.AspNetCore.Mvc;
using TaskBoardDemo.Source.Application.UseCases;
using TaskBoardDemo.Source.Infrastructure.Adapters.Input.DTOs.User;
using TaskBoardDemo.Source.Infrastructure.Adapters.Output.Mappers;

namespace TaskBoardDemo.Source.Infrastructure.Adapters.Input;

[ApiController]
[Route("api/[controller]")]
public class UserInputAdapter(IUserUseCase userUseCase) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetUserByIdResponse>> GetUserById(Guid id)
    {
        var user = await userUseCase.GetUserByIdAsync(id);
        if (user == null) return NotFound(new { Message = $"User with ID {id} not found." });
        return Ok(UserMapper.ToGetUserByIdResponse(user));
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequest request)
    {
        var user = UserMapper.ToEntity(request);
        try
        {
            var createdUser = await userUseCase.CreateUserAsync(user);
            if (createdUser == null)
                return BadRequest(new { Message = "User could not be created." });
            return Ok(UserMapper.ToCreateUserResponse(createdUser));
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpPut]
    public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var user = UserMapper.ToEntity(request);
        try
        {
            var updatedUser = await userUseCase.UpdateUserAsync(user);
            Console.WriteLine($"3 Updated user with ID {updatedUser}");
            if (updatedUser == null)
                return NotFound(new { Message = $"User with ID {request.Id} not found." });
            return Ok(UserMapper.ToUpdateUserResponse(updatedUser));
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            var deletedUser = await userUseCase.DeleteUserAsync(id);
            if (deletedUser == null)
                return NotFound(new { Message = $"User with ID {id} not found." });
            return Ok(new { Message = $"User with ID {id} deleted." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllUsersResponse>>> GetAllUsers()
    {
        var users = await userUseCase.GetAllUsersAsync();
        return Ok(UserMapper.ToGetAllUsersResponseList(users));
    }
}