using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.Entities.DTOs.Incoming.Contract_In
{
    public class PostContractDTO
    {
        public string FirmName { get; set; }

        public int ContractNumber { get; set; }

        public decimal? ContractSumma { get; set; }
        public decimal? InSummaIntoContract { get; set; }
        public decimal? OutSummaFromContract { get; set; }

        public string FrimPhone { get; set; }

        public string BankName { get; set; }

        public string Description { get; set; }

        public string ContractDate { get; set; }

        public string WarehouseId { get; set; }        

        public string UserId { get; set; }

       
    }
}
