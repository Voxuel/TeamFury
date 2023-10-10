namespace TeamFury_API.Services
{
    public interface ICRUDService<PlaceHolder> where PlaceHolder : class
    {
        Task<T> GetAll<T>();
        Task<T> GetByID<T>(int id);
        Task<T> UpdateAsync<T>(PlaceHolder newUpdate);
        Task<T> DeleteAsync<T>(int id);
        Task<T> CreateAsync<T>(PlaceHolder toCreate);

    }
}
