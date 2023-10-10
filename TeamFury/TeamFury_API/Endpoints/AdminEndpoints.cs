using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using TeamFury_API.Data;

namespace TeamFury_API.Endpoints;

public static class AdminEndpoints
{
    public static void AdminEndpointConfig(this WebApplication app)
    {
        app.MapGet("/api/admin/", GetAllUsersAsync).RequireAuthorization("IsAdmin");
    }

    private static async Task<IResult> GetAllUsersAsync(AppDbContext context)
    {
        return Results.Ok(await context.Users.ToListAsync());
    }
}