using AutoMapper;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Incoming;

namespace StockManagment.Api.Profiles
{
    public class UserWarehouseProfile:Profile
    {
        public UserWarehouseProfile()
        {
            CreateMap<JoinWarehouseToUserDTO, UserWarehouses>();
        }
    }
}
