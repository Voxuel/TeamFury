using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;

namespace TeamFury_API.Services
{
    public class LeaveDaysService : ILeaveDaysService
    {
        private readonly AppDbContext _context;


        public LeaveDaysService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<LeaveDays> CreateAsync(LeaveDays toCreate)
        {
            var found = _context.LeaveDays.FirstOrDefaultAsync(x=>x.Request.RequestID == toCreate.Request.RequestID);
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
            var daysUsed = await _context.LeaveDays.Where(x => x.IdentityUser.Id == id).ToListAsync();
            var maxDays = await _context.RequestTypes.ToListAsync();
            var daysLeft = maxDays; 
            foreach (var day in daysUsed)
            {
               maxDays.Where(x => x.RequestTypeID == day.Request.RequestType.RequestTypeID).Select(x => x.MaxDays - day.Days);
            }

            return null;
            throw new NotImplementedException();
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
    }
}
