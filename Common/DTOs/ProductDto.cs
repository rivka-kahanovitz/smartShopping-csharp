using System.ComponentModel.DataAnnotations;

namespace common.DTOs
{
    // DTO להצגת מוצר
    public class ProductDto
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        [Required]
        public string Barcode { get; set; } 
    }


    // DTO להוספת מוצר
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Barcode { get; set; }
        public string Brand { get; set; }

    }


    // DTO לעדכון מוצר
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string Category { get; set; }
        public string Barcode { get; set; }

    }
    public class ProductPriceResultDto
    {
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public string ChainName { get; set; }
        public string StoreName { get; set; }
        public decimal Price { get; set; }
    }

}