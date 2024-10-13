using AutoMapper;
using BussinessObject;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Orders
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderResponseModel>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.Payment.Id));


        }
    }
}
