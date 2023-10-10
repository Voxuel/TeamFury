using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Models;

namespace TeamFury_API.Endpoints;

public static class SecurityEndpoints
{

    public static void SecurityConfig(this WebApplication app)
    {
        app.MapPost("/api/token/", CreateJwtToken).AllowAnonymous();
    }


    private static async Task<IResult> CreateJwtToken(IConfiguration config, LoginDTO user)
    {
        if (user.Username != "Admin2" && user.Password != "MTG15") return Results.Unauthorized();
        
        var claims = new[]
        {
            new Claim("Id", "1"),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role","admin")
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