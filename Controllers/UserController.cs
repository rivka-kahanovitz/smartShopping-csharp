using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using System.Threading.Tasks;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // TODO: Add your service interface
        // private readonly IUserService _userService;

        public UserController(/*IUserService userService*/)
        {
            // _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto loginDto)
        {
            // TODO: Implement user login
            // Return JWT token or other authentication response
            return Ok("JWT Token will be returned here");
        }

        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp(UserSignUpDto signUpDto)
        {
            // TODO: Implement user registration
            // Return JWT token or other authentication response
            return Ok("JWT Token will be returned here");
        }
    }
} 