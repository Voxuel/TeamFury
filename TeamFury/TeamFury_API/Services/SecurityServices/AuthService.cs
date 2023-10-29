using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
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
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="login">Login object containing username and password</param>
    /// <returns>Access token with set role for authentication/authorization</returns>
    public async Task<(int, string)> Login(LoginDTO login)
    {
        var user = await _userManager.FindByNameAsync(login.Username);
        if (user == null) return (0, "Invalid Username or password");
        
        if (!await _userManager.CheckPasswordAsync(user, login.Password)) return (0, "Invalid Username or password");

        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new("Id", Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.ToUpper())));

        var token = GenerateToken(authClaims);

        return (1, token);
    }

    public async Task<StatusResult> UpdatePassword(string password, string id)
    {
        var found = await _userManager.FindByIdAsync(id);
        var result = new StatusResult();
        if (found == null || string.IsNullOrEmpty(password))
        {
            result.ResultStatus = Status.Empty;
            return result;
        };
        
        var passwordToken = await _userManager.GeneratePasswordResetTokenAsync(found);
        var valid = await _userManager.ResetPasswordAsync(found, passwordToken, password);
        if (!valid.Succeeded)
        {
            result.ResultStatus = Status.Invalid;
            return result;
        }
        await _userManager.UpdateAsync(found);
        result.ResultStatus = Status.Accepted;
        result.ResultUserObject = found;
        return result;
    }
    
    
    
    /// <summary>
    /// Generates a new access token.
    /// </summary>
    /// <param name="claims"></param>
    /// <returns>JwtToken with roles as claims for login</returns>
    private string GenerateToken(IEnumerable<Claim> claims)
    {
        SecretClientOptions options = new SecretClientOptions()
        {
            Retry =
            {
                Delay = TimeSpan.FromSeconds(2),
                MaxDelay = TimeSpan.FromSeconds(16),
                MaxRetries = 5,
                Mode = RetryMode.Exponential
            }
        };
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

public class StatusResult
{
    public User ResultUserObject { get; set; }

    public Status ResultStatus { get; set; }
}
public enum Status
{
    Accepted, Invalid, Empty
}