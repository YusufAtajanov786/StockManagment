using StockManagment.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.IRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> GetByIdentityId(Guid identityId);

        Task<bool> UpdateUser(User user);
    }
}
