using Models.Models;

namespace TeamFury_API.Services
{
    public interface IRequestService: ICRUDService<Request>
    {
        Task<Request> GetRequestByEmployeeID(string id);
    }
}
