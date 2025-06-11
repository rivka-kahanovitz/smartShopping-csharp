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

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserLoginController : ControllerBase
    {
        private readonly IService<UserLoginDto> _context;
        private readonly IRepository<User> _userRepository;

        public UserLoginController(IService<UserLoginDto> context, IRepository<User> userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] UserLoginDto dto)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u =>
    u.Email == dto.Email && u.Password == PasswordHasher.Hash(dto.Password));


            if (user == null)
                return Unauthorized("שם משתמש או סיסמה שגויים");

            // ✅ יוצרים Claims עם גם Email וגם Id
            var claims = new[]
            {
        new Claim("id", user.Id.ToString()),                         // חשוב! חובה לשים אותו בשם "id"
        new Claim(ClaimTypes.Email, user.Email)
    };

            var token = TokenGenerator.GenerateToken(
                claims,
                "ThisIsAReallyStrongSecretKey123456789!", // אפשר מה־Configuration
                "SmartShoppingAPI",
                "SmartShoppingClient"
            );

            return Ok(new { token });
        }



    }
}
