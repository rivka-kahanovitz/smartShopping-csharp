using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class ShoppingListItem
    {
        [Key]
        public int Id { get; set; }
        //דרוש הסבר  :)

        [ForeignKey("ShoppingList")]
        public int ListId { get; set; }
        public ShoppingList ShoppingList { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;
    }
}
