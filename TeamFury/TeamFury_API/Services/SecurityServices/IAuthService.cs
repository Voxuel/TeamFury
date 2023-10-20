using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.SecurityServices;

public interface IAuthService
{
    Task<(int, string)> Login(LoginDTO login);
    Task<StatusResult> UpdatePassword(string password, string id);
}