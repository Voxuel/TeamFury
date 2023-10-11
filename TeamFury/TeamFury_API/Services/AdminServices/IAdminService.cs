using Models.Models;

namespace TeamFury_API.Services.AdminServices;

public interface IAdminService : ICRUDService<User>
{
    Task<User> DeleteAsync(string id);
}