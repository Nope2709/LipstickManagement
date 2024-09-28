using AutoMapper;
using BussinessObject;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Addresses
{
    public class AddressProfileMapping : Profile
    {
        public AddressProfileMapping()
        {
            CreateMap<Address, AddressResponseModel>();
               
        }
    }
}
