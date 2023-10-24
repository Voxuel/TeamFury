using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Models;
using Models.Models.API_Model_Tools;
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

        app.MapPut("/api/user/password/{id}", async
            (IAuthService service, string password, string id) =>
        {
            try
            {
                var response = new ApiResponse();
                var result = await service.UpdatePassword(password, id);
                switch (result.ResultStatus)
                {
                    case Status.Empty:
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.ErrorMessages.Add("No user found");
                        return Results.NotFound(response);
                    case Status.Invalid:
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.ErrorMessages.Add("Invalid password");
                        return Results.BadRequest(response);
                    case Status.Accepted:
                        break;
                    default:
                        return null;
                }
                response.Result = result;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).AllowAnonymous()
            .Produces<ApiResponse>(200)
            .Produces(204)
            .Produces(400)
            .WithName("UpdateUserPassword");
    }
    
    
}