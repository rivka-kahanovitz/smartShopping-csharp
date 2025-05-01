using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Repository.Entities
{

    public class Price
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }    

        [ForeignKey("Store")]
        public int StoreId { get; set; }
        public Stores Store { get; set; }

        [Required]
        public double PriceValue { get; set; }
    }

}
