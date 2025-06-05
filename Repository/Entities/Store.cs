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
        public string Name { get; set; }

        public string Chain { get; set; }

        public string Address { get; set; }

        //public decimal Latitude { get; set; }

        //public decimal Longitude { get; set; }
    }
}
