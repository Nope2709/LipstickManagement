
using DataAccess.Customizes;

using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Customizations
{
    public class CustomizationRepository : ICustomizationRepository
    {
        public readonly CustomizeDAO _customizeDAO;
        public CustomizationRepository(CustomizeDAO customizationDAO)
        {
            _customizeDAO = customizationDAO;
        }
        public async Task<string> CreateCustomization(CreateCustomizeRequestModel cus) => await _customizeDAO.CreateCustomization(cus);
        

        public async Task<string> DeleteCustomization(int id) => await _customizeDAO.DeleteCustomization(id);



        public async Task<CustomizeResponseModel> GetCustomizationByID(int id) => await _customizeDAO.GetCustomizationByID(id);



        public async Task<string> UpdateCustomization(UpdateCustomizeRequestModel cus) => await _customizeDAO.UpdateCustomization(cus);


    }
}
