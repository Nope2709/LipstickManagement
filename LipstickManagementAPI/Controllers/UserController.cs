
using HotPotToYou.Controllers.ResponseType;
using HotPotToYou.Service.Jwt;
using LipstickManagementAPI.DTO.RequestModel;
using LipstickManagementAPI.DTO.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories.Users;
using System.Net.Mime;

namespace LipstickManagementWebAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("user/login")]
        public async Task<ActionResult<JsonResponse<string>>> Login(
                       [FromBody] LoginRequestModel loginRequest)
        {
            try
            {
                var result = await _userService.Login(loginRequest);
                var token = _jwtService.CreateToken(result.Id, result.Role);
                return Ok(new JsonResponse<string>(token));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("user/register")]

        public async Task<ActionResult<JsonResponse<string>>> CreateUser(
            [FromBody] CreateUserRequestModel user)
        {
            try
            {
                var result = await _userService.CreateUser(user);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<List<UserResponseModel>>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize)
        {
            try
            {
                var result = await _userService.GetUsers(search, gender, sortBy, pageIndex, pageSize);
                return Ok(new List<UserResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        [AllowAnonymous]
        [HttpGet("user/get-user-by-email")]
        public async Task<ActionResult<JsonResponse<UserResponseModel>>> GetUserByEmail(string email)
        {
            try
            {
                var result = await _userService.GetUserByEmail(email);
                return Ok(new JsonResponse<UserResponseModel>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }

        [HttpPut("user/update")]
        public async Task<ActionResult<JsonResponse<string>>> UpdateUser(
            [FromBody] UpdateUserRequestModel user)
        {
            try
            {
                var result = await _userService.UpdateUser(user);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        [HttpPut("user/change-password")]
        public async Task<ActionResult<JsonResponse<string>>> ChangePassword(
            [FromBody] ChangePasswordRequestModel user)
        {
            try
            {
                var result = await _userService.ChangePasswordUser(user);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        
    }
}
