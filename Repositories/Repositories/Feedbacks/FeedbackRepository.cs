using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using DataAccess.Feedbacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Feedbacks
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public readonly FeedbacksDAO _feedbacksDAO;
        public FeedbackRepository(FeedbacksDAO feedbacksDAO) { 
            _feedbacksDAO = feedbacksDAO;   
        }
        public async Task<string> CreateFeedback(CreateFeedbackRequestModel f) => await _feedbacksDAO.CreateFeedback(f);


        public async Task<string> DeleteFeedback(int id) => await _feedbacksDAO.DeleteFeedback(id);
        

        public async Task<FeedbackResponseModel> GetFeedbackByID(int id) => await _feedbacksDAO.GetFeedbackByID(id);    
        

        public async Task<string> UpdateFeedback(UpdateFeedbackRequestModel f) => await _feedbacksDAO.UpdateFeedback(f);  
        
    }
}
