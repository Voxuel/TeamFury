using Microsoft.AspNetCore.Identity;
using Models.DTOs;

namespace TeamFury_API.Services.SecurityServices;

public class AuthService : IAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService()
    {
        
    }
    
    
    public Task<(int, string)> Registration()
    {
        throw new NotImplementedException();
    }

    public Task<(int, string)> Login(LoginDTO login)
    {
        throw new NotImplementedException();
    }
}