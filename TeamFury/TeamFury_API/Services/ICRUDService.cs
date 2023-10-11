namespace TeamFury_API.Services
{
    public interface ICRUDService<T>
    {
        Task<T> GetAll<T>();
        Task<T> GetByID<T>(int id);
        Task<T> UpdateAsync<T>(T newUpdate);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(T toCreate);

    }
}
