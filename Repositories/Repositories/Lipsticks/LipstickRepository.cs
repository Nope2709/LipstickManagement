using BussinessObject;

using DataAccess.DTO.Lipsticks;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Lipsticks
{
    public class LipstickRepository : ILipstickRepositories
    {
        public Task<string> CreateLipstick(CreateLipstickRequestModel lipStick) => LipstickDAO.Instance.CreateLipstick(lipStick);

        public Task<string> DeleteLipstick(int id) => LipstickDAO.Instance.DeleteLipstick(id);

        public Task<List<LipstickResponseModel>> GetLipstick(string? search, string? sortBy, decimal? fromPrice, decimal? toPrice, int? flavorID, string? size, string? type, int pageIndex, int pageSize) => LipstickDAO.Instance.GetLipsticks(search, sortBy, fromPrice, toPrice, flavorID, size, type, pageIndex, pageSize);
        

        public Task<LipstickResponseModel> GetLipstickByID(int id) => LipstickDAO.Instance.GetLipstickByID(id);
        

        public Task<string> UpdateLipstick(UpdateLipstickRequestModel lipStick) => LipstickDAO.Instance.UpdateLipstick(lipStick);

    }
}
