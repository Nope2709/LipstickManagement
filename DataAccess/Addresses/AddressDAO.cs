using AutoMapper;
using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Addresses
{
    public class AddressDAO
    {
        private readonly LipstickManagementContext _context = new();
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public AddressDAO(LipstickManagementContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }



        public async Task<string> CreateAddress(CreateAddressRequestModel f)
        {


            var newAddress = new Address()
            {
                AccountId = f.AccountId,
                StreetAddress = f.StreetAddress,
                City = f.City,
                ZipCode = f.ZipCode,

            };

            _context.Addresses.Add(newAddress);
            try
            {
                await _context.SaveChangesAsync();
                return "Create Address Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> UpdateAddress(UpdateAddressRequestModel f)
        {
            var fb = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == f.Id);
            if (fb == null)
                throw new InvalidDataException("Address is not found");


            
            fb.AccountId = f.Id;
            fb.StreetAddress = f.StreetAddress;
            fb.City = f.City;
            fb.ZipCode = f.ZipCode;



            _context.Addresses.Update(fb);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Address Successfully";
            else
                return "Update Failed";
        }

        public async Task<string> DeleteAddress(int id)
        {
            var fb = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == id);
            if (fb == null)
                throw new InvalidDataException("Feedback is not found");



            _context.Addresses.Remove(fb);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Successfully";
            else
                return "Delete Failed";
        }



        public async Task<AddressResponseModel> GetAddressByID(int id)
        {
            var fb = await _context.Addresses.SingleOrDefaultAsync(x => x.Id == id);
            if (fb == null)
                throw new Exception("Address is not found");

            return _mapper.Map<AddressResponseModel>(fb);
        }

    }
}
