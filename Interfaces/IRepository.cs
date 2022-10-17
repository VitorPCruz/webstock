using WebStock.Models.Entities;

namespace WebStock.Interfaces;

public interface IRepository<TEntity> : IDisposable
{
    Task<TEntity> GetEntityById(Guid id);
    Task<List<TEntity>> GetAllEntities();
    Task DeleteEntityById(Guid id);
    Task UpdateEntity(TEntity entity);
    Task AddEntity(TEntity entity);
}
