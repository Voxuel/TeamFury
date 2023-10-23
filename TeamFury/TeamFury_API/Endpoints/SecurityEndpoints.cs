using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;
using TeamFury_API.Services.SecurityServices;
namespace TeamFury_API.Endpoints;

public static class SecurityEndpoints
{
    public static void AddSecurityEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login/", async
            (IAuthService authService, LoginDTO login) =>
        {
            if (login == null) return Results.BadRequest();

            var (status, message) = await authService.Login(login);
            
            return status == 0 ? Results.BadRequest() : Results.Ok(message);
        }).AllowAnonymous()
            .WithName("Login");
    }
    
    
}