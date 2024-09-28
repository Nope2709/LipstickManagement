
using LipstickManagementAPI.DTO.RequestModel;
using LipstickManagementAPI.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Users
{
    public interface IUserService
    {
        Task<LoginResponseModel> Login(LoginRequestModel loginRequest);
        Task<string> CreateUser(CreateUserRequestModel user);
        Task<List<UserResponseModel>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize);
        Task<UserResponseModel> GetUserByEmail(string email);
        Task<string> UpdateUser(UpdateUserRequestModel user);
        Task<string> DeleteUser(int id);
        Task<string> ChangePasswordUser(ChangePasswordRequestModel user);
    }
}
