using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartShopping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // TODO: Add your service interface
        // private readonly IProductService _productService;

        public ProductController(/*IProductService productService*/)
        {
            // _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            // TODO: Implement get all products
            return Ok(new List<ProductDto>());
        }

        [HttpGet("{barcode}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string barcode)
        {
            // TODO: Implement get product by barcode
            return Ok(new ProductDto());
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto product)
        {
            // TODO: Implement create product
            return CreatedAtAction(nameof(GetProduct), new { barcode = product.Barcode }, product);
        }

        [HttpPut("{barcode}")]
        public async Task<IActionResult> UpdateProduct(string barcode, ProductUpdateDto product)
        {
            if (barcode != product.Barcode)
            {
                return BadRequest();
            }

            // TODO: Implement update product
            return NoContent();
        }

        [HttpDelete("{barcode}")]
        public async Task<IActionResult> DeleteProduct(string barcode)
        {
            // TODO: Implement delete product
            return NoContent();
        }
    }
} 