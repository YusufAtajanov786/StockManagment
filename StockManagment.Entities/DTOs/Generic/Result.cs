using StockManagment.Entities.DTOs.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DTOs.Generic
{
    public class Result<T>
    {
        public T Content { get; set; }

        public Error Error { get; set; }

        public bool IsSucces => Error == null;

        public DateTime ResponseTime { get; set; } = DateTime.UtcNow;
    }
}
