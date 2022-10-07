using WebStock.Models.Entities;

namespace WebStock.Intefaces;

public interface IRepository<TEntity> : IDisposable
{
    Task<TEntity> GetById(Guid id);
    Task<List<TEntity>> GetAll();
    Task Delete(Guid id);
    Task Update(TEntity entity);
    Task Add(TEntity entity);
}
