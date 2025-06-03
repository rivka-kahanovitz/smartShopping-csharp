using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System.Linq;
using Mock;
using Service.DTOs;
using Service;
using Service.Utils;
using Microsoft.AspNetCore.Authorization;
using Service.Interfaces;
namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IService<UserLoginDto> _context;

        public UserController(IService<UserLoginDto> context)   
        {
            _context = context;
        } 
        [HttpPost("login")]
        public void Login([FromForm]UserLoginDto dto)
        {
            var user = _context.GetAll().FirstOrDefault(u =>
                u.Email == dto.Email && u.Password == PasswordHasher.Hash(dto.Password));

            if (user == null)
                throw new UnauthorizedAccessException("שם משתמש או סיסמה שגויים");

            // כאן לא שומרים למסד! רק מאשרים קיום
            // אפשר להחזיר טוקן, מזהה, או פשוט להשאיר ריק
        }
        // הרשמת משתמש חדש עם DTO

        //public ActionResult<int> Signup([FromForm] UserSignUpDto dto)
        //{
        //    if (_context.Users.Any(u => u.Email == dto.Email))
        //        return BadRequest("Email already exists.");
        //    var user = new User
        //    {
        //        Name = dto.Name,
        //        Email = dto.Email,
        //        Password = PasswordHasher.Hash(dto.Password), // כאן ההצפנה
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    return Ok(user.Id);
        //}

        //[HttpPost("login")]
        //public ActionResult<string> Login([FromForm] UserLoginDto dto)
        //{
        //    var user = _context.Users
        //        .FirstOrDefault(u => u.Email == dto.Email && u.Password == PasswordHasher.Hash(dto.Password));

        //    if (user == null)
        //        return Unauthorized();

        //    string secretKey = "ThisIsAReallyStrongSecretKey123456789!";
        //    string token = TokenGenerator.GenerateToken(user.Email, secretKey);

        //    return Ok(token);
        //}


        [Authorize]
        [HttpGet("{id:int}")]
        public UserLoginDto GetById(int id)
        {
            return _context.GetById(id);
        }

        //[HttpGet("by-email/{email}")]
        //public UserLoginDto GetByEmail(string email)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
        //    return user != null ? Ok(user) : NotFound();
        //}

        //[HttpGet("by-password/{password}")]
        //public ActionResult<User> GetByPassword(string password)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Password == password);
        //    return user != null ? Ok(user) : NotFound();
        //}
        //[Authorize]
        //[HttpGet("by-name/{name}")]
        //public ActionResult<User> GetByName(string name)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Name == name);
        //    return user != null ? Ok(user) : NotFound();
        //}

        //[HttpPut("{email}")]
        //public ActionResult UpdateUser(string email, [FromForm] User user)
        //{
        //    var existing = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (existing == null) return NotFound();

        //    existing.Name = user.Name;
        //    existing.Password = user.Password;

        //    _context.SaveChanges();
        //    return NoContent();
        //}

        //[HttpPatch("{email}/name")]
        //public ActionResult UpdateName(string email, [FromForm] string newName)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (user == null) return NotFound();

        //    user.Name = newName;
        //    _context.SaveChanges();
        //    return NoContent();
        //}

        //[HttpPatch("{email}/password")]
        //public ActionResult UpdatePassword(string email, [FromForm] string newPassword)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (user == null) return NotFound();

        //    user.Password = newPassword;
        //    _context.SaveChanges();
        //    return NoContent();
        //}

        //[HttpDelete("{email}")]
        //public ActionResult Delete(string email)
        //{
        //    var user = _context.Users.FirstOrDefault(u => u.Email == email);
        //    if (user == null) return NotFound();

        //    _context.Users.Remove(user);
        //    _context.SaveChanges();
        //    return NoContent();
        //}
    }
}
