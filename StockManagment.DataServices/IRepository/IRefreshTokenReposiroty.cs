using StockManagment.Entities.DbSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.IRepository
{
    public interface IRefreshTokenReposiroty: IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GetByRefreshToken(string refreshToken);

        Task<bool> MarkRefreshTokenAsUsed(RefreshToken refreshToken);
    }

    
}
