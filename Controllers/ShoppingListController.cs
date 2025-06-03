using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        // TODO: Add your service interface
        // private readonly IShoppingListService _shoppingListService;

        public ShoppingListController(/*IShoppingListService shoppingListService*/)
        {
            // _shoppingListService = shoppingListService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListDto>>> GetAllLists()
        {
            // TODO: Implement get all shopping lists
            return Ok(new List<ShoppingListDto>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShoppingListDto>> GetList(int id)
        {
            // TODO: Implement get shopping list by id
            return Ok(new ShoppingListDto());
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingListDto>> CreateList(ShoppingListDto shoppingList)
        {
            // TODO: Implement create shopping list
            return CreatedAtAction(nameof(GetList), new { id = 1 }, shoppingList);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateList(int id, ShoppingListDto shoppingList)
        {
            if (id != shoppingList.Id)
            {
                return BadRequest();
            }

            // TODO: Implement update shopping list
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            // TODO: Implement delete shopping list
            return NoContent();
        }
    }
} 