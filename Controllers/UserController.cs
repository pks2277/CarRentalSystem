using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CarRentalSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            await _userService.RegisterUser(user);
            return Ok("User registered successfully.");
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var token = await _userService.AuthenticateUser(loginRequest.Email, loginRequest.Password);
            if (token == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(new { Token = token });
        }
    }
}
