using AutoMapper;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Incoming;

namespace StockManagment.Api.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(
                dest => dest.DateOfBirth,
                from => from.MapFrom( x => $"{Convert.ToDateTime(x.DateOfBirth)}") );

            CreateMap<UserUpdateDTO, User>()
                .ForMember(
                dest => dest.DateOfBirth,
                from => from.MapFrom(x => $"{Convert.ToDateTime(x.DateOfBirth)}"))
                .ForMember(
                dest => dest.Id,
                from => from.MapFrom(x => $"{new Guid(x.Id)}"));
        }
    }
}
