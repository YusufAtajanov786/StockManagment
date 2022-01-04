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
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenReposiroty
    {
        public RefreshTokenRepository(
            AppDbContext appDbContext,
            ILogger logger)
            : base(appDbContext, logger)
        {

        }
        public async Task<RefreshToken> GetByRefreshToken(string refreshToken)
        {
            try
            {
                return await dbSet.Where(x => x.Token.ToLower() == refreshToken.ToLower())
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} This is all method has generated error", typeof(UserRepository));
                return null;
            }
        }

        public async Task<bool> MarkRefreshTokenAsUsed(RefreshToken refreshToken)
        {
            try
            {
                var token = await dbSet.Where(x => x.Token.ToLower() == refreshToken.Token.ToLower())
                                .AsNoTracking()
                                .FirstOrDefaultAsync();

                if (token == null) return false;

                token.isUsed = refreshToken.isUsed;
                return true;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} This is all method has generated error", typeof(UserRepository));
                return false;
            }
        }
    }
}
