using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System.Linq;
using Mock;
using common.DTOs;
using Service;
using Service.Utils;
using Microsoft.AspNetCore.Authorization;
using common.Interfaces;
using System.Security.Claims;
using Repository.Interfaces;
using Microsoft.Extensions.Configuration; // חשוב

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IService<UserLoginDto> _context;
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;  // הוסף שדה IConfiguration

        // תוסיף את IConfiguration בקונסטרקטור
        public UserLoginController(IService<UserLoginDto> context, IRepository<User> userRepository, IConfiguration configuration)
        {
            _context = context;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] UserLoginDto dto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u =>
                u.Email == dto.Email && u.Password == PasswordHasher.Hash(dto.Password));

            if (user == null)
                return Unauthorized("שם משתמש או סיסמה שגויים");

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var secretKey = _configuration["Jwt:SecretKey"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var (token, expiresAt) = TokenGenerator.GenerateToken(claims, secretKey, issuer, audience);

            return Ok(new { token, expiresAt });
        }

    }
}
