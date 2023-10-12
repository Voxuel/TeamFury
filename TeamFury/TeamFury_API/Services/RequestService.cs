using Microsoft.AspNetCore.Identity;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using TeamFury_API.Data;

namespace TeamFury_API.Services
{
    public class RequestService : IRequestService
    {

        private readonly AppDbContext _context;


        public RequestService(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Request>> GetAll()
        {
            return await _context.Requests.ToListAsync();
        }

        public async Task<Request> GetByID(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<Request> UpdateAsync(Request newUpdate)
        {
            var found = await _context.Requests.FindAsync(newUpdate.RequestID);
            if (found == null) return null;
            _context.Update(newUpdate);
            _context.SaveChanges();
            return found;
        }

        public async Task<Request> DeleteAsync(int id)
        {
            var found = await _context.Requests.FindAsync(id);
            if (found == null) return null;
            _context.Remove(found);
            _context.SaveChanges();
            return found;
        }

        public async Task<Request> CreateAsync(Request toCreate)
        {


            throw new NotImplementedException();
        }

        public Task<Request> GetRequestByEmployeeID(string id)
        {
            throw new NotImplementedException();
        }
    }
}
