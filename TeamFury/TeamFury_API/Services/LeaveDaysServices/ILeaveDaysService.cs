using Models.DTOs;
using Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TeamFury_API.Services
{
    public interface ILeaveDaysService: ICRUDService<LeaveDays>
    {
        Task<IEnumerable<RemainingLeaveDaysDTO>> GetLeaveDaysByEmployeeID(string id);
        Task<IEnumerable<RemainingLeaveDaysDTO>> GetLeaveDaysUsed();
        Task<LeaveDays> UpdateLeaveDaysOnAprovedRequest(RequestUpdateDTO comparison, Request toUpdate);
        Task<LeaveDays> FindByRequest(Request request);


    }
}
