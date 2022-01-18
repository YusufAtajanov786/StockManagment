using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;

namespace StockManagment.Api.Controllers.v1
{

    public class Factura_InController : BaseController
    {
        public Factura_InController(
           IUnitOfWork iUnitOfWork,
           UserManager<IdentityUser> userManager,
           IMapper mapper
           ) : base(iUnitOfWork, userManager, mapper)
        {

        }

       

    }
}
