using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Repositories.Repositories.Feedbacks;
using Repository.Repositories.Customizations;
using Repository.Repositories.Lipsticks;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FeedbackController : Controller
    {
        private readonly ILipstickRepositories _lipRepo;
        private readonly IFeedbackRepository _feedRepo;

        public FeedbackController(ILipstickRepositories lipRepo, IFeedbackRepository feedRepo)
        {
            _lipRepo = lipRepo;
            _feedRepo = feedRepo;
        }

        [HttpPost("feedback/new/{lipId}")]
        public async Task<ActionResult<JsonResponse<string>>> Feedback(
            [FromBody] CreateFeedbackRequestModel feedback, int lipId)
        {
            try
            {
                if (!await _lipRepo.lipstickExists(lipId))
                {
                    return BadRequest("Lipstick does not Exists");
                }
                var result = await _feedRepo.CreateFeedback(feedback);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpPut("feedback/update/{lipId}")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateFeedback(
            [FromBody] UpdateFeedbackRequestModel feedback, int lipId)
        {
            try
            {
                if (!await _lipRepo.lipstickExists(lipId))
                {
                    return BadRequest("Lipstick does not Exists");
                }
                var result = await _feedRepo.UpdateFeedback(feedback);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }



        [HttpDelete("feedback/remove")]
        public async Task<ActionResult<JsonResponse<string>>> DeleteFeedback(int id)
        {
            try
            {
                var result = await _feedRepo.DeleteFeedback(id);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("feedback/get-feedback-by-id")]
        public async Task<ActionResult<JsonResponse<FeedbackResponseModel>>> GetFeedbackByID(int id)
        {
            try
            {
                
                var result = await _feedRepo.GetFeedbackByID(id);
                return Ok(new JsonResponse<FeedbackResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }


        }
    }
}

