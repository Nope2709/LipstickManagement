using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Addresses
{
    public interface IAddressRepository
    {
        Task<string> CreateAddress(CreateAddressRequestModel f);

        Task<string> UpdateAddress(UpdateAddressRequestModel f);

        Task<string> DeleteAddress(int id);



        Task<AddressResponseModel> GetAddressByID(int id);
    }
}
