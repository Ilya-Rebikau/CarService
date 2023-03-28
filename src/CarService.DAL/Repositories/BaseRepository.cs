using CarService.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarService.DAL.Repositories
{
    internal class BaseRepository<T> : IRepository<T>
        where T : class, IModel
    {
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        protected DbContext DbContext { get; private set; }
        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>().AsNoTracking();
        }
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }
        public virtual async Task<T> CreateAsync(T obj)
        {
            var temp = DbContext.Entry(obj);
            temp.State = EntityState.Added;
            await DbContext.Set<T>().AddAsync(obj);
            await DbContext.SaveChangesAsync();
            temp.State = EntityState.Detached;
            return temp.Entity;
        }
        public virtual async Task<T> UpdateAsync(T obj)
        {
            var temp = DbContext.Entry(obj);
            temp.State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
            temp.State = EntityState.Detached;
            return temp.Entity;
        }
        public virtual async Task<T> DeleteAsync(T obj)
        {
            var temp = DbContext.Entry(obj);
            temp.State = EntityState.Deleted;
            DbContext.Set<T>().Remove(obj);
            await DbContext.SaveChangesAsync();
            temp.State = EntityState.Detached;
            return temp.Entity;
        }
    }
}
