using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.AdminServices;

public interface IAdminService : ICRUDService<User>
{
    Task<User> GetByIdAsync(string id);
    Task<User> UpdateAsync(User newUpdate, string password);
    Task<User> DeleteAsync(string id);
    Task<User> CreateAsync(User user, string role);

    Task<RequestType> CreateRequestTypeAsync(RequestType requestType);
    Task<RequestType> UpdateRequestTypeAsync(RequestType requestType);
    Task<RequestType> DeleteRequestTypeAsync(int id);
}