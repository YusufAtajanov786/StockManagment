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
    internal class WarehouseRepository:GenericRepository<Warehouse>,IWarehouseRepository
    {
        public WarehouseRepository(
           AppDbContext appDbContext,
           ILogger logger
           ) : base(appDbContext, logger)
        {

        }
    }
}
