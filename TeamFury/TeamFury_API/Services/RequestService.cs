using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using TeamFury_API.Data;

namespace TeamFury_API.Services
{
    public class RequestService : IRequestService
    {

        private readonly AppDbContext _context;
        private readonly UserManager<User> _manager;


        public RequestService(AppDbContext context, UserManager<User> manager)
        {
            _context = context;
            _manager = manager;
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

            if (found.StatusRequest == StatusRequest.Accepted && newUpdate.StatusRequest != StatusRequest.Accepted)
            {
                return null;
            }
            found.StatusRequest = newUpdate.StatusRequest;
            found.MessageForDecline = newUpdate.MessageForDecline;
            _context.Update(found);

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

            if (VerifyRequestTimeLimit(toCreate, daysCheck, usedDays, out var failedDaysCheck))
                return failedDaysCheck;
            
            if (found != null) return null;
            
            _context.Add(toCreate);
            await _context.SaveChangesAsync();
            
            await AddConnectionRequestEmployee(toCreate, id);
            
            return toCreate;
        }

        private async Task AddConnectionRequestEmployee(Request toCreate, string id)
        {
            var x = await _context.Requests.FirstOrDefaultAsync(r => r.RequestSent == toCreate.RequestSent);
            var z = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
            LeaveDays newLeave = new LeaveDays();
            newLeave.IdentityUser = z;
            newLeave.Days = 0;
            newLeave.Request = x;
            _context.Add(newLeave);
            await _context.SaveChangesAsync();
        }

        private static bool VerifyRequestTimeLimit
            (Request toCreate, RequestType daysCheck, List<int> usedDays, out Request failedDaysCheck)
        {
            var time = (int)toCreate.EndDate.Subtract(toCreate.StartDate).TotalDays;
            if (daysCheck.MaxDays - (time) + usedDays.Sum() < 0)
            {
                var req = new Request
                {
                    MessageForDecline = "Too many requested days."
                };
                {
                    failedDaysCheck = req;
                    return true;
                }
            }

            failedDaysCheck = new Request();
            return false;
        }

        public async Task<RequestType> GetRequestTypeID(int id)
        {
            return await _context.RequestTypes.FindAsync(id);
        }

        public async Task<IEnumerable<Request>> GetRequestsByEmployeeID(string id)
        {
            return await _context.LeaveDays.Where(x =>
                x.IdentityUser.Id == id).Select(x => x.Request).ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetAllLogs(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            
            var requests = await _context.LeaveDays.Where(l =>
                l.IdentityUser == user).Include(rt =>
                rt.Request.RequestType).Select(r => r.Request).ToListAsync();
            
            var logs = await _context.RequestLogs.Include(requestLog =>
                requestLog.Request).ToListAsync();

            var RQLs = logs.Where(x =>
                (requests.Any(y => y.RequestID == x.Request.RequestID)));

            var result = RQLs.Select(r => r.Request);

            return result;
        }
        
        public async Task<RequestLog> AddRequestToLog(Request request)
        {
            var log = new RequestLog() {Request = request};
            var found = await _context.RequestLogs.FirstOrDefaultAsync(x =>
            x.Request.RequestID == log.Request.RequestID);
            if (found != null)
            {
                _context.RequestLogs.Update(found);
                await _context.SaveChangesAsync();
                return log;
            }
            _context.RequestLogs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        #region Overidden Methods
        public Task<Request> CreateAsync(Request toCreate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
