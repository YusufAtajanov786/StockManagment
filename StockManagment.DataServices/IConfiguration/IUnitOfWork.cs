using StockManagment.DataServices.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManagment.DataServices.IConfiguration
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        Task CompleteAsync();
    }
}
