using Models.Models.API_Model_Tools;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TeamFury_API.Data;
using TeamFury_API.Services;
using TeamFury_API.Services.AdminServices;

namespace TeamFury_API.Endpoints
{
    public static class SharedEndpoints
    {
        public static void SharedEndpointsConfig(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/shared/employee/{id}",
                async (IAdminService services, string id) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await services.GetByIdAsync(id);
                    if (result == null)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessages.Add("User not found");
                        response.StatusCode = HttpStatusCode.NotFound;
                        return Results.NotFound(response);
                    }

                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Result = result;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e);
                }

            }).AllowAnonymous()
                .Produces<ApiResponse>(200)
                .Produces(400)
                .WithName("GetSingleEmployee");

            app.MapDelete("/api/request/{id}",
                async (IRequestService service, int id) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await service.DeleteAsync(id);

                    response.Result = result;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e);
                }
            }).AllowAnonymous()
                .Produces(204)
                .WithName("DeleteRequest");

            app.MapGet("/api/request/{id:int}",
                async (IRequestService service, int id) =>
            {
                try
                {
                    var response = new ApiResponse();
                    var result = await service.GetByID(id);

                    response.Result = result;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e);
                }
            }).AllowAnonymous()
                .WithName("GetRequestByID");
          
            app.MapGet("/api/user/leavedays/used/{id}",
               async (ILeaveDaysService service, string id) =>
               {
                   try
                   {
                       var response = new ApiResponse();
                       var result = await service.GetLeaveDaysByEmployeeID(id);
                       if (result == null)
                       {
                           response.IsSuccess = false;
                           response.StatusCode = HttpStatusCode.BadRequest;
                           response.ErrorMessages.Add("User not found");
                           return Results.BadRequest();
                       }
                       response.IsSuccess = true;
                       response.StatusCode = HttpStatusCode.OK;
                       response.Result = result;
                       return Results.Ok(response);
                   }
                   catch (Exception e)
                   {
                       return Results.BadRequest(e);
                   }
               }).AllowAnonymous()
                 .Produces<ApiResponse>(200)
                 .WithName("GetLeaveDaysLeft");

            app.MapGet("/api/requesttype", async (AppDbContext context) => {
                try
                {
                    var response = new ApiResponse();
                    var result = await context.RequestTypes.ToListAsync();

                    response.Result = result;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    return Results.Ok(response);
                }
                catch (Exception e)
                {
                    return Results.BadRequest(e);
                }
            }).AllowAnonymous()
            .WithName("GetAllRequestTypes");
        }
    }
}
