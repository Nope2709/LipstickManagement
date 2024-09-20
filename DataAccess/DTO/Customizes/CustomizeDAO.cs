using AutoMapper;
using BussinessObject;
using DataAccess.DTO.Lipsticks;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Customizes
{
    public class CustomizeDAO
    {
        private static CustomizeDAO instance;
        private static object instanceLock = new object();
        private readonly LipstickManagementContext _context = new();
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CustomizeDAO(LipstickManagementContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public CustomizeDAO()
        {
        }

        public static CustomizeDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomizeDAO();
                    }
                }
                return instance;
            }
        }



        public async Task<string> CreateCustomization(CreateCustomizeRequestModel cus)
        {


            var newCus = new Customization()
            {
                LipstickId = cus.LipstickId,
                EngravingText = cus.EngravingText,
                QrCodeUrl = cus.QrCodeUrl,
                
            };

            _context.Customizations.Add(newCus);
            try
            {
                await _context.SaveChangesAsync();
                return "Create Customization Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> UpdateCustomization(UpdateCustomizeRequestModel cus)
        {
            var hotPotEntity = await _context.Customizations.SingleOrDefaultAsync(x => x.CustomizationId == cus.CustomizationId);
            if (hotPotEntity == null)
                throw new InvalidDataException("Lipstick is not found");



            hotPotEntity.LipstickId = cus.LipstickId;
            hotPotEntity.EngravingText = cus.EngravingText;
            hotPotEntity.QrCodeUrl = cus.QrCodeUrl;
          

            _context.Customizations.Update(hotPotEntity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Customize Successfully";
            else
                return "Update Customize Failed";
        }

        public async Task<string> DeleteCustomization(int id)
        {
            var hotPot = await _context.Customizations.SingleOrDefaultAsync(x => x.CustomizationId == id);
            if (hotPot == null)
                throw new InvalidDataException("Customization is not found");



            _context.Customizations.Remove(hotPot);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Customization Successfully";
            else
                return "Delete Customization Failed";
        }

        //public async Task<List<CustomizeResponseModel>> GetCustomizations(string? search, string? sortBy,
        //    decimal? fromPrice, decimal? toPrice,
        //    int? flavorID, string? size, string? type,
        //    int pageIndex, int pageSize)
        //{
        //    IQueryable<Customization> hotPots = _context.Customizations.Where(x => x.CustomizationId != null);

        //    //TÌM THEO TÊN
        //    if (!string.IsNullOrEmpty(search))
        //    {
        //        hotPots = hotPots.Where(x => x.ShadeName.Contains(search) || x.Type.Contains(search));
        //    }


        //    // FILTER THEO GIÁ
        //    if (fromPrice.HasValue)
        //    {
        //        hotPots = hotPots.Where(x => x.Price >= fromPrice.Value);
        //    }

        //    if (toPrice.HasValue)
        //    {
        //        hotPots = hotPots.Where(x => x.Price <= toPrice.Value);
        //    }

        //    //SORT THEO TÊN
        //    if (!string.IsNullOrEmpty(sortBy))
        //    {
        //        if (sortBy.Equals("ascName"))
        //        {
        //            hotPots = hotPots.OrderBy(x => x.ShadeName);
        //        }
        //        else if (sortBy.Equals("descName"))
        //        {
        //            hotPots = hotPots.OrderByDescending(x => x.ShadeName);
        //        }
        //    }

        //    //SORT THEO GIÁ
        //    if (!string.IsNullOrEmpty(sortBy))
        //    {
        //        if (sortBy.Equals("ascPrice"))
        //        {
        //            hotPots = hotPots.OrderBy(x => x.Price);
        //        }
        //        else if (sortBy.Equals("descPrice"))
        //        {
        //            hotPots = hotPots.OrderByDescending(x => x.Price);
        //        }
        //    }

        //    var paginatedUsers = PaginatedList<Lipstick>.Create(hotPots, pageIndex, pageSize);

        //    return _mapper.Map<List<LipstickResponseModel>>(paginatedUsers);
        //}

        public async Task<CustomizeResponseModel> GetCustomizationByID(int id)
        {
            var hotpot = await _context.Customizations.SingleOrDefaultAsync(x => x.CustomizationId == id);
            if (hotpot == null)
                throw new Exception("Customization is not found");

            return _mapper.Map<CustomizeResponseModel>(hotpot);
        }
    }
}
