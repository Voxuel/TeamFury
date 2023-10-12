using System.Net;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using Models.Models.API_Model_Tools;
using TeamFury_API.Data;
using TeamFury_API.Services.AdminServices;
using TeamFury_API.Services.UserServices;

namespace TeamFury_API.Endpoints;

public static class AdminEndpoints
{
    public static void AdminEndpointConfig(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/employee/", async
                (UserManager<User> manager, IMapper mapper) =>
        {
            try
            {
                var response = new ApiResponse();
                var result = await manager.Users.ToListAsync();
                response.Result = result;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e);
            }
        }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .WithName("GetAllEmployees");
        
        app.MapPost("/api/admin/employee/", async
            (IAdminService services, IMapper mapper, UserCreateDTO user_c_dto) =>
        {
            try
            {
                var response = new ApiResponse();
                var hasher = new PasswordHasher<User>();
                var user = mapper.Map<User>(user_c_dto);
                user.PasswordHash = hasher.HashPassword(null, user_c_dto.Password);
                var result = await services.CreateAsync(user, user_c_dto.Role);
                if (result == null) return Results.BadRequest();
                response.Result = result;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;

                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e);
            }
        }).RequireAuthorization("IsAdmin")
            .Accepts<UserCreateDTO>("application/json")
            .Produces<ApiResponse>(201)
            .Produces(400)
            .WithName("CreateEmployee");

        app.MapDelete("/api/admin/employee/{id}", async
                (IAdminService services, string id) =>
        {
            try
            {
                var response = new ApiResponse();
                var result = await services.DeleteAsync(id);

                if (result == null)
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    return Results.NotFound(response);
                }

                response.Result = result;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e);
            }
        }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .Produces(400)
            .WithName("DeleteEmployee");

        app.MapPut("/api/admin/employee/", async
            (IAdminService services, IMapper mapper, UserUpdateDTO newUpdate) =>
        {
            try
            {
                var response = new ApiResponse();
                var user = mapper.Map<User>(newUpdate);
                var result = await services.UpdateAsync(user, newUpdate.Password);

                if (result == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No employee with the given ID could be found");
                    return Results.NotFound(response);
                }

                response.Result = result;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e);
            }
        }).RequireAuthorization("IsAdmin")
            .Accepts<UserUpdateDTO>("application/json")
            .Produces<ApiResponse>(200)
            .Produces(204)
            .Produces(400)
            .WithName("UpdateEmployee");
    }
}