using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockManagment.DataServices.Data;
using StockManagment.DataServices.IRepository;
using StockManagment.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.Repository
{
    public class Contract_InRepository:GenericRepository<Contract_in>, IContract_InRepository
    {
        public Contract_InRepository(
           AppDbContext appDbContext,
           ILogger logger
           ) : base(appDbContext, logger)
        {

        }

        public async Task<IEnumerable<Contract_in>> GetAllContractsOfWarehouse( Guid warehouseId)
        {
            return await dbSet.Where(x => x.WarehouseId == warehouseId).ToListAsync();
        }
    }
}
