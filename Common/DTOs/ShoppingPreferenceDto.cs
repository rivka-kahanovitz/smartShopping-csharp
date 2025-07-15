using common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ShoppingPreferenceDto
    {
        public bool IsOnline { get; set; } // true = משלוח, false = באיסוף
        public ShoppingListDto ShoppingList { get; set; } // כל הסל
    }
}
