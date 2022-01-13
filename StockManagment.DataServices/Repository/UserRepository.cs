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
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        public UserRepository(
            AppDbContext appDbContext,
            ILogger logger
            ) : base(appDbContext, logger)
        {

        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                var objUser = await dbSet.Where(x => x.Status == 1 && x.Id == user.Id)
                                    .FirstOrDefaultAsync();
                if (objUser == null) return false;

                objUser.FirstName = user.FirstName;
                objUser.LastName = user.LastName;
                objUser.Email = user.Email;
                objUser.Phone = user.Phone;
                objUser.UpdateDate = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} This is all method has generated error", typeof(UserRepository));
                return false;
            }
        }
        public async Task<User> GetByIdentityId(Guid identityId)
        {
            try
            {
                return await dbSet.Where(x => x.Status == 1 && x.Identity == identityId)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} This is all method has generated error", typeof(UserRepository));
                return null;
            }
        }

       
    }
}
