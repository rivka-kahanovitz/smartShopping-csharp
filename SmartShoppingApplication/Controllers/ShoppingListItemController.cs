using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using common.DTOs;
using common.Interfaces;
using System.Collections.Generic;

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ShoppingListItemController : ControllerBase
    {
        private readonly IService<ShoppingListItemDto> _service;

        public ShoppingListItemController(IService<ShoppingListItemDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ShoppingListItemDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingListItemDto> GetById(int id)
        {
            var item = _service.GetById(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ShoppingListItemDto> Add([FromForm] ShoppingListItemDto dto)
        {
            // שליפת userId מהטוקן
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);

            // הפעלת שירות שיאחסן את ה־userId
            (_service as dynamic).SetUserId(userId);

            var added = _service.AddItem(dto);
            return CreatedAtAction(nameof(GetById), new { id = added.ProductId }, added);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
