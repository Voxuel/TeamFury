namespace TeamFury_API.Services
{
    public interface IRequestService
    {
        Task<T> GetRequestByEmployeeID<T>(int id);
    }
}
