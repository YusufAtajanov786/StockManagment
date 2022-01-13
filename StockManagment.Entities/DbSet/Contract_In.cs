using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DbSet
{
    public class Contract_in : BaseEntity
    {
        public string FirmName { get; set; }

        public int ContractNumber { get; set; }

        public decimal ContractSumma { get; set; }
        public decimal InSummaIntoContract { get; set; }
        public decimal OutSummaFromContract { get; set; }

        public string FrimPhone { get; set; }

        public string BankName { get; set; }

        public string Description { get; set; }

        public DateTime ContractDate { get; set; }

        public Guid WarehouseId { get; set; }

        [ForeignKey(nameof(WarehouseId))]
        public Warehouse Warehouse { get; set; }

        public Guid UserId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }




    }
}
