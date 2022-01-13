using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DTOs.Incoming
{
    public class JoinWarehouseToUserDTO
    {
        public Guid UserId { get; set; }

        public Guid WarehouseId { get; set; }
    }
}
