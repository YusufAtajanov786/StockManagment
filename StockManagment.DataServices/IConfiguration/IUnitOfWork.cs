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

        IRefreshTokenReposiroty RefreshTokenReposiroty { get; }

        IWarehouseRepository WarehouseRepository { get; }

        IUserWarehouseRepository UserWarehouseRepository { get; }

        IContract_InRepository Contract_InRepository { get; }

        IFactura_InRepository Factura_InRepository { get; }

        IMockProductsRepository MockProductsRepository { get; }

        Task CompleteAsync();
    }
}
