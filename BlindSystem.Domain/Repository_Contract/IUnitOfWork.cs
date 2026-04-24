using BlindSystem.Domain.Entities;

namespace BlindSystem.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;


        Task<int> CompleteAsync();
    }
}
