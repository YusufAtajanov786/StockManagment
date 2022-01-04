using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;

namespace StockManagment.Api.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
      

        public UserController(IUnitOfWork iUnitOfWork, UserManager<IdentityUser> userManager)
            :base(iUnitOfWork, userManager)
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
