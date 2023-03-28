﻿namespace CarService.DAL.Interfaces
{
    /// <summary>
    /// Представляет паттерн репозиторий для CRUD операций.
    /// </summary>
    public interface IRepository<T>
        where T : IModel
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task<T> DeleteAsync(T obj);
    }
}
