using AbySalto.Mid.Application.DTOs;
using AbySalto.Mid.Application.Interfaces;
using API.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.WebApi.Controllers
{
    public class AccountController(IUserService userService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto dto)
        {
            try
            {
                var user = await userService.RegisterAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto dto)
        {
            var token = await userService.LoginAsync(dto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized("Invalid username or password");

            return Ok(new { username = dto.Username, token = token });

        }
    }
}
