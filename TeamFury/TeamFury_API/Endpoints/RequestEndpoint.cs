using AutoMapper;
using FluentValidation;
using Models.DTOs;
using Models.Models;
using Models.Models.API_Model_Tools;
using System.Data.Entity;
using System.Net;
using TeamFury_API.Data;
using TeamFury_API.Services;

namespace TeamFury_API.Endpoints
{
    public static class RequestEndpoint
    {
        public static void RequestEndpointConfig(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/request", async (IRequestService service) =>
            {
                try
                {
                    ApiResponse response = new ApiResponse();
                    
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
            .WithName("GetAllRequests");

            app.MapGet("/api/request/{id:int}", async (IRequestService service, int id) =>
            {
                try
                {
                    ApiResponse response = new ApiResponse();

                    response.Result = await service.GetByID(id);
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {

                    return Results.BadRequest(e);
                }
            }).AllowAnonymous().WithName("GetRequestByID");

            app.MapDelete("/api/request/", async (IRequestService service, int id) =>
            {
                try
                {
                    ApiResponse response = new ApiResponse();

                    response.Result = await service.DeleteAsync(id);
                    response.IsSuccess = true;
                    response.StatusCode= HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {

                    return Results.BadRequest(e);
                }
            }).WithName("DeleteRequest")
            .Produces(204);

            app.MapPost("/api/request/", async (IRequestService service, IMapper mapper, IValidator<RequestCreateDTO> validator,
                RequestCreateDTO req_c_DTO, string id) =>
            {
                try
                {
                    ApiResponse response = new ApiResponse();
                    var validationResult = await validator.ValidateAsync(req_c_DTO);
                    if (!validationResult.IsValid)
                    {
                        response.IsSuccess = false;
                        response.Result = validationResult.Errors;
                        response.StatusCode = HttpStatusCode.BadRequest;
                        return Results.BadRequest(response);
                    }
                    var request = mapper.Map<Request>(req_c_DTO);
                    RequestType tempRequestType = new RequestType();
                    tempRequestType = await service.GetRequestTypeID(req_c_DTO.RequestTypeID);
                    request.RequestType = tempRequestType;
                    var result = await service.CreateAsync(request, id);
                    if (result != null)
                    {
                        if (!string.IsNullOrEmpty(result.MessageForDecline))
                        {
                            response.IsSuccess = false;
                            response.ErrorMessages.Add(result.MessageForDecline);
                            response.StatusCode = HttpStatusCode.BadRequest;
                        }
                    }
                    if (result == null)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessages.Add("Request already exist");
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                    response.IsSuccess = true;
                    response.Result = result;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {

                    return Results.BadRequest(e);
                }
            }).AllowAnonymous()
            .Produces<ApiResponse>(200)
            .Produces(201)
            .Accepts<RequestCreateDTO>("application/json")
            .WithName("CreateRequest");

            app.MapGet("/api/request/log/", async
                (IRequestService service, IMapper mapper, string EmpId) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await service.GetAllLogs(EmpId);
                    var logDtos = mapper.Map<IEnumerable<RequestLogEntityDTO>>(result);
                    if (!result.Any()) return Results.Ok();
                    
                    response.Result = logDtos;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e);
                }
            }).AllowAnonymous()
                .Produces<ApiResponse>(200)
                .WithName("GetAllRequestLogs");
        }
    }
}
