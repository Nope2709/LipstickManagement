using AutoMapper;
using BussinessObject;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Lipsticks
{
    public class LipstickMappingProfile : Profile
    {
        public LipstickMappingProfile() { CreateMap<Lipstick, LipstickResponseModel>(); }
        
    }
}
