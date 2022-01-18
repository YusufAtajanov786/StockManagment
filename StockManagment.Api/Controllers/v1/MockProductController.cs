using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;

namespace StockManagment.Api.Controllers.v1
{

    public class MockProductController : BaseController
    {
        public MockProductController(
            IUnitOfWork iUnitOfWork,
            UserManager<IdentityUser> userManager,
            IMapper mapper
            ) : base(iUnitOfWork, userManager, mapper)
        {

        }
    }
}
