using common.DTOs;
using common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace SmartShoppingApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IService<ShoppingListDto> _service;

        public ShoppingListController(IService<ShoppingListDto> service)
        {
            _service = service;
        }

        private bool TrySetUserId()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return false;

            int userId = int.Parse(userIdClaim.Value);

            // שליחה לשירות את ה־userId
            (_service as dynamic).SetUserId(userId);
            return true;
        }

        [HttpPost]
        public ActionResult<ShoppingListDto> Create([FromForm] ShoppingListDto dto)
        {
            if (!TrySetUserId())
                return Unauthorized("Missing user ID in token.");

            var newList = _service.AddItem(dto);
            return CreatedAtAction(nameof(GetById), new { id = newList.Id }, newList);
        }

        [HttpGet]
        public ActionResult<List<ShoppingListDto>> GetAll()
        {
            if (!TrySetUserId())
                return Unauthorized("Missing user ID in token.");

            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingListDto> GetById(int id)
        {
            if (!TrySetUserId())
                return Unauthorized("Missing user ID in token.");

            try
            {
                return Ok(_service.GetById(id));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ShoppingListDto> Update(int id, [FromForm] ShoppingListDto dto)
        {
            if (!TrySetUserId())
                return Unauthorized("Missing user ID in token.");

            try
            {
                var updated = _service.Update(id, dto);
                return Ok(updated);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!TrySetUserId())
                return Unauthorized("Missing user ID in token.");

            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
