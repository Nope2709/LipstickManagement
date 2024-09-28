﻿namespace LipstickManagementAPI.DTO.RequestModel
{
    public class UserRequestModel
    {
    }
    public class CreateUserRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }

    public class UpdateUserRequestModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public bool IsEnabled { get; set; }
        public int RoleID { get; set; }
    }

    public class LoginRequestModel
    {
        public string Phone { get; set; }
        
        public string Password { get; set; }
        
    }
    public class ChangePasswordRequestModel
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
