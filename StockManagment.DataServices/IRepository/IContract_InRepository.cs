using StockManagment.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.IRepository
{
    public interface IContract_InRepository : IGenericRepository<Contract_in>
    {
        Task<IEnumerable<Contract_in>> GetAllContractsOfWarehouse(Guid warehouseId);
    }
}
