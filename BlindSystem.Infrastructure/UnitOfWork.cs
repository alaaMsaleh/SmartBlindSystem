using BlindSystem.Domain.Entities;
using BlindSystem.Domain.Interfaces;
using BlindSystem.Infrastructure.Data.DBContext;
using BlindSystem.Infrastructure.Repositories;
using System.Collections;

namespace BlindSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly IGenericRepository _Repository;
        private readonly BlindSystemDbContext _dbContext;

        private Hashtable _Repository;
        public UnitOfWork(BlindSystemDbContext dbContext)
        {
            _dbContext = dbContext;

            _Repository = new Hashtable();
        }
        // There must be a method that returns a repository.
        // Instead of creating each repository manually,
        // I create a generic method that creates the repository automatically.
        // This avoids creating multiple instances manually.
        // If the repository was created before, it will be reused.
        // Otherwise, a new instance will be created and stored.
        //Create dectionar 
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            //i need store repo generic to not repait i that mean not solve problem

            var key = typeof(TEntity).Name;

            if (!_Repository.ContainsKey(key))
            {

                var Repos = new GenericRepository<TEntity>(_dbContext);
                _Repository.Add(key, Repos);
            }
            return _Repository[key] as IGenericRepository<TEntity>;
        }
        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }


    }
}
