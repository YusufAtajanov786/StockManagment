using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using StockManagment.Configuration.Messages;
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
        public async Task<IActionResult> GetUser([FromBody] UserGetDTO userGetDTO)
        {
            Log.Information($"{userGetDTO.Id}");
            var user = await _iUnitOfWork.UserRepository.GetById( new Guid(userGetDTO.Id));
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

      /*  [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid id)
        {

        }*/

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody]UserUpdateDTO userDTO)
        {
            if (ModelState.IsValid)
            {   

                var mappedUser = _mapper.Map<User>(userDTO);
                var isUpdated = await _iUnitOfWork.UserRepository.UpdateUser(mappedUser);
                var result = new Result<User>();
                if(isUpdated)
                {
                    await _iUnitOfWork.CompleteAsync();
                    result.Content = mappedUser;
                    return Ok(result);
                }
                result.Error = PopulateError(404,
                         ErrorMessages.User.UserNotFound,
                         ErrorMessages.Generic.InvalidPayload);
                return BadRequest(result);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
