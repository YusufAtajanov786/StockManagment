using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class UserWarehouses : BaseEntity
    {
        public Guid UserId { get; set; }

     
        public User User { get; set; }

        public Guid WarehouseId { get; set; }
      
        public Warehouse Warehouse { get; set; }
    }
}
