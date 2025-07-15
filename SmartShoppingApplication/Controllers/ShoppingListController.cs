using common.DTOs;
using common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
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
            //_shoppingListService.SetUserId(userId);
            return true;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<ShoppingListDto> Create([FromBody] ShoppingListDto dto)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);

            (_service as dynamic).SetUserId(userId);

            var newList = _service.AddItem(dto);
            return Ok(newList);
        }


        [HttpGet]
        public ActionResult<List<ShoppingListDto>> GetAll()
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);
            (_service as dynamic).SetUserId(userId);

            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingListDto> GetById(int id)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);
            (_service as dynamic).SetUserId(userId);

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
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);
            (_service as dynamic).SetUserId(userId);

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
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);
            (_service as dynamic).SetUserId(userId);

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
