using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models.Entities;

namespace WebStock.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly ApplicationDbContext _dbcontext;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        _dbSet = _dbcontext.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetEntityById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<List<TEntity>> GetAllEntities()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task AddEntity(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public virtual async Task DeleteEntityById(Guid id)
    {
        _dbSet.Remove(new TEntity { Id = id });

    }

    public virtual async Task UpdateEntity(TEntity entity)
    {
         _dbcontext.ChangeTracker.Clear();
         _dbSet.Update(entity);
    }

    public void Dispose()
    {
        _dbcontext?.Dispose();
    }
}
