using AutoMapper;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.AdminServices;

public interface IAdminService : ICRUDService<User>
{
    Task<IEnumerable<UserViewDTO>> GetAllViewModels();
    Task<User> GetByIdAsync(string id);
    Task<User> UpdateAsync(User newUpdate, string password);
    Task<User> DeleteAsync(string id);
    Task<User> CreateAsync(User user, string role);

    Task<RequestType> CreateRequestTypeAsync(RequestType requestType);
    Task<RequestType> UpdateRequestTypeAsync(RequestType requestType);
    Task<RequestType> DeleteRequestTypeAsync(int id);
    Task<IEnumerable<RequestWithUser>> GetUserRequestName();
    Task<int> GetRequestDaysLeft(int requestId, string userId);
    
    // Used for development
    Task ResetLeaveDays();
}