using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;

namespace StockManagment.Api.Controllers.v1
{
    [Route("api/v{version:apiVersion}[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _iUnitOfWork;

        public BaseController(
       IUnitOfWork unitOfWork     
       )
        {
            _iUnitOfWork = unitOfWork;
          
        }
    }
}
