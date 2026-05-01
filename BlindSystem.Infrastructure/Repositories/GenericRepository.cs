using BlindSystem.Domain.Entities; // Add this using directive to resolve BaseEntity
using BlindSystem.Domain.Interfaces;
using BlindSystem.Domain.ISpecifications;
using BlindSystem.Infrastructure.Data.DBContext;
using BlindSystem.Infrastructure.Specification;
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


        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuerey(_dbContext.Set<T>().AsQueryable(), spec);
        }


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        public async Task<T> GetEntityWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }


        public async Task<T> GetAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);  //use find to search frist at cach tif not foumd go to server DB
        }


        public T Add(T entity)
        {
            var entry = _dbContext.Set<T>().Add(entity);
            return entry.Entity;
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
            }
        }
    }
}
