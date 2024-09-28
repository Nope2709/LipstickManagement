﻿using AutoMapper;
using BussinessObject;
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

namespace DataAccess.Lipsticks
{
    public class LipstickDAO
    {
        private static LipstickDAO instance;
        //private static object instanceLock = new object();
        private readonly LipstickManagementContext _context = new();
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public LipstickDAO(LipstickManagementContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

       



        public async Task<string> CreateLipstick(CreateLipstickRequestModel lipStick)
        {

            
            var newLipStick = new Lipstick()
            {
                Name = lipStick.Name,
                Usage = lipStick.Usage, 
                Type = lipStick.Type,
                Description = lipStick.Description,
                Price = lipStick.Price,
                StockQuantity = lipStick.StockQuantity,
                ImageURLs = lipStick.imageURLs,
            };

            _context.Lipsticks.Add(newLipStick);
            try
            {
                await _context.SaveChangesAsync();
                return "Create Lipstick Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> UpdateLipstick(UpdateLipstickRequestModel lipStick)
        {
            var hotPotEntity = await _context.Lipsticks.SingleOrDefaultAsync(x => x.LipstickId == lipStick.LipstickId);
            if (hotPotEntity == null)
                throw new InvalidDataException("Lipstick is not found");



            hotPotEntity.Name = lipStick.Name;
            hotPotEntity.Usage = lipStick.Usage;    
            hotPotEntity.Type = lipStick.Type;
            hotPotEntity.Description = lipStick.Description;
            hotPotEntity.Price = lipStick.Price;
            hotPotEntity.StockQuantity = lipStick.StockQuantity;
            hotPotEntity.ImageURLs = lipStick.imageURLs;


            _context.Lipsticks.Update(hotPotEntity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Lipstick Successfully";
            else
                return "Update Lipstick Failed";
        }

        public async Task<string> DeleteLipstick(int id)
        {
            var hotPot = await _context.Lipsticks.SingleOrDefaultAsync(x => x.LipstickId == id);
            if (hotPot == null)
                throw new InvalidDataException("Lipstick is not found");



            _context.Lipsticks.Remove(hotPot);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Lipstick Successfully";
            else
                return "Delete Lipstick Failed";
        }

        public async Task<List<LipstickResponseModel>> GetLipsticks(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, string? type,
            int pageIndex, int pageSize)
        {
            IQueryable<Lipstick> hotPots = _context.Lipsticks.Include(f=>f.Feedbacks).Include(i=>i.ImageURLs).Where(x => x.LipstickId != null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(search))
            {
                hotPots = hotPots.Where(x => x.Name.Contains(search) || x.Type.Contains(search));
            }


            // FILTER THEO GIÁ
            if (fromPrice.HasValue)
            {
                hotPots = hotPots.Where(x => x.Price >= fromPrice.Value);
            }

            if (toPrice.HasValue)
            {
                hotPots = hotPots.Where(x => x.Price <= toPrice.Value);
            }

            //SORT THEO TÊN
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascName"))
                {
                    hotPots = hotPots.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("descName"))
                {
                    hotPots = hotPots.OrderByDescending(x => x.Name);
                }
            }

            //SORT THEO GIÁ
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascPrice"))
                {
                    hotPots = hotPots.OrderBy(x => x.Price);
                }
                else if (sortBy.Equals("descPrice"))
                {
                    hotPots = hotPots.OrderByDescending(x => x.Price);
                }
            }

            var paginatedUsers = PaginatedList<Lipstick>.Create(hotPots, pageIndex, pageSize);

            return _mapper.Map<List<LipstickResponseModel>>(paginatedUsers);
        }

        public async Task<LipstickResponseModel> GetLipstickByID(int id)
        {
            var hotpot = await _context.Lipsticks.Include(f=>f.Feedbacks).Include(i => i.ImageURLs).SingleOrDefaultAsync(x => x.LipstickId == id);
            if (hotpot == null)
                throw new Exception("Lipstick is not found");

            return _mapper.Map<LipstickResponseModel>(hotpot);
        }
        public Task<bool> lipstickExists(int id)
        {
            return _context.Lipsticks.AnyAsync(l => l.LipstickId == id);
        }
    }
}
