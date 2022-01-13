using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class MockProduct : BaseEntity
    {
        public string Name { get; set; }

        public UnitType UnitType { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }
    }
}
