using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Brand { get; set; }

        public string Category { get; set; }

        public string Barcode { get; set; }
        public double Price { get; set; }


        public string ImageUrl { get; set; }
    }

}
