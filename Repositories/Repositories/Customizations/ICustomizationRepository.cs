using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Customizations
{
    public interface ICustomizationRepository
    {
        Task<string> CreateCustomization(CreateCustomizeRequestModel cus);

        Task<string> UpdateCustomization(UpdateCustomizeRequestModel cus);

        Task<string> DeleteCustomization(int id);



        Task<CustomizeResponseModel> GetCustomizationByID(int id);
        
    }
}
