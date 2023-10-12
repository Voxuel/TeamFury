using Microsoft.AspNetCore.Identity;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using TeamFury_API.Data;


namespace TeamFury_API.Services
{
    public class RequestService : IRequestService
    {

        private readonly AppDbContext _context;
        
        private readonly UserManager<User> _userManager;


        public RequestService(UserManager<User> userManager, AppDbContext context) 
        {
            _userManager = userManager;
            _context = context;
        }
        public Task<IEnumerable<Request>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Request> UpdateAsync(Request newUpdate)
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
