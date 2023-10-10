using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using TeamFury_API.Data;

namespace TeamFury_API.Endpoints;

public static class AdminEndpoints
{
    public static void AdminEndpointConfig(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/", async (UserManager<User> manager, IMapper mapper) =>
        {
            return Results.Ok(await manager.Users.ToListAsync());
        }).RequireAuthorization("IsAdmin");
    }
}