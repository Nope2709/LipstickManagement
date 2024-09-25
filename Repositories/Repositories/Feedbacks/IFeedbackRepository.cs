using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Feedbacks
{
    public interface IFeedbackRepository
    {
         Task<string> CreateFeedback(CreateFeedbackRequestModel f);

         Task<string> UpdateFeedback(UpdateFeedbackRequestModel f);

         Task<string> DeleteFeedback(int id);



         Task<FeedbackResponseModel> GetFeedbackByID(int id);
    }
}
