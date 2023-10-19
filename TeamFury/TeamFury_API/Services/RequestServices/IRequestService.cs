using Models.Models;

namespace TeamFury_API.Services
{
    public interface IRequestService: ICRUDService<Request>
    {
        Task<IEnumerable<Request>> GetRequestsByEmployeeID(string id);
        Task<Request> CreateAsync(Request toCreate, string id);

        Task<RequestType> GetRequestTypeID(int id);

        Task<RequestLog> AddRequestToLog(Request request);
        Task<IEnumerable<Request>> GetAllLogs(string id);

    }
}
