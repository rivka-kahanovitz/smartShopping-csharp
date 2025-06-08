using Microsoft.AspNetCore.Mvc;
using Mock;
using Microsoft.AspNetCore.Authorization;
using Service.Service;
using common.DTOs;

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataBase _context;
        private readonly ProductService _productService;

        public ProductController(DataBase context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        // שליפת כל המוצרים
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            var products = _context.Products
                .Select(p => new ProductDto
                {
                    //Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category,
                    Barcode = p.Barcode

                })
                .ToList();

            return Ok(products);
        }

        // שליפת מוצר לפי מזהה
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var p = _context.Products.Find(id);
            if (p == null) return NotFound();

            var dto = new ProductDto
            {
                //Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Category = p.Category,
                Barcode = p.Barcode
            };

            return Ok(dto);
        }

        // הוספת מוצר חדש
        [Authorize]
        [HttpPost]
        public ActionResult<int> Create([FromForm] ProductCreateDto dto)
        {
            // שולף את ה־userId מהטוקן
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
                return Unauthorized("Missing user ID in token.");

            int userId = int.Parse(userIdClaim.Value);

            // קורא לשירות שמטפל בלוגיקה (כולל רשימת קניות)
            _productService.UserIdForAddIrem(userId);

            return Ok();
        }

        // עדכון מוצר
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromForm] ProductUpdateDto dto)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            product.Name = dto.Name;
            product.ImageUrl = dto.ImageUrl;
            product.Category = dto.Category;

            _context.SaveChanges();
            return NoContent();
        }

        // מחיקת מוצר
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
