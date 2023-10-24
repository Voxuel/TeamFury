using AutoMapper;
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
        private readonly IMapper _mapper;

        public LeaveDaysService(AppDbContext context, UserManager<User> manager, IMapper mapper)
        {
            _context = context;
            _manager = manager;
            _mapper = mapper;
        }

        public async Task<LeaveDays> CreateAsync(LeaveDays toCreate)
        {
            var found = await _context.LeaveDays.FirstOrDefaultAsync(x => x.Request.RequestID == toCreate.Request.RequestID);
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
        public async Task<LeaveDays> FindByRequest(Request request)
        {
            return await _context.LeaveDays.Include(y => y.IdentityUser)
                .Include(z => z.Request.RequestType).FirstOrDefaultAsync(x => x.Request == request);
        }


        public async Task<LeaveDays> UpdateAsync(LeaveDays newUpdate)
        {
            var found = await _context.LeaveDays.FindAsync(newUpdate.ID);
            if (found == null) return null;
            _context.Update(found);
            await _context.SaveChangesAsync();
            return found;
        }

        public async Task<LeaveDays> UpdateLeaveDaysOnAprovedRequest(RequestUpdateDTO comparison , Request toUpdate)
        {

            var daysLeft = await _context.LeaveDays.FirstOrDefaultAsync(x => x.Request.RequestID == toUpdate.RequestID);
            if (toUpdate.StatusRequest != StatusRequest.Accepted && comparison.StatusRequest == StatusRequest.Accepted)
            {
                var daysOff = Convert.ToInt32((toUpdate.EndDate - toUpdate.StartDate).TotalDays);
                daysLeft.Days += daysOff;
            }
            else if (toUpdate.StatusRequest != StatusRequest.Declined && comparison.StatusRequest == StatusRequest.Declined)
            {
                daysLeft.Days = 0;
            }
            else return null;
            _context.Update(daysLeft);
            await _context.SaveChangesAsync();
            return null;
        }
        #region LeavedaysUsedByEmployee
        public async Task<IEnumerable<RemainingLeaveDaysDTO>> GetLeaveDaysByEmployeeID(string id)
        {
            var user = await _manager.FindByIdAsync(id);
            if (user == null) return null;
            var leavedaysForUser = await _context.LeaveDays.Where(u => u.IdentityUser == user)
                .Include(leaveDays => leaveDays.Request).ToListAsync();
            var allRequestTypes = await _context.RequestTypes.ToListAsync();

            var result = CalculateLeaveDays(leavedaysForUser, allRequestTypes, _mapper);
            return result;
        }

        private static IEnumerable<RemainingLeaveDaysDTO> CalculateLeaveDays
            (List<LeaveDays> leaveDays, List<RequestType> Rts, IMapper mapper)
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

            var result = mapper.Map<List<RemainingLeaveDaysDTO>>(Rts);

            foreach (var (key, value) in combined)
            {
                RemainingLeaveDaysDTO found = result.FirstOrDefault(x => x.LeaveType == key);
                if (found != null) result.Remove(found);

                result.Add(new RemainingLeaveDaysDTO() { DaysLeft = value, LeaveType = key });
            }

            return result;
        }
        #endregion

        #region GetTotalLeavedaysUsed 
        public async Task<IEnumerable<RemainingLeaveDaysDTO>> GetLeaveDaysUsed()
        {
            var leavedaysForAllUsers = await _context.LeaveDays.Include(leaveDays => leaveDays.Request).ToListAsync();
            var allRequestTypes = await _context.RequestTypes.ToListAsync();

            var result = CalculateLeaveDaysUsed(leavedaysForAllUsers, allRequestTypes, _mapper);
            return result;
        }
        private static IEnumerable<RemainingLeaveDaysDTO> CalculateLeaveDaysUsed
         (List<LeaveDays> leaveDays, List<RequestType> Rts, IMapper mapper)
        {
            var combined = new Dictionary<string, int?>();
            foreach (var day in leaveDays)
            {
                if (combined.ContainsKey(day.Request.RequestType.Name))
                {
                    combined[day.Request.RequestType.Name] += day.Days;
                    continue;
                }
                combined.Add(day.Request.RequestType.Name, day.Days);
            }

            var result = mapper.Map<List<RemainingLeaveDaysDTO>>(Rts);
            foreach (var res in result)
            {
                res.DaysLeft = 0;
            }

            foreach (var (key, value) in combined)
            {
                RemainingLeaveDaysDTO found = result.FirstOrDefault(x => x.LeaveType == key);
                if (found != null) result.Remove(found);

                result.Add(new RemainingLeaveDaysDTO() { DaysLeft = value, LeaveType = key });
            }

            return result;
        }


        #endregion
    }
}