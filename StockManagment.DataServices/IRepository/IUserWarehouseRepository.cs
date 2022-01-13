using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Outgoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.IRepository
{
    public interface IUserWarehouseRepository : IGenericRepository<UserWarehouses>
    {
        Task<IEnumerable<GetWarehouseOfUserByIdDTO>> GetWarehouseOfUserByUserId(Guid userId);
    }
}
