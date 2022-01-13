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
using StockManagment.Entities.DTOs.Outgoing;

namespace StockManagment.Api.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserWarehouseController : BaseController
    {
        public UserWarehouseController(IUnitOfWork iUnitOfWork, UserManager<IdentityUser> userManager, IMapper mapper)
            : base(iUnitOfWork, userManager, mapper)
        {

        }

        [HttpPost]
        public async  Task<IActionResult> JoinWarehouseToUser( [FromBody] JoinWarehouseToUserDTO joinWarehouseToUserDTO)
        {
            var mappedUserWarehouse = _mapper.Map<UserWarehouses>(joinWarehouseToUserDTO);
            await _iUnitOfWork.UserWarehouseRepository.Add(mappedUserWarehouse);
            await _iUnitOfWork.CompleteAsync();
            var result = new Result<UserWarehouses>();
            result.Content = mappedUserWarehouse;
            return CreatedAtRoute("GetUserWarehouse", new { id = mappedUserWarehouse.Id }, result);
        }

        [HttpGet]
        [Route("GetUserWarehouse", Name = "GetUserWarehouse")]
        public async Task<IActionResult> GetUserWarehouse([FromBody]string id)
        {
            var warehouse = await _iUnitOfWork.UserWarehouseRepository.GetById(new Guid(id));
            var result = new Result<UserWarehouses>();
            if (warehouse == null)
            {
                result.Error = new Error()
                {
                    Code = 401,
                    Message = "GetUserWarehouse not Found",
                    Type = "Bad Request"
                };
                return NotFound(result);
            }

            result.Content = warehouse;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetWarehouseOfUser", Name = "GetWarehouseOfUser")]
        public async Task<IActionResult> GetWarehouseOfUser([FromBody] GetWarehouseOfUserDTO getWarehouseOfUserDTO)
        {
            var warehouses = await _iUnitOfWork.UserWarehouseRepository.GetWarehouseOfUserByUserId( new Guid(getWarehouseOfUserDTO.Id));
            var result = new PageResult<GetWarehouseOfUserByIdDTO>();
            if (warehouses == null)
            {
                result.Error = new Error()
                {
                    Code = 401,
                    Message = "GetUserWarehouse not Found",
                    Type = "Bad Request"
                };
                return NotFound(result);
            }
            result.Content = (List<GetWarehouseOfUserByIdDTO>)warehouses;
            return Ok(result);
        }

    }
}
