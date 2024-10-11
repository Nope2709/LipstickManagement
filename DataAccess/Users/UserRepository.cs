using AutoMapper;
using BussinessObject;
using DataAccess;
using LipstickManagementAPI.DTO.RequestModel;
using LipstickManagementAPI.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using Repository.Service.Paging;
using Service.CurrentUser;
using Service.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly LipstickManagementContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPasswordService _passwordService;

        public UserRepository(LipstickManagementContext context, IMapper mapper, ICurrentUserService currentUserService, IPasswordService passwordService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _passwordService = passwordService;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel loginRequest)
        {
            var user = await _context.Accounts.SingleOrDefaultAsync(x => x.Phone == loginRequest.Phone
            );
            if (user == null)
            {
                throw new InvalidDataException($"Phone is not found - {loginRequest.Phone}");
            }

            if (user != null)
            {
                var chucvu = await _context.Roles.SingleOrDefaultAsync(x => x.Id == user.RoleId);
                if (chucvu == null)
                {
                    throw new Exception($"Role is not found - {loginRequest.Phone}");
                }

                var samePassword = _passwordService.VerifyPassword(loginRequest.Password, user.Password);
                
                if (samePassword)
                {
                    return _mapper.Map<LoginResponseModel>(user);
                    
                }
                
            }

            throw new Exception("Wrong Phone Or Password");
        }
        public async Task<string> CreateUser(CreateUserRequestModel user)
        {
            var checkEmail = await _context.Accounts.AnyAsync(x => x.Email == user.Email);
            if (checkEmail)
                throw new InvalidDataException("Email is existing");
            var checkPhone = await _context.Accounts.AnyAsync(x => x.Phone == user.Phone);
            if (checkPhone)
                throw new InvalidDataException("Phone is existing");
            //var checkAddress = await _context.Addresses.AnyAsync(a=>a.Id)
            var role = await _context.Roles.SingleOrDefaultAsync(x => x.RoleName.Equals("Guest"));
            if (role == null)
                throw new InvalidDataException("Guest is not found");
            
            if (user.ConfirmPassword != user.Password)
                throw new InvalidDataException("Password not match");
            

           
            var newUser = new Account()
            {
                Name = user.Name,
                Email = user.Email,
                Password = _passwordService.HashPassword(user.Password),
                Gender = user.Gender,
                Phone = user.Phone,
                IsEnabled = true,
                RoleId = 3,
                //CreateByID = _currentUserService.UserId,
                CreatedDate = DateTime.Now
            };
            
            _context.Accounts.Add(newUser);
            if (await _context.SaveChangesAsync() > 0)
                return "Register Successfully";
            else
                return "Register Failed";
        }
        public async Task<List<UserResponseModel>> GetUsers(string? search, string? gender, string? sortBy, int pageIndex, int pageSize)
        {
            //NHỚ CHECK DELETEDATE
            IQueryable<Account> users = _context.Accounts.Include(x => x.Role).Include(a=>a.Addresses);


            //SEARCH THEO NAME
            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(x => x.Name.Contains(search));
            }

            //FILTER THEO GENDER
            if (!string.IsNullOrEmpty(gender))
            {
                users = users.Where(x => x.Equals(gender));
            }

            //SORT THEO NAME
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("asc"))
                {
                    users = users.OrderBy(x => x.Name);
                }
                else if (sortBy.Equals("desc"))
                {
                    users = users.OrderByDescending(x => x.Name);
                }
            }

            var paginatedUsers = PaginatedList<Account>.Create(users, pageIndex, pageSize);

            return _mapper.Map<List<UserResponseModel>>(paginatedUsers);
        }
        public async Task<UserResponseModel> GetUserByEmail(string email)
        {
            var user = await _context.Accounts.Include(r=>r.Role).SingleOrDefaultAsync(x => x.Email == email);
            if (user == null)
                throw new Exception("User is not found");

            return _mapper.Map<UserResponseModel>(user);
        }
        public async Task<string> UpdateUser(UpdateUserRequestModel user)
        {
            
            var checkUser = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == user.Id);
            if (checkUser == null)
                throw new InvalidDataException("User is not found");

            var checkEmail = await _context.Accounts.AnyAsync(x => x.Email == user.Email && x.Id != user.Id);
            if (checkEmail)
                throw new InvalidDataException("Email is existing");

            var checkPhone = await _context.Accounts.AnyAsync(x => x.Phone == user.Phone && x.Id != user.Id);
            if (checkPhone)
                throw new InvalidDataException("Phone is existing");

            checkUser.Name = user.Name;
            checkUser.Email = user.Email;
            checkUser.Gender = user.Gender;
            checkUser.Phone = user.Phone;
            checkUser.IsEnabled = user.IsEnabled;
            checkUser.RoleId = user.RoleID;
            checkUser.UpdatedDate = DateTime.Now;   



            _context.Accounts.Update(checkUser);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Successfully";
            else
                return "Update Failed";

        }
        public async Task<string> ChangePasswordUser(ChangePasswordRequestModel user)
        {
            
            var checkPassword = await _context.Accounts.SingleOrDefaultAsync(x => x.Phone == user.Phone);
            if (checkPassword == null)
                throw new InvalidDataException("Phone not found");

            if (user.ConfirmPassword != user.NewPassword)
                throw new InvalidDataException("Password not match");
           
            checkPassword.Password = _passwordService.HashPassword(user.NewPassword);
            _context.Accounts.Update(checkPassword);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Password Successfully";
            else
                return "Update Password Failed";



        }

        public async Task<UserResponseModel> GetUserByID(int id)
        {
            var user = await _context.Accounts.Include(r => r.Role).Include(a=> a.Addresses).SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
                throw new Exception("User is not found");

            return _mapper.Map<UserResponseModel>(user);
        }
        public async Task<UserResponseModel> GetUserByPhone(string phone)
        {
            var user = await _context.Accounts.Include(r => r.Role).Include(a => a.Addresses).SingleOrDefaultAsync(x => x.Phone.Equals(phone));
            if (user == null)
                throw new Exception("User is not found");

            return _mapper.Map<UserResponseModel>(user);
        }

        public Task<string> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
        //public async Task<string> UpdateUserProfile(UpdateUserProfile user)
        //{

        //    var checkUser = await _context.Accounts.SingleOrDefaultAsync(x => x.Id == user.Id);
        //    if (checkUser == null)
        //        throw new InvalidDataException("User is not found");

        //    var checkEmail = await _context.Accounts.AnyAsync(x => x.Email == user.Email && x.Id != user.Id);
        //    if (checkEmail)
        //        throw new InvalidDataException("Email is existing");

        //    var checkPhone = await _context.Accounts.AnyAsync(x => x.Phone == user.Phone && x.Id != user.Id);
        //    if (checkPhone)
        //        throw new InvalidDataException("Phone is existing");

        //    checkUser.Name = user.Name;
        //    checkUser.Email = user.Email;
        //    checkUser.Gender = user.Gender;
        //    checkUser.Phone = user.Phone;
        //    checkUser.IsEnabled = user.IsEnabled;
        //    checkUser.RoleId = user.RoleID;
        //    checkUser.UpdatedDate = DateTime.Now;



        //    _context.Accounts.Update(checkUser);
        //    if (await _context.SaveChangesAsync() > 0)
        //        return "Update Successfully";
        //    else
        //        return "Update Failed";

        //}
    }
}
