using AutoMapper;
using BussinessObject;
using DataAccess.DTO.RequestModel;
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
        public LipstickMappingProfile() {
            CreateMap<Lipstick, LipstickResponseModel>()
                .ForMember(dest => dest.feedbacks, opt => opt.MapFrom(src => src.Feedbacks))
                .ForMember(dest => dest.imageURLs, opt => opt.MapFrom(src => src.ImageURLs))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.LipstickIngredients, opt => opt.MapFrom(src => src.LipstickIngredients));
            CreateMap<Feedback, FeedbackResponseModel>();
            CreateMap<LipstickIngredient, LipstickIngredientRespnseModel>();
            CreateMap<ImageURL,ImageURLResponseModel>();
            CreateMap<Category, CategoryResponseModel>();

        }

        
    }
}
