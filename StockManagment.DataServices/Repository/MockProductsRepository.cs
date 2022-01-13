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
    public class MockProductsRepository:GenericRepository<MockProduct>, IMockProductsRepository
    {

        public MockProductsRepository(
           AppDbContext appDbContext,
           ILogger logger
           ) : base(appDbContext, logger)
        {

        }
    }
}
