using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class CategoryRepository : IRepository<Category>
{
    private readonly ApplicationDbContext _dbcontext;

    public CategoryRepository(ApplicationDbContext context)
    {
        _dbcontext = context;
    }

    public async Task<Category> GetEntityById(Guid id)
    {
        return await _dbcontext.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<Category>> GetAllEntities()
    {
        return await _dbcontext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task DeleteEntityById(Guid categoryId)
    {
       var category = await _dbcontext.Categories.AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        _dbcontext.Remove(category);
    }

    public async Task UpdateEntity(Category category)
    {
        _dbcontext.Update(category);
    }

    public void Dispose()
    {
        _dbcontext?.Dispose();
    }

    public async Task AddEntity(Category entity)
    {
        await _dbcontext.AddAsync(entity);
    }
}
