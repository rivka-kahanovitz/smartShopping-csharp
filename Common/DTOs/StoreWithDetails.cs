using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class StoreWithDetails
    {
        public Stores Store { get; set; }             // האובייקט המלא של החנות
        public double TotalPrice { get; set; }        // המחיר של כל הסל בחנות הזו
        public int DistanceInMeters { get; set; }     // מרחק מהמשתמש (לצורך סינון)
        public string DistanceText { get; set; }      // תיאור ידידותי, כמו "8.4 ק״מ"
    }

}
