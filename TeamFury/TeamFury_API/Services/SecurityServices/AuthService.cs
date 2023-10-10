using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Services.SecurityServices;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }
    
    
    public Task<(int, string)> Registration()
    {
        throw new NotImplementedException();
    }

    public async Task<(int, string)> Login(LoginDTO login)
    {
        var user = await _userManager.FindByNameAsync(login.Username);
        if (user == null) return (0, "Invalid user");
        
        
        if (!await _userManager.CheckPasswordAsync(user, login.Password)) return (0, "Invalid password");

        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        string token = GenerateToken(authClaims);

        return (1, token);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var tokenDesc = new SecurityTokenDescriptor()
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            Expires = DateTime.Now.AddHours(3),
            SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDesc);
        return tokenHandler.WriteToken(token);
    }
}