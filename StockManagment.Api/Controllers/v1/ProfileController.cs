using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Errors;
using StockManagment.Entities.DTOs.Generic;

namespace StockManagment.Api.Controllers.v1
{
    
    public class ProfileController : BaseController
    {
        public ProfileController(IUnitOfWork iUnitOfWork, UserManager<IdentityUser> userManager)
           : base(iUnitOfWork, userManager)
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var loggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var result = new Result<User>();

            if (loggedInUser == null)
            {
                result.Error = new Error()
                {
                    Code = 400,
                    Message = "User not found",
                    Type = "Bad Request"
                };
                return BadRequest(result);
            }

            var identityId = new Guid(loggedInUser.Id);
            var profile = await _iUnitOfWork.UserRepository.GetByIdentityId(identityId);
            if (profile == null)
            {
                result.Error = new Error()
                {
                    Code = 400,
                    Message = "User not found",
                    Type = "Bad Request"
                };
                return BadRequest(result);
            }
            result.Content = profile;
            return Ok(result);
        }
    }
}
