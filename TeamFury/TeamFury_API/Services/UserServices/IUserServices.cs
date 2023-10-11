using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.UserServices;

public interface IUserServices
{
    Task<User> CreateEmployeeUser(UserCreateDTO user);
    Task CreateRoleAsync();
}