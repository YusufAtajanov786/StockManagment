using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.DataServices.IConfiguration;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Errors;
using StockManagment.Entities.DTOs.Generic;
using StockManagment.Entities.DTOs.Incoming;

namespace StockManagment.Api.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
      

        public UserController(IUnitOfWork iUnitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
            :base(iUnitOfWork, userManager, mapper)
        {
           
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _iUnitOfWork.UserRepository.All();
            return Ok(users);
        }


      

       [HttpPost]
       public async Task<IActionResult> PostUser(UserDTO userDTO)
       {
            var mappedUser = _mapper.Map<User>(userDTO);

            await _iUnitOfWork.UserRepository.Add(mappedUser);
            await _iUnitOfWork.CompleteAsync();

            var result = new Result<User>();
            result.Content = mappedUser;
            return CreatedAtRoute("GetUser", new {id = mappedUser.Id}, result);
       }

        [HttpGet]
        [Route("GetUser", Name = "GetUser")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _iUnitOfWork.UserRepository.GetById(id);
            var result = new Result<User>();
            if (user == null)
            {
                result.Error = new Error()
                {
                    Code = 401,
                    Message = "User not Found",
                    Type = "Bad Request"
                };
                return NotFound(result);
            }


            result.Content = user;
            return Ok(result);
        }


    }
}
