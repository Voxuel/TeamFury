using AutoMapper;
using FluentValidation;
using Models.DTOs;
using Models.Models;
using Models.Models.API_Model_Tools;
using System.Net;
using TeamFury_API.Services;

namespace TeamFury_API.Endpoints
{
    public static class UserEndpoint
    {
        public static void UserEndpointConfig(this IEndpointRouteBuilder app)
        {
          
            app.MapPost("/api/user/request/{id}", async
                (IRequestService service, IMapper mapper, IValidator<RequestCreateDTO> validator,
                    RequestCreateDTO req_c_DTO, string id) =>
                {
                    try
                    {
                        var response = new ApiResponse();
                        var validationResult = await validator.ValidateAsync(req_c_DTO);
                        if (!validationResult.IsValid)
                        {
                            response.IsSuccess = false;
                            response.Result = validationResult.Errors;
                            response.StatusCode = HttpStatusCode.BadRequest;
                            return Results.BadRequest(response);
                        }

                        var request = mapper.Map<Request>(req_c_DTO);
                        var tempRequestType = new RequestType();
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


            app.MapGet("/api/user/request/{id}", async
                (IRequestService service, IMapper mapper, string id) =>
            {
                try
                {
                    var response = new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true
                    };
                    var result = await service.GetRequestsByEmployeeID(id);
                    if (!result.Any())
                    {
                        response.Result = null;
                        return Results.Ok(response);
                    }

                    var requests = mapper.Map<IEnumerable<RequestLogEntityDTO>>(result);
                    response.Result = requests;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e.Message);
                }
            }).AllowAnonymous()
                .Produces<ApiResponse>(200)
                .WithName("GetPendingRequestsForEmployee");
         
            app.MapGet("/api/user/request/log/{id}",
                async (IRequestService service, IMapper mapper, string id) =>
                {
                    try
                    {
                        var response = new ApiResponse();
                        var result = await service.GetAllLogs(id);
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