using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public IList<UserWarehouses> UserWarehouses { get; set; }
    }
}
