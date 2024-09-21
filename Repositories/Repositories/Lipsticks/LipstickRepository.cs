using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using DataAccess.Lipsticks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Lipsticks
{
    public class LipstickRepository : ILipstickRepositories
    {
        public readonly LipstickDAO _lipstickDAO;
        public LipstickRepository(LipstickDAO lipstickDAO)
        {
            _lipstickDAO = lipstickDAO;
        }

            public async Task<string> CreateLipstick(CreateLipstickRequestModel lipStick) => await _lipstickDAO.CreateLipstick(lipStick);

        public async Task<string> DeleteLipstick(int id) => await _lipstickDAO.DeleteLipstick(id);

        public async Task<List<LipstickResponseModel>> GetLipstick(string? search, string? sortBy, decimal? fromPrice, decimal? toPrice, int? flavorID, string? size, string? type, int pageIndex, int pageSize) => await _lipstickDAO.GetLipsticks(search, sortBy, fromPrice, toPrice, flavorID, size, type, pageIndex, pageSize);
        

        public async Task<LipstickResponseModel> GetLipstickByID(int id) => await _lipstickDAO.GetLipstickByID(id);
        

        public async Task<string> UpdateLipstick(UpdateLipstickRequestModel lipStick) => await _lipstickDAO.UpdateLipstick(lipStick);

    }
}
