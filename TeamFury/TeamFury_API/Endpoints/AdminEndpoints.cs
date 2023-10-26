using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using Models.Models;
using Models.Models.API_Model_Tools;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TeamFury_API.Services;
using TeamFury_API.Services.AdminServices;
using TeamFury_API.Services.EmailServices;

namespace TeamFury_API.Endpoints;

public static class AdminEndpoints
{
    public static void AdminEndpointConfig(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/employee/", async
                (IAdminService service, IMapper mapper) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await service.GetAll();
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

        app.MapGet("/api/admin/employee/view", async
            (IAdminService service) =>
        {
            try
            {
                var response = new ApiResponse();
                var result = await service.GetAllViewModels();
                response.Result = result;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Results.Ok(response);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

        app.MapGet("/api/admin/request", async
                (IRequestService service) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await service.GetAll();

                    response.Result = result;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .WithName("GetAllRequests");


        app.MapPost("/api/admin/employee/", async
                (IAdminService services, IMapper mapper, UserCreateDTO user_c_dto) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var hasher = new PasswordHasher<User>();
                    var user = mapper.Map<User>(user_c_dto);
                    user.PasswordHash = hasher.HashPassword(user, user_c_dto.Password);
                    var result = await services.CreateAsync(user, user_c_dto.Role);
                    if (result == null) return Results.BadRequest();
                    response.Result = result;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.Created;

                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).RequireAuthorization("IsAdmin")
            .Accepts<UserCreateDTO>("application/json")
            .Produces<ApiResponse>(201)
            .Produces(400)
            .WithName("CreateEmployee");

        app.MapDelete("/api/admin/employee/{id}",
                async (IAdminService services, string id) =>
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
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .Produces(400)
            .WithName("DeleteEmployee");

        app.MapPut("/api/admin/employee/",
                async (IAdminService services,
                    IMapper mapper, UserUpdateDTO newUpdate) =>
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
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization("IsAdmin")
            .Accepts<UserUpdateDTO>("application/json")
            .Produces<ApiResponse>(200)
            .Produces(204)
            .Produces(400)
            .WithName("UpdateEmployee");

        app.MapPost("/api/admin/type/",
                async (IAdminService service, IValidator<RequestTypeDto> validator,
                    IMapper mapper, RequestTypeDto rt_c_dto) =>
                {
                    try
                    {
                        var response = new ApiResponse();
                        var validResult = await validator.ValidateAsync(rt_c_dto);
                        if (!validResult.IsValid)
                        {
                            response.IsSuccess = false;
                            response.Result = validResult.Errors;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            return Results.BadRequest(response);
                        }

                        var requestType = mapper.Map<RequestType>(rt_c_dto);
                        var result = await service.CreateRequestTypeAsync(requestType);
                        if (result == null)
                        {
                            response.IsSuccess = false;
                            response.ErrorMessages.Add("Request type already exists");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            return Results.BadRequest(response);
                        }

                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.Created;
                        response.Result = result;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization("IsAdmin")
            .Accepts<RequestTypeDto>("application/json")
            .Produces<ApiResponse>(200)
            .Produces(201)
            .WithName("CreateRequestType");

        app.MapPut("/api/admin/type/{id:int}",
                async (IAdminService services, IValidator<RequestTypeDto> validator
                    , IMapper mapper, RequestTypeDto rt_u_dto,
                    int id) =>
                {
                    try
                    {
                        var response = new ApiResponse();
                        var validResult = await validator.ValidateAsync(rt_u_dto);
                        if (!validResult.IsValid)
                        {
                            response.IsSuccess = false;
                            response.Result = validResult.Errors;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            return Results.BadRequest(response);
                        }

                        var requestType = mapper.Map<RequestType>(rt_u_dto);
                        requestType.RequestTypeID = id;
                        var result = await services.UpdateRequestTypeAsync(requestType);
                        if (result == null)
                        {
                            response.IsSuccess = false;
                            response.ErrorMessages.Add("No request type with matching ID was found");
                            response.StatusCode = HttpStatusCode.NotFound;
                            return Results.NotFound(response);
                        }

                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Result = result;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization("IsAdmin")
            .Accepts<RequestTypeDto>("application/json")
            .Produces<ApiResponse>(200)
            .Produces(204)
            .Produces(400)
            .WithName("UpdateRequestType");

        app.MapDelete("/api/admin/type/{id:int}",
                async (IAdminService services, int id) =>
                {
                    try
                    {
                        var response = new ApiResponse();
                        var result = await services.DeleteRequestTypeAsync(id);
                        if (result == null)
                        {
                            response.IsSuccess = false;
                            response.ErrorMessages.Add("No request type with matching ID was found");
                            response.StatusCode = HttpStatusCode.NotFound;
                            return Results.NotFound(response);
                        }

                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Result = result;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .Produces(204)
            .Produces(400)
            .WithName("DeleteRequestType");

        app.MapPut("/api/admin/request/",
                async (IRequestService service, ILeaveDaysService leaveDays,
                    IMapper mapper, RequestUpdateDTO req_u_DTO, IEmailService emailService) =>
                {
                    try
                    {
                        var response = new ApiResponse();
                        var preUpdate = await service.GetByID(req_u_DTO.RequestID);
                        if (preUpdate == null)
                        {
                            response.ErrorMessages.Add("Request not found");
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response.IsSuccess = false;
                            return Results.BadRequest(response);
                        }

                        var request = mapper.Map<Request>(req_u_DTO);
                        await leaveDays.UpdateLeaveDaysOnAprovedRequest(req_u_DTO, preUpdate);
                        var result = await service.UpdateAsync(request);
                        var found = await leaveDays.FindByRequest(result);
                        await emailService.SendEmail(found);
                        await service.AddRequestToLog(result);
                        response.IsSuccess = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Result = result;
                        return Results.Ok(response);
                    }
                    catch (Exception e)
                    {
                        return Results.BadRequest(e.Message);
                    }
                }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .Produces(400)
            .WithName("UpdateRequest");

        app.MapGet("/api/admin/leavedays/totalused", async (ILeaveDaysService repo) =>
            {
                try
                {
                    ApiResponse response = new();
                    var result = await repo.GetLeaveDaysUsed();
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = result;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e);
                }
            }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .WithName("GetTotalLeaveDaysUsed");

        app.MapPut("/api/dev/reset/", async
            (IAdminService service) =>
        {
            await service.ResetLeaveDays();
        }).RequireAuthorization("IsAdmin");

        app.MapGet("/api/admin/user/utility", async
                (IAdminService service) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await service.GetUserRequestName();
                    if (result == null)
                    {
                        response.IsSuccess = false;
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.ErrorMessages.Add("No user with the bound request was found");
                        return Results.NotFound(response);
                    }

                    response.IsSuccess = true;
                    response.Result = result;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).RequireAuthorization("IsAdmin")
            .Produces<ApiResponse>(200)
            .Produces(400)
            .WithName("GetRequestAndConnectedUser");
    }
}