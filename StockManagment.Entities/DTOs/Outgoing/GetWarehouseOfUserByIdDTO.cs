using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DTOs.Outgoing
{
    public class GetWarehouseOfUserByIdDTO
    {
        public Guid WarehouseId { get; set; }

        public string Warehous { get; set; }

        public string Address { get; set; }
    }
}
