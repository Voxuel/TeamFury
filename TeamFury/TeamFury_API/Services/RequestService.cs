using Models.Models;

namespace TeamFury_API.Services
{
    public class RequestService : ICRUDService<Request>, IRequestService
    {
        public Task<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByID<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync<T>(T newUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateAsync<T>(T toCreate)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetRequestByEmployeeID<T>(int id)
        {
            throw new NotImplementedException();
        }
    }
}
