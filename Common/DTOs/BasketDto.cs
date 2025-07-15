using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class BasketItemDto
    {
        public string Barcode { get; set; }
        public int Quantity { get; set; }
    }

    public class BasketCheckRequestDto
    {
        public List<BasketItemDto> Items { get; set; }
    }

}