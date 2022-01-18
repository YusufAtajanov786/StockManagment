using AutoMapper;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Incoming.Contract_In;

namespace StockManagment.Api.Profiles
{
    public class Contract_InProfile:Profile
    {
        public Contract_InProfile()
        {
            CreateMap<PostContractDTO, Contract_in>()
                .ForMember(
                dest => dest.ContractDate,
                from => from.MapFrom(x => $"{Convert.ToDateTime(x.ContractDate)}"));
        }
    }
}
