using AutoMapper;
using BussinessObject;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Customizes
{
    public class CustomizeMappingProfile : Profile
    {
        public CustomizeMappingProfile() 
        { 
            CreateMap<Customization, CustomizeResponseModel>()
                .ForMember(dest => dest.Lipstick, opt => opt.MapFrom(src => src.Lipstick.Name)); 
        }
    }
}
