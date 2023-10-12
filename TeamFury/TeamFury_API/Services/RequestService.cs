using Microsoft.AspNetCore.Identity;
using Models.Models;
using Microsoft.EntityFrameworkCore;


namespace TeamFury_API.Services
{
    public class RequestService : IRequestService
    {
        private readonly UserManager<User> _userManager;


        public RequestService(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }
        public Task<IEnumerable<Request>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Request> UpdateAsync(Request newUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<Request> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Request> CreateAsync(Request toCreate)
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetRequestByEmployeeID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
