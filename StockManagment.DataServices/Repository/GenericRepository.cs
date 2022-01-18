using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockManagment.DataServices.Data;
using StockManagment.DataServices.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        internal DbSet<T> dbSet;

        protected readonly ILogger _logger;
        public GenericRepository(
            AppDbContext appDbContext,
            ILogger logger
            )
        {
            this._appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            this._logger = logger;
            dbSet = appDbContext.Set<T>();
        }



        public virtual async Task<bool> Add(T entity)
        {
             await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<bool> Delete(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
