using AutoMapper;
using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
            var lipstickIngredientDTOList = lipStick.LipstickIngredients;
            var checkCategory = await _context.Categories.SingleOrDefaultAsync(c => c.Id == lipStick.CategoryId);
            if (checkCategory == null)
            {
                throw new InvalidDataException("Category is not found");
            }
            var checkExistedCategoryId = await _context.Lipsticks.AnyAsync(c => c.CategoryId == lipStick.CategoryId);
            if (checkExistedCategoryId)
                throw new InvalidDataException("This category belong to another lipstick");
            var newLipStick = new Lipstick()
            {
                Name = lipStick.Name,
                Usage = lipStick.Usage,
                Description = lipStick.Description,
                Details = lipStick.Details,
                Price = lipStick.Price,
                StockQuantity = lipStick.StockQuantity,
                DiscountPercentage = lipStick.DiscountPercentage,
                DiscountPrice = lipStick.DiscountPrice,
                Category = checkCategory,
                CategoryId = lipStick.CategoryId,
                CreatedDate=DateTime.Now,
                ExpiredDate=lipStick.ExpiredDate,
                ImageURLs = new List<ImageURL>(),
                LipstickIngredients = new List<LipstickIngredient>(),
        };
           
            
            foreach (var item in lipStick.imageURLs)
            {
                var image = new ImageURL()
                {
                   
                    URL = item.URL,
                };
                newLipStick.ImageURLs.Add(image);
                
            }
            foreach (var lipstickIngredients in lipstickIngredientDTOList)
            {             
                    var checkIngredient = await _context.Ingredients
                    .SingleOrDefaultAsync(x => x.Id == lipstickIngredients.IngredientId);
                    if (checkIngredient == null)
                {
                  throw new InvalidDataException("Ingredient is not found");
                   
                }
                        
                    
                    var lipStickIngredient = new LipstickIngredient()
                    {
                        Ingredient = checkIngredient,
                        Lipstick = newLipStick,

                    };
                newLipStick.LipstickIngredients.Add(lipStickIngredient);
              
            }
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
            var lipstickIngredientList = lipStick.LipstickIngredients;
            var hotPotEntity = await _context.Lipsticks.Include(l=>l.LipstickIngredients).Include(i=>i.ImageURLs).Include(c=>c.Category).SingleOrDefaultAsync(x => x.Id == lipStick.Id);
            if (hotPotEntity == null)
                throw new InvalidDataException("Lipstick is not found");
            var checkCategory = await _context.Categories.SingleOrDefaultAsync(c => c.Id == lipStick.CategoryId);
            if (checkCategory == null)
            {
                throw new InvalidDataException("Category is not found");
            }
            var checkExistedCategoryId = await _context.Lipsticks.AnyAsync(c=>c.CategoryId== lipStick.CategoryId && c.Id!=lipStick.Id);
            if (checkExistedCategoryId)
                throw new InvalidDataException("This category belong to another lipstick");


            hotPotEntity.Name = lipStick.Name;
            hotPotEntity.Usage = lipStick.Usage;    
            hotPotEntity.Description = lipStick.Description;
            hotPotEntity.Details = lipStick.Details;
            hotPotEntity.Price = lipStick.Price;
            hotPotEntity.StockQuantity = lipStick.StockQuantity;
            hotPotEntity.DiscountPrice = lipStick.DiscountPrice;
            hotPotEntity.DiscountPercentage = lipStick.DiscountPercentage;  
            hotPotEntity.Category = checkCategory;
            hotPotEntity.CategoryId=lipStick.CategoryId;
            hotPotEntity.UpdatedDate=DateTime.Now;
            hotPotEntity.ExpiredDate=lipStick.ExpiredDate;
            //hotPotEntity.ImageURLs = new List<ImageURL>();
            //hotPotEntity.LipstickIngredients = new List<LipstickIngredient>();
           
            foreach (var item in lipStick.imageURLs)
            {
                var image = await _context.ImageURLs.SingleOrDefaultAsync( x => x.Id == item.Id && x.LipstickId == lipStick.Id);
                
                if (image == null)
                    throw new InvalidDataException("Image is not belong to this lipstick");

                    image.LipstickId = lipStick.Id;
                    image.URL = item.URL;
                _context.ImageURLs.Update(image);
                await _context.SaveChangesAsync();


            }

                var existedLipstickIngredientIdInList = hotPotEntity.LipstickIngredients.Select(x => x.IngredientId).ToList();
                var newDishIngredientIdInList = lipstickIngredientList.Select(x => x.IngredientId).ToList();
                var removeLipstickIngredientIdInList = existedLipstickIngredientIdInList.Where(existedDishIngredientId => !newDishIngredientIdInList.Contains(existedDishIngredientId)).ToList();

                foreach ( var lipstickIngredients in lipstickIngredientList)
                {
                    int existedIngredientIdPosition = existedLipstickIngredientIdInList.IndexOf(lipstickIngredients.IngredientId);
                    if(existedIngredientIdPosition == -1)
                    {
                        var checkIngredient = await _context.Ingredients.SingleOrDefaultAsync(x => x.Id == lipstickIngredients.IngredientId);
                        if (checkIngredient==null)
                            throw new InvalidDataException("Ingredient is not found");
                        var lipStickIngredient = new LipstickIngredient()
                        {
                            Ingredient = checkIngredient,
                            Lipstick = hotPotEntity,
                            
                        };
                    hotPotEntity.LipstickIngredients.Add(lipStickIngredient);
                    }
                    else
                    {
                        var existedIngredientId = existedLipstickIngredientIdInList[existedIngredientIdPosition];
                        var checkLipstickIngredient = _context.LipstickIngredients.FirstOrDefault(i => i.Ingredient.Id == existedIngredientId);
                        hotPotEntity.LipstickIngredients.Add(checkLipstickIngredient);
                    }
                    foreach (var removeLíptickIngredientId in removeLipstickIngredientIdInList)
                    {
                        var lipIngredient = _context.LipstickIngredients.FirstOrDefault(i => i.Lipstick.Id == hotPotEntity.Id && i.Ingredient.Id  == removeLíptickIngredientId);

                        hotPotEntity.LipstickIngredients.Remove(lipIngredient);
                    }              
            }
            _context.Lipsticks.Update(hotPotEntity);
            await _context.SaveChangesAsync();
           
            return "Update Lipstick Successfully";
            
            
        }

        public async Task<string> DeleteLipstick(int id)
        {
            var hotPot = await _context.Lipsticks.SingleOrDefaultAsync(x => x.Id == id);
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
            IQueryable<Lipstick> hotPots = _context.Lipsticks
                .Include(f=>f.Feedbacks).Include(i=>i.ImageURLs)
                .Include(c=>c.Category)
                .Include(l=>l.LipstickIngredients).ThenInclude(ig=>ig.Ingredient).Where(x => x.Id != null);

            //TÌM THEO TÊN
            if (!string.IsNullOrEmpty(search))
            {
                hotPots = hotPots.Where(x => x.Name.Contains(search)    );
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
            var hotpot = await _context.Lipsticks.Include(f=>f.Feedbacks).Include(i => i.ImageURLs).Include(c=>c.Category).SingleOrDefaultAsync(x => x.Id == id);
            if (hotpot == null)
                throw new Exception("Lipstick is not found");

            return _mapper.Map<LipstickResponseModel>(hotpot);
        }
        public Task<bool> lipstickExists(int id)
        {
            return _context.Lipsticks.AnyAsync(l => l.Id == id);
        }
    }
}
