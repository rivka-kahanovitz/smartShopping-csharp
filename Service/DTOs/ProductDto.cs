using System.ComponentModel.DataAnnotations;

namespace Service.DTOs
{
    // DTO להצגת מוצר
    public class ProductDto
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        [Required]
        public string Barcode { get; set; } // ✅ חשוב!
    }


    // DTO להוספת מוצר
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]

        public string Category { get; set; }
    }

    // DTO לעדכון מוצר
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [Required]

        public string Category { get; set; }
    }
}