using BlindSystem.Domain.Entities;
using BlindSystem.Domain.ISpecifications;

namespace BlindSystem.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetAsync(Guid id);

        T Add(T entity);

        void Update(T entity);

        Task DeleteAsync(Guid id);



        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

        Task<T> GetEntityWithSpecAsync(ISpecifications<T> spec);
    }
}
