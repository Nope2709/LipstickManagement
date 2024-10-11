using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;

namespace LipstickManagementAPI.DTO.ResponseModel
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? Gender { get; set; }
        public string Role { get; set; }
        public List<AddressResponseModel>? Addresses { get; set; }
    }

    public class LoginResponseModel
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string Role { get; set; }

    }
}
