using BussinessObject;
using DataAccess.Addresses;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using DataAccess.Lipsticks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Addresses
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AddressDAO _addressDAO;
        public AddressRepository(AddressDAO addressDAO) 
        {
            _addressDAO = addressDAO;
        }

        public async Task<string> CreateAddress(CreateAddressRequestModel f) => await _addressDAO.CreateAddress(f); 
        

        public async Task<string> DeleteAddress(int id) => await _addressDAO.DeleteAddress(id);



        public async Task<AddressResponseModel> GetAddressByID(int id) => await _addressDAO.GetAddressByID(id);



        public async Task<string> UpdateAddress(UpdateAddressRequestModel f) => await _addressDAO.UpdateAddress(f);   


    }
}
