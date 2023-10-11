using Models.Models;

namespace TeamFury_API.Services
{
    public class RequestService : ICRUDService<Request>, IRequestService
    {
        public Task<Request> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Request> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Request> UpdateAsync(Request newUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<Request> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Request> CreateAsync(Request toCreate)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetRequestByEmployeeID<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
