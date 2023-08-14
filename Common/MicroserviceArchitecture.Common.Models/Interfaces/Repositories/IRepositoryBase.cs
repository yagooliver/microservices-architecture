using MicroserviceArchitecture.Common.Models.Entities;

namespace MicroserviceArchitecture.Common.Models.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>?> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
    }
}