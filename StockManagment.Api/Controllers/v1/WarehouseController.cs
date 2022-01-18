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
    public class WarehouseController : BaseController
    {
        public WarehouseController(
            IUnitOfWork iUnitOfWork,
            UserManager<IdentityUser> userManager,
            IMapper mapper
            ): base(iUnitOfWork, userManager, mapper)
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetWarehouses()
        {
            var warehouses = await _iUnitOfWork.WarehouseRepository.All();
            var result = new PageResult<Warehouse>()
            {
                Content = (List<Warehouse>)warehouses,
                ResultCount = warehouses.Count()
            };
            return Ok(result); 
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouse( [FromBody] WarehouseDTO warehouseDTO)
        {
            var mappedWarehouse = _mapper.Map<Warehouse>(warehouseDTO);
           
            await _iUnitOfWork.WarehouseRepository.Add(mappedWarehouse);
            await _iUnitOfWork.CompleteAsync();

            var result = new Result<Warehouse>();
            result.Content = mappedWarehouse;

            return CreatedAtRoute("GetWarehouse", new {id = mappedWarehouse.Id}, result);
        }

        [HttpGet]
        [Route("GetWarehouse" , Name = "GetWarehouse")]
        public async Task<IActionResult> GetWarehouse(Guid id)
        {
            var warehouse = await _iUnitOfWork.WarehouseRepository.GetById(id);
            var result = new Result<Warehouse>();
            if (warehouse == null)
            {
                result.Error = new Error()
                {
                    Code = 401,
                    Message = "User not Found",
                    Type = "Bad Request"
                };
                return NotFound(result);
            }

            result.Content = warehouse;
            return Ok(result);
        }
    }
}
