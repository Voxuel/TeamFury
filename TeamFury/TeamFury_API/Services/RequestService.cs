using Microsoft.EntityFrameworkCore;
using Models.Models;
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
            // if (newUpdate.StatusRequest == StatusRequest.Accepted && found.StatusRequest != StatusRequest.Accepted)
            // {
            //     LeaveDaysService leaveDays = new();
            //     await leaveDays.UpdateLeaveDaysOnAprovedRequest(newUpdate);
            // }
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

            //Finds the number of leave days used by the employee on requested type
            var usedDays = await _context.LeaveDays.Where(x => x.IdentityUser.Id == id
            && x.Request.RequestType.RequestTypeID == toCreate.RequestType.RequestTypeID)
                .Select(y => y.Days).ToListAsync();

            //Finds maximum allowed days of for the requested type
            var daysCheck = await _context.RequestTypes.FirstOrDefaultAsync(y =>
            y.RequestTypeID == toCreate.RequestType.RequestTypeID);

            if (daysCheck.MaxDays - (Convert.ToInt32((toCreate.EndDate - toCreate.StartDate).TotalDays) + usedDays.Sum()) < 0)
            {
                Request req = new Request();
                req.MessageForDecline = "Too many requested days.";
                return req;
            };
            if (found != null) return null;
            _context.Add(toCreate);
            
            await _context.SaveChangesAsync();
            var x = await _context.Requests.FirstOrDefaultAsync(r => r.RequestSent == toCreate.RequestSent);
            var z = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
            LeaveDays newLeave = new LeaveDays();
            newLeave.IdentityUser = z;
            newLeave.Days = 0;
            newLeave.Request = x;
            _context.Add(newLeave);

            await _context.SaveChangesAsync();
            return toCreate;
        }

        public async Task<RequestType> GetRequestTypeID(int id)
        {
            return await _context.RequestTypes.FindAsync(id);
        }

        public async Task<IEnumerable<Request>> GetRequestsByEmployeeID(string id)
        {
            return await _context.LeaveDays.Where(x => x.IdentityUser.Id == id).Select(x => x.Request).ToListAsync();
        }

        #region Overidden Methods
        public Task<Request> CreateAsync(Request toCreate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
