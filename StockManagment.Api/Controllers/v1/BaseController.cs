using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;

namespace StockManagment.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _iUnitOfWork;

        protected UserManager<IdentityUser> _userManager;

        protected readonly IMapper _mapper;

        public BaseController(
       IUnitOfWork unitOfWork,
       UserManager<IdentityUser> userManager,
       IMapper mapper
       )
        {
            _iUnitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }
    }
}
