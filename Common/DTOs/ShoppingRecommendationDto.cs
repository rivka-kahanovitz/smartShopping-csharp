using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class ShoppingRecommendationDto
    {
        public List<StoreWithDetails> RecommendedStores { get; set; } // שלוש החנויות הכי קרובות
        public StoreWithDetails CheapestStore { get; set; } // הכי זולה מתוכן
    }

}
