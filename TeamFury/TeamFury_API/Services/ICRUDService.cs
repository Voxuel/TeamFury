namespace TeamFury_API.Services
{
    public interface ICRUDService<T>
    {
        Task<T> GetAll();
        Task<T> GetByID(int id);
        Task<T> UpdateAsync(T newUpdate);
        Task<T> DeleteAsync(int id);
        Task<T> CreateAsync(T toCreate);

    }
}
