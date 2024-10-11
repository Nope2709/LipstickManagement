using LipstickManagementAPI.DTO.RequestModel;
using LipstickManagementAPI.DTO.ResponseModel;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest)
        {
            return await _userRepository.Login(loginRequest);
        }

        public async Task<string> CreateUser(CreateUserRequestModel user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<List<UserResponseModel>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize)
        {
            return await _userRepository.GetUsers(search, gender, sortBy, pageIndex, pageSize);
        }
        public async Task<string> UpdateUser(UpdateUserRequestModel user)
        {
            return await _userRepository.UpdateUser(user);
        }
        public async Task<string> DeleteUser(int id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<string> ChangePasswordUser(ChangePasswordRequestModel user)
        {
            return await _userRepository.ChangePasswordUser(user);
        }

        public async Task<UserResponseModel> GetUserByPhone(string phone)
        {
            return await _userRepository.GetUserByPhone(phone);
        }
    }
}
