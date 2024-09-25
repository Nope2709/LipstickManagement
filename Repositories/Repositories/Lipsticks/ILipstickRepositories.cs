using BussinessObject;
using DataAccess;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Lipsticks
{
    public interface ILipstickRepositories
    {
        Task<string> CreateLipstick(CreateLipstickRequestModel lipStick);

        Task<string> UpdateLipstick(UpdateLipstickRequestModel lipStick);

         Task<string> DeleteLipstick(int id);

        Task<List<LipstickResponseModel>> GetLipstick(string? search, string? sortBy,
            decimal? fromPrice, decimal? toPrice,
            int? flavorID, string? size, string? type,
            int pageIndex, int pageSize);

        Task<LipstickResponseModel> GetLipstickByID(int id);
        Task<bool> lipstickExists(int id);
    }
}
