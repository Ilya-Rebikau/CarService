using CarService.DAL.Interfaces;
using CarService.MainAPI.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CarService.MainAPI.Services
{
    internal class BaseService<T> : IBaseService<T>
            where T : IModel
    {
        protected BaseService(IRepository<T> repository, IConfiguration configuration)
        {
            Repository = repository;
            CountOnPage = configuration.GetValue<int>("CountOnPage");
        }

        protected int CountOnPage { get; private set; }
        protected IRepository<T> Repository { get; private set; }

        public virtual IEnumerable<T> GetAll(int pageNumber)
        {
            CheckForPageNumber(pageNumber);
            var models = Repository.GetAll();
            models = models.OrderBy(m => m.Id).Skip((pageNumber - 1) * CountOnPage).Take(CountOnPage);
            return models;
        }

        public async virtual Task<T> GetById(int id)
        {
            var model = await CheckForIdAndGetModel(id);
            return model;
        }

        public async virtual Task<T> Create(T obj)
        {
            var model = await Repository.Create(obj);
            return model;
        }

        public async virtual Task<T> Update(T obj)
        {
            var model = await Repository.Update(obj);
            return model;
        }

        public async virtual Task<T> Delete(T obj)
        {
            var model = await Repository.Delete(obj);
            return model;
        }

        public async virtual Task<int> DeleteById(int id)
        {
            var model = await CheckForIdAndGetModel(id);
            await Delete(model);
            return id;
        }

        private static void CheckForPageNumber(int pageNumber)
        {
            if (pageNumber <= 0)
            {
                throw new ValidationException("Номер страницы должен быть положительным!");
            }
        }

        private async Task<T> CheckForIdAndGetModel(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("ID должен быть положительным!");
            }

            var model = await Repository.GetById(id);
            return model is null ? throw new ValidationException("Объект не найден!") : model;
        }
    }
}
