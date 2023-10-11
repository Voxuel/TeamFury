using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using Models.Models.API_Model_Tools;
using TeamFury_API.Data;
using TeamFury_API.Services.UserServices;

namespace TeamFury_API.Endpoints;

public static class AdminEndpoints
{
    public static void AdminEndpointConfig(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/employee/", async (UserManager<User> manager, IMapper mapper) =>
        {
            return Results.Ok(await manager.Users.ToListAsync());
        }).RequireAuthorization("IsAdmin");
        
        app.MapPost("/api/admin/employee/", async
            (IUserServices services, UserCreateDTO user_c_dto) =>
        {
            var response = new ApiResponse();

            var result = await services.CreateEmployeeUser(user_c_dto);
            if (result == null) return Results.BadRequest();
            response.Result = result;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;

            return Results.Ok(response);
        }).RequireAuthorization("IsAdmin");
    }
}