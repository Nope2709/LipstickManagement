using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Customizations;
using Repository.Repositories.Lipsticks;

namespace LipstickManagementWebAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CustomizationController : Controller
    {
        private readonly ICustomizationRepository _customizeRepo;

        public CustomizationController(ICustomizationRepository customizeRepo)
        {
            _customizeRepo = customizeRepo;
        }

        [HttpPost("customize/new")]
        public async Task<ActionResult<JsonResponse<string>>> CreateCustomize(
            [FromBody] CreateCustomizeRequestModel customize)
        {
            try
            {
                var result = await _customizeRepo.CreateCustomization(customize);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("customize/update")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateCustomize(
            [FromBody] UpdateCustomizeRequestModel customize)
        {
            try
            {
                var result = await _customizeRepo.UpdateCustomization(customize);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        

        [HttpDelete("customize/remove")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteCustomize(int id)
        {
            try
            {
                var result = await _customizeRepo.DeleteCustomization(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("customize/get-lipstick-by-id")]
        public async Task<ActionResult<JsonResponse<CustomizeResponseModel>>> GetCustomizeByID(int id)
        {
            try
            {
                var result = await _customizeRepo.GetCustomizationByID(id);
                return Ok(new JsonResponse<CustomizeResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }


        }
    }
}
