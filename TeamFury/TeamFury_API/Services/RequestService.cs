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
            await _context.SaveChangesAsync();
            return found;
        }

        public async Task<Request> DeleteAsync(int id)
        {
            var found = await _context.Requests.FindAsync(id);
            if (found == null) return null;
            _context.Remove(found);
            await _context.SaveChangesAsync();
            return found;
        }

        public async Task<Request> CreateAsync(Request toCreate, string id)
        {
            var found = await _context.LeaveDays.FirstOrDefaultAsync(x => x.IdentityUser.Id == id &&
            x.Request.StartDate == toCreate.StartDate && x.Request.EndDate == toCreate.EndDate);
            if (found != null) return null;
            _context.Add(toCreate);
            await _context.SaveChangesAsync();
            return toCreate;
        }

        public async Task<IEnumerable<Request>> GetRequestsByEmployeeID(string id)
        {
            return await _context.LeaveDays.Where(x=>x.IdentityUser.Id == id).Select(x=> x.Request).ToListAsync();
        }

        #region Overidden Methods
        public Task<Request> CreateAsync(Request toCreate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
