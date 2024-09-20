namespace LipstickManagementAPI.DTO.ResponseModel
{
    public class UserResponseModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Role { get; set; }
    }

    public class LoginResponseModel
    {
        public string Email { get; set; }
        public int ID { get; set; }
        public string Role { get; set; }

    }
}
