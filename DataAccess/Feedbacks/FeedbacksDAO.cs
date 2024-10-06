using AutoMapper;
using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using DataAccess.Lipsticks;
using Microsoft.EntityFrameworkCore;
using Repository.Service.Paging;
using Service.CurrentUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Feedbacks
{
    public class FeedbacksDAO
    {
        
        //private static object instanceLock = new object();
        private readonly LipstickManagementContext _context = new();
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public FeedbacksDAO(LipstickManagementContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }



        public async Task<string> CreateFeedback(CreateFeedbackRequestModel f)
        {

            
            var newFB = new Feedback()
            {
                AccountId = f.AccountId,    
                Content = f.Content,
                LipstickId = f.LipstickId,
            };

            _context.Feedbacks.Add(newFB);
            try
            {
                await _context.SaveChangesAsync();
                return "Create Feedback Successfully";
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task<string> UpdateFeedback(UpdateFeedbackRequestModel f)
        {
            var fb = await _context.Feedbacks.SingleOrDefaultAsync(x => x.Id == f.Id);
            if (fb == null)
                throw new InvalidDataException("Feedback is not found");
            

            fb.Content = f.Content;

            


            _context.Feedbacks.Update(fb);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Feedback Successfully";
            else
                return "Update Feedback Failed";
        }

        public async Task<string> DeleteFeedback(int id)
        {
            var fb = await _context.Feedbacks.SingleOrDefaultAsync(x => x.Id == id);
            if (fb == null)
                throw new InvalidDataException("Feedback is not found");



            _context.Feedbacks.Remove(fb);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Feedback Successfully";
            else
                return "Delete Feedback Failed";
        }

        

        public async Task<FeedbackResponseModel> GetFeedbackByID(int id)
        {
            var fb = await _context.Feedbacks.SingleOrDefaultAsync(x => x.Id == id);
            if (fb == null)
                throw new Exception("Feedback is not found");

            return _mapper.Map<FeedbackResponseModel>(fb);
        }
        

    }
}
