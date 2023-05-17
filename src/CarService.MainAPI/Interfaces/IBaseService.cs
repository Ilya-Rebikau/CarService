namespace CarService.MainAPI.Interfaces
{
    public interface IBaseService<T>
    {
        IEnumerable<T> GetAll(int pageNumber);
        Task<T> GetById(int id);
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<T> Delete(T obj);
        Task<int> DeleteById(int id);
    }
}
