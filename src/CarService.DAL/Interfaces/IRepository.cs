namespace CarService.DAL.Interfaces
{
    /// <summary>
    /// Представляет паттерн репозиторий для CRUD операций.
    /// </summary>
    public interface IRepository<T>
        where T : IModel
    {
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<T> Delete(T obj);
    }
}
