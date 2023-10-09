using Microsoft.EntityFrameworkCore;
using TeamFury_API.Data;

namespace TeamFury_API.Endpoints;

public static class AdminEndpoints
{
    public static void ConfigureAdminEndpoints(this WebApplication app)
    {
        app.MapGet("/api/admin/", GetAllAdmins);
    }

    private static async Task<IResult> GetAllAdmins(AppDbContext context)
    {
        return Results.Ok(await context.Admins.ToListAsync());
    }
}