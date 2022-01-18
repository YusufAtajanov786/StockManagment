using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockManagment.Configuration.Messages;
using StockManagment.DataServices.IConfiguration;
using StockManagment.Entities.DbSet;
using StockManagment.Entities.DTOs.Generic;
using StockManagment.Entities.DTOs.Incoming.Contract_In;

namespace StockManagment.Api.Controllers.v1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class Contract_InController : BaseController
    {
        public Contract_InController(
            IUnitOfWork iUnitOfWork,
            UserManager<IdentityUser> userManager,
            IMapper mapper
            ): base(iUnitOfWork, userManager, mapper)
        {

        }

        [HttpGet]
        public async Task<IActionResult> GetAllContractsOfWarehouse( [FromBody] GetAllContractsOfWarehouseDTO getAllContractsOfWarehouseDTO )
        {
            var contracts = await _iUnitOfWork.Contract_InRepository
                                .GetAllContractsOfWarehouse(new Guid(getAllContractsOfWarehouseDTO.id));
            var result = new PageResult<Contract_in>();
            result.Content = (List<Contract_in>)contracts;
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostContract( [FromBody] PostContractDTO postContractDTO)
        {
            var mappedContract = _mapper.Map<Contract_in>(postContractDTO);
            await _iUnitOfWork.Contract_InRepository.Add(mappedContract);
            await _iUnitOfWork.CompleteAsync();

            var result = new Result<Contract_in>();
            result.Content = mappedContract;
            return CreatedAtRoute("GetContract", new { id = mappedContract.Id }, result);
        }

        [HttpGet]
        [Route("GetContract", Name = "GetContract")]
        public async Task<IActionResult> GetContract(Guid id )
        {
            var contract = await _iUnitOfWork.Contract_InRepository.GetById(id);
            var result = new Result<Contract_in>();
            if (contract == null)
            {
                result.Error = PopulateError(404,
                        ErrorMessages.User.UserNotFound,
                        ErrorMessages.Generic.InvalidPayload);
                return BadRequest(result);
            }

            result.Content = contract;
            return Ok(result);
        }

    }
}
