using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShopping.Controllers
{
    [Route("api/shopping-lists/{listId}/items")]
    [ApiController]
    public class ShoppingListItemController : ControllerBase
    {
        // TODO: Add your service interface
        // private readonly IShoppingListItemService _shoppingListItemService;

        public ShoppingListItemController(/*IShoppingListItemService shoppingListItemService*/)
        {
            // _shoppingListItemService = shoppingListItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingListItemDto>>> GetItems(int listId)
        {
            // TODO: Implement get all items for a shopping list
            return Ok(new List<ShoppingListItemDto>());
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingListItemDto>> AddItem(int listId, ShoppingListItemDto item)
        {
            // TODO: Implement add item to shopping list
            return CreatedAtAction(nameof(GetItems), new { listId }, item);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem(int listId, int itemId, ShoppingListItemDto item)
        {
            // TODO: Implement update shopping list item
            return NoContent();
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteItem(int listId, int itemId)
        {
            // TODO: Implement delete shopping list item
            return NoContent();
        }
    }
} 