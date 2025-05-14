using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using System.Linq;
using Mock;

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataBase _context;

        public UserController(DataBase context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user.Id); // מחזיר את המזהה שנוצר
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _context.Users.Find(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] User user)
        {
            var existing = _context.Users.Find(id);
            if (existing == null) return NotFound();

            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.Password = user.Password;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("login")]
        public ActionResult<bool> ValidateLogin([FromBody] User login)
        {
            var exists = _context.Users
                .Any(u => u.Email == login.Email && u.Password == login.Password);
            return Ok(exists);
        }

        [HttpPatch("{id}/name")]
        public ActionResult UpdateName(int id, [FromBody] string newName)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Name = newName;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}/email")]
        public ActionResult UpdateEmail(int id, [FromBody] string newEmail)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Email = newEmail;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}/password")]
        public ActionResult UpdatePassword(int id, [FromBody] string newPassword)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            user.Password = newPassword;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
