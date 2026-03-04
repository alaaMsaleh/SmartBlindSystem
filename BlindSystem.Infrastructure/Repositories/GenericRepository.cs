using BlindSystem.Domain.Entities; // Add this using directive to resolve BaseEntity
using BlindSystem.Domain.Interfaces;
using BlindSystem.Infrastructure.Data.DBContext;
using Microsoft.EntityFrameworkCore;

namespace BlindSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly BlindSystemDbContext _dbContext;
        public GenericRepository(BlindSystemDbContext dbcontext)
        {

            _dbContext = dbcontext;


        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }


        public async Task<T> GetAsync(Guid id)
        {
           return await _dbContext.Set<T>().FindAsync(id);  //use find to search frist at cach tif not foumd go to server DB
        }


        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async void Delete(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }
    }
}
