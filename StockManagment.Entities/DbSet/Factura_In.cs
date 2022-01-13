using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class Factura_In : BaseEntity
    {
        public Guid ContractId { get; set; }

        [ForeignKey(nameof(ContractId))]
        public Contract_in Contract_In { get; set; }

        public int FacturaNumber { get; set; }

        public decimal FacturaSumma { get; set; }

        public Guid WarehouseId { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }


    }
}
