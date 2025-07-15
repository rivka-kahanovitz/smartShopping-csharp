using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Stores
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string StorName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public List<AllProductsStores> AllProductsStores { get; set; } = new List<AllProductsStores>();

    }
}