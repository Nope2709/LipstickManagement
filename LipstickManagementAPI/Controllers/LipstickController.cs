using AutoMapper;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Lipsticks;

namespace LipstickManagementWebAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class LipstickController : Controller
    {
        private readonly ILipstickRepositories _lipStickRepo;

        public LipstickController(ILipstickRepositories lipStickRepo, IMapper mapper)
        {
            _lipStickRepo = lipStickRepo;
        }

        [HttpPost("lipstick/new")]
        public async Task<ActionResult<JsonResponse<string>>> CreateLipstick(
            [FromBody] CreateLipstickRequestModel lipstick)
        {
            try
            {
                var result = await _lipStickRepo.CreateLipstick(lipstick);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("lipstick/update")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateLipstick(
            [FromBody] UpdateLipstickRequestModel lipstick)
        {
            try
            {
                var result = await _lipStickRepo.UpdateLipstick(lipstick);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("lipstick")]
        public async Task<ActionResult<List<JsonResponse<LipstickResponseModel>>>> GetLipsticks(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, string? type,
            int pageIndex, int pageSize)
        {
            try
            {
                var result = await _lipStickRepo.GetLipstick(search, sortBy, fromPrice, toPrice, flavorID, size, type, pageIndex, pageSize);
                return Ok(new JsonResponse<List<LipstickResponseModel>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpDelete("lipstick/remove")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteLipstick(int id)
        {
            try
            {
                var result = await _lipStickRepo.DeleteLipstick(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("lipstick/get-lipstick-by-id")]
        public async Task<ActionResult<JsonResponse<LipstickResponseModel>>> GetLisptickByID(int id)
        {
            try
            {
                var result = await _lipStickRepo.GetLipstickByID(id);
                return Ok(new JsonResponse<LipstickResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }


        }
    }
}
