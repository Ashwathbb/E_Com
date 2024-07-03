using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shop.Service.IService;
using Shop.DataAccess.DTOs;

namespace Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersInfoService _userService;
        public AuthController(IUsersInfoService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _userService.Authenticate(loginDto.UserName, loginDto.Password);

            if (user == null)
            {
                return BadRequest("Wrong password, please enter correct password");
            }
            else if (!user.IsActive)
            {
                return BadRequest("Your account is inactive due to too many failed login attempts");
            }
            else
            {
                return Ok("Successfully logged in");
            }
        }
    }
}
