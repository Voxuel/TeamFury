using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;

namespace TeamFury_API.Services
{
    public class LeaveDaysService : ILeaveDaysService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _manager;


        public LeaveDaysService(AppDbContext context, UserManager<User> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task<LeaveDays> CreateAsync(LeaveDays toCreate)
        {
            var found = _context.LeaveDays.FirstOrDefaultAsync(x => x.Request.RequestID == toCreate.Request.RequestID);
            if (found != null) return null;
            _context.Add(toCreate);
            await _context.SaveChangesAsync();
            return toCreate;
        }

        public async Task<LeaveDays> DeleteAsync(int id)
        {
            var found = await _context.LeaveDays.FindAsync(id);
            if (found == null) return null;
            _context.Remove(found);
            await _context.SaveChangesAsync();
            return found;
        }

        public async Task<IEnumerable<LeaveDays>> GetAll()
        {
            return await _context.LeaveDays.ToListAsync();
        }

        public async Task<LeaveDays> GetByID(int id)
        {
            return await _context.LeaveDays.FindAsync(id);
        }

        public async Task<IEnumerable<RemainingLeaveDaysDTO>> GetLeaveDaysByEmployeeID(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            if (user == null) return null;
            var leavedaysForUser = await _context.LeaveDays.Where(u => u.IdentityUser == user)
                .Include(leaveDays => leaveDays.Request).ToListAsync();
            var allRequestTypes = await _context.RequestTypes.ToListAsync();

            var result = CalculateLeaveDays(leavedaysForUser, allRequestTypes);
            return result;
        }

        public async Task<LeaveDays> UpdateAsync(LeaveDays newUpdate)
        {
            var found = await _context.LeaveDays.FindAsync(newUpdate.ID);
            if (found == null) return null;
            _context.Update(found);
            await _context.SaveChangesAsync();
            return found;
        }

        public async Task<LeaveDays> UpdateLeaveDaysOnAprovedRequest(Request days)
        {
            var daysLeft = await _context.LeaveDays.FirstOrDefaultAsync(x => x.Request.RequestID == days.RequestID);
            int daysOff = Convert.ToInt32((days.EndDate - days.StartDate).TotalDays);
            daysLeft.Days += daysOff;
            _context.Update(daysLeft);
            await _context.SaveChangesAsync();
            return null;
        }

        private static IEnumerable<RemainingLeaveDaysDTO> CalculateLeaveDays
            (List<LeaveDays> leaveDays, List<RequestType> Rts)
        {
            var combined = new Dictionary<string, int?>();
            foreach (var day in leaveDays)
            {
                var daysSelected = Rts.Where(x =>
                    x.RequestTypeID == day.Request.RequestType.RequestTypeID).Select(x => x.MaxDays).Sum();
                if (combined.ContainsKey(day.Request.RequestType.Name))
                {
                    combined[day.Request.RequestType.Name] -= day.Days;
                    continue;
                }
                combined.Add(day.Request.RequestType.Name, daysSelected - day.Days);
            }

            var result = new List<RemainingLeaveDaysDTO>();

            foreach (var (key, value) in combined)
            {
                result.Add(new RemainingLeaveDaysDTO() {DaysLeft = value, LeaveType = key});
            }

            return result;
        }
    }
}