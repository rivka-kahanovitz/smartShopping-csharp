using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Repository.Entities
{
    public class AllProductsStores
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Barcode { get; set; }
        [ForeignKey("Stores")]
        public int StoreId { get; set; }
        public Stores Stores { get; set; }
        public double Price { get; set; }
    }
}