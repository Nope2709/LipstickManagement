using DataAccess.DTO.Customizes;
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
        public Task<string> CreateCustomization(CreateCustomizeRequestModel cus) => CustomizeDAO.Instance.CreateCustomization(cus);
        

        public Task<string> DeleteCustomization(int id) => CustomizeDAO.Instance.DeleteCustomization(id);



        public Task<CustomizeResponseModel> GetCustomizationByID(int id) => CustomizeDAO.Instance.GetCustomizationByID(id);



        public Task<string> UpdateCustomization(UpdateCustomizeRequestModel cus) => CustomizeDAO.Instance.UpdateCustomization(cus);


    }
}
