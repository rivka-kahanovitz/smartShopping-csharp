using Microsoft.AspNetCore.Mvc;
using Repository.Entities;
using Mock;
using Service.DTOs; // ודא שזו התיקייה שבה נמצאים ה-DTO

namespace SmartShoppingApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DataBase _context;

        public ProductController(DataBase context)
        {
            _context = context;
        }

        // שליפת כל המוצרים
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            var products = _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category
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
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Category = p.Category
            };

            return Ok(dto);
        }

        // הוספת מוצר חדש
        [HttpPost]
        public ActionResult<int> Create([FromForm] ProductCreateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
                Category = dto.Category
            };

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product.Id);
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
