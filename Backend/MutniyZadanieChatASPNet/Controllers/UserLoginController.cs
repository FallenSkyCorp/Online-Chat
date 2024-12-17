using Chat.Core.Abstractions;
using Chat.Core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MutniyZadanieChatASPNet.API.Contracts;

namespace MutniyZadanieChatASPNet.Controllers
{
    [ApiController]
    [Route("login")]
    public class UserLoginController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UserLoginController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /*
        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetUsers()
        {
            var users = await _usersService.GetAllUsers();

            var response = users.Select(u => new UsersResponse(u.Id, u.Email, u.Password));

            return Ok(response);
        }
        */

        [HttpPost]
        public async Task<IResult> LoginUser([FromBody] UsersRequest request)
        {
            var users = await _usersService.GetAllUsers();

            var user = users.FirstOrDefault(u => u.Email == request.Email);

            if (user is null)
            {
                Response.StatusCode = 404;

                return Results.Json(new { message = "Пользователя с такой почтой не существует." });
            }

            if (user.Password != request.Password)
            {
                Response.StatusCode = 400;

                return Results.Json(new { message = "Неправильный пароль." });
            }

            Response.StatusCode = 200;

            return Results.Json(request);
        }

        /*
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id, [FromBody] UsersRequest request)
        {
            var userId = await _usersService.UpdateUser(
                id,
                request.Email,
                request.Password);

            return Ok(userId);
        }

        [HttpDelete ("{id:guid}")]
        public async Task<ActionResult<Guid>> DeketeUser(Guid id)
        {
            var userId = await _usersService.DeleteUser(id);

            return Ok(userId);
        }
        */
    }
}
