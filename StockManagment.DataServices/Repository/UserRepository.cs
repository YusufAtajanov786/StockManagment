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
