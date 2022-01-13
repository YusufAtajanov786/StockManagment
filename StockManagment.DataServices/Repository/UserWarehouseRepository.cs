using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockManagment.DataServices.Data;
using StockManagment.DataServices.IRepository;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.Repository
{
    public class UserWarehouseRepository : GenericRepository<UserWarehouses>, IUserWarehouseRepository
    {
        public UserWarehouseRepository(
           AppDbContext appDbContext,
           ILogger logger
           ) : base(appDbContext, logger)
        {

        }

        public async Task<IEnumerable<GetWarehouseOfUserByIdDTO>> GetWarehouseOfUserByUserId(Guid userId)
        {
            return await dbSet.Where( x => x.UserId == userId ).Select( warehouses => new GetWarehouseOfUserByIdDTO()
            {
                WarehouseId = warehouses.WarehouseId,
                Warehous = warehouses.Warehouse.Name.ToString(),
                Address = warehouses.Warehouse.Address.ToString()

            } ).ToListAsync();
        }
    }
}
