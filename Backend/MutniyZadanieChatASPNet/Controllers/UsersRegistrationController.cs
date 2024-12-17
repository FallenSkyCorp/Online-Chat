using Chat.Core.Abstractions;
using Chat.Core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MutniyZadanieChatASPNet.API.Contracts;

namespace MutniyZadanieChatASPNet.API.Controllers
{
    [ApiController]
    [Route("registration")]
    public class UsersRegistrationController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersRegistrationController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IResult> RegistrateUser([FromBody] UsersRequest request)
        {
            var (user, error) = Chat.Core.Models.User.Create(
                Guid.NewGuid(),
                request.Email,
                request.Password
                );

            if (!string.IsNullOrEmpty(error))
            {
                Response.StatusCode = 400;

                return Results.Json(error);
            }

            var users = await _usersService.GetAllUsers();

            if (users.FirstOrDefault(u => u.Email == user.Email) != default)
            {
                Response.StatusCode = 400;

                return Results.Json(new { message = "Пользователь с такой почтой уже существует." });
            }

            var userId = await _usersService.CreateUser(user);

            Response.StatusCode = 201;

            return Results.Json(user);
        }
    }
}
