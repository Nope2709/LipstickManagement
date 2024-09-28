using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories.Addresses;
using Repositories.Repositories.Feedbacks;
using Repository.Repositories.Lipsticks;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AddressController : Controller
    {
        
            private readonly IAddressRepository _addRepo;
           

            public AddressController(IAddressRepository addRepo)
            {
                _addRepo = addRepo;
            }

            [HttpPost("address/new")]
            public async Task<ActionResult<JsonResponse<string>>> CreateAddress(
                [FromBody] CreateAddressRequestModel address, int lipId)
            {
                try
                {
                  
                    var result = await _addRepo.CreateAddress(address);
                    return Ok(new JsonResponse<string>(result));
                }
                catch (Exception ex)
                {
                    return BadRequest(new JsonResponse<string>(ex.Message));
                }
            }

            [HttpPut("address/update")]
            public async Task<ActionResult<JsonResponse<string>>> UpdateFeedback(
                [FromBody] UpdateAddressRequestModel address)
            {
                try
                {
                  
                    var result = await _addRepo.UpdateAddress(address);
                    return Ok(new JsonResponse<string>(result));
                }
                catch (Exception ex)
                {
                    return BadRequest(new JsonResponse<string>(ex.Message));
                }
            }



            [HttpDelete("address/remove")]
            public async Task<ActionResult<JsonResponse<string>>> DeleteAddress(int id)
            {
                try
                {
                    var result = await _addRepo.DeleteAddress(id);
                    return Ok(new JsonResponse<string>(result));
                }
                catch (Exception ex)
                {
                    return BadRequest(new JsonResponse<string>(ex.Message));
                }
            }

            [HttpGet("address/get-feedback-by-id")]
            public async Task<ActionResult<JsonResponse<AddressResponseModel>>> GetAddressByID(int id)
            {
                try
                {

                    var result = await _addRepo.GetAddressByID(id);
                    return Ok(new JsonResponse<AddressResponseModel>(result));
                }
                catch (Exception ex)
                {
                    return BadRequest(new JsonResponse<string>(ex.Message));
                }


            }
        }
}
