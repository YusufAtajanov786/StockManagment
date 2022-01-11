using AutoMapper;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Incoming;

namespace StockManagment.Api.Profiles
{
    public class WarehouseProfile:Profile
    {
        public WarehouseProfile()
        {
            CreateMap<WarehouseDTO, Warehouse>();
        }
    }
}
