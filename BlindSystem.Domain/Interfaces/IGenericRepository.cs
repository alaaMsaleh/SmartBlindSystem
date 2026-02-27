using BlindSystem.Domain.Entities;

namespace BlindSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(Guid id);

    }
}
