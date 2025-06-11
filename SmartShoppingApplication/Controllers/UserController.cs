using common.DTOs;
using common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SmartShoppingApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IService<UserDto> _service;

        public UserController(IService<UserDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _service.GetById(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public ActionResult<UserDto> UpdateUser(int id, [FromForm] UserDto dto)
        {
            var updated = _service.Update(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
