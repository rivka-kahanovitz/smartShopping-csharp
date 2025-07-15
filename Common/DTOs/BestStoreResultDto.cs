using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
    public class BestStoreResultDto
    {
        public string StoreName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
