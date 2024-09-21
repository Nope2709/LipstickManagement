using AutoMapper;
using BussinessObject;
using LipstickManagementAPI.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() 
        {
            CreateMap<Account, LoginResponseModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName));

            CreateMap<Account, UserResponseModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.RoleName));
        }
    }
}
