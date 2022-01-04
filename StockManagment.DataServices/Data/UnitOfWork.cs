using Microsoft.Extensions.Logging;
using StockManagment.DataServices.IConfiguration;
using StockManagment.DataServices.IRepository;
using StockManagment.DataServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private readonly AppDbContext _appDbContext;

        private readonly ILogger _logger;
        public IUserRepository UserRepository { get; private set; }

        public IRefreshTokenReposiroty RefreshTokenReposiroty { get; private set; }

        public UnitOfWork(AppDbContext appDbContext, ILoggerFactory loggerFactory)
        {
            this._appDbContext = appDbContext;
            this._logger = loggerFactory.CreateLogger("db_logs");

            UserRepository = new UserRepository(appDbContext, _logger);
            RefreshTokenReposiroty = new RefreshTokenRepository(appDbContext, _logger);


        }
        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
