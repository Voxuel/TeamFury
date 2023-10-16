﻿using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services
{
    public interface ILeaveDaysService: ICRUDService<LeaveDays>
    {
        Task<IEnumerable<RemainingLeaveDaysDTO>> GetLeaveDaysByEmployeeID(string id);
        Task<LeaveDays> UpdateLeaveDaysOnAprovedRequest(Request days);

    }
}