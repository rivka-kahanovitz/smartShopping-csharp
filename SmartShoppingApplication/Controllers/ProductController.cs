using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using common.DTOs;
using common.Interfaces;

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IService<ProductDto> _productService;

        public ProductController(IService<ProductDto> productService)
        {
            _productService = productService;
        }

        // שליפת כל המוצרים
        [HttpGet]
        public ActionResult<List<ProductDto>> GetAll()
        {
            return Ok(_productService.GetAll());
        }

        // שליפת מוצר לפי ID
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            try
            {
                return Ok(_productService.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // הוספת מוצר (כולל הוספה לעגלה)
        [Authorize]
        [HttpPost]
        public ActionResult<ProductDto> Create([FromForm] ProductDto dto)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);
            (_productService as dynamic).UserIdForAddIrem(userId);

            try
            {
                var created = _productService.AddItem(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Barcode }, created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // עדכון מוצר
        [HttpPut("{id}")]
        public ActionResult<ProductDto> Update(int id, [FromForm] ProductDto dto)
        {
            try
            {
                var updated = _productService.Update(id, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // מחיקת מוצר
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _productService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
