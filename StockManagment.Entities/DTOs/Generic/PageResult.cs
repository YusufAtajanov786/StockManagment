using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DTOs.Generic
{
    public class PageResult<T>:Result<List<T>>
    {
        public int Page { get; set; }

        public int ResultCount { get; set; }

        public int ResultsPerpage { get; set; }
    }
}
