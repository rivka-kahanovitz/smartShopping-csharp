using common.DTOs;
using common.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using Service.Service;
using System.Collections.Generic;

namespace SmartShoppingApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService service)
        {
            _userService = service;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            return Ok(_userService.GetAll());
        }
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult<UserDto> UpdateUser(int id, [FromForm] UserDto dto)
        {
            var updated = _userService.Update(id, dto);
            return Ok(updated);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
        [Authorize]
        [HttpPut("toggle-admin/{userId}")]
        public IActionResult ToggleAdmin(int userId)
        {
            // שליפת המזהה מתוך ה-Claims
            var idClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(idClaim))
                return Unauthorized("לא הצלחנו לזהות את המשתמש");

            int currentUserId = int.Parse(idClaim);

            // שליפת המשתמש הנוכחי
            var currentUser = _userService.GetById(currentUserId);
            if (currentUser == null)
                return NotFound("המשתמש הנוכחי לא נמצא");

            // הרשאה - רק אדמין
            if (!currentUser.IsAdmin)
                return Forbid("רק מנהל יכול לשנות הרשאות");

            // שליפת המשתמש שאותו רוצים לעדכן
            var user = _userService.GetById(userId);
            if (user == null)
                return NotFound("המשתמש לא נמצא");

            // הפיכת שדה isAdmin
            user.IsAdmin = !user.IsAdmin;

            // עדכון המשתמש
            var updated = _userService.Update(user.Id, user);

            return Ok(updated);
        }
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                await _userService.ResetPasswordAsync(dto.Email);
                return Ok("סיסמה חדשה נשלחה למייל.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
