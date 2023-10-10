using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;

namespace TeamFury_API.Endpoints;

public static class SecurityEndpoints
{
    public static void SecurityConfig(this WebApplication app)
    {
        app.MapPost("/api/token/", CreateJwtToken).AllowAnonymous();
    }


    private static async Task<IResult> CreateJwtToken(IConfiguration config, IdentityUser user)
    {
        var hasher = new PasswordHasher<IdentityUser>();
        var pass = hasher.HashPassword(null, "Password");

        
        if (user.UserName != "Admin1" && user.PasswordHash != pass) return Results.Unauthorized();
        
        var claims = new[]
        {
            new Claim("Id", "1"),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var signinCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer:config["Jwt:Issuer"],
            audience:config["Jwt:Audience"],
            expires:DateTime.UtcNow.AddHours(6),
            claims:claims,
            signingCredentials:signinCred
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(jwt);
    }
}