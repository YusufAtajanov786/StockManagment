using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;

namespace StockManagment.Api.Controllers.v1
{
  
    public class UserController : BaseController
    {
      

        public UserController(IUnitOfWork iUnitOfWork)
            :base(iUnitOfWork)
        {
           
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _iUnitOfWork.UserRepository.All();
            return Ok(users);
        }
    }
}
