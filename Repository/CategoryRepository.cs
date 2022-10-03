using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using WebStock.Data;
using WebStock.Intefaces;
using WebStock.Models;
using WebStock.Models.Entities;

namespace WebStock.Repository;

public class CategoryRepository : IRepository<Category>
{
    private readonly ApplicationDbContext _dbcontext;

    public CategoryRepository(ApplicationDbContext context)
    {
        _dbcontext = context;
    }

    public async Task<Category> GetById(Guid id)
    {
        return await _dbcontext.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<List<Category>> GetAll()
    {
        return await _dbcontext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task Delete(Guid categoryId)
    {
       var category = await _dbcontext.Categories.AsNoTracking()
            .FirstOrDefaultAsync(category => category.Id == categoryId);

        _dbcontext.Remove(category);

        //await _dbcontext.SaveChangesAsync();
    }

    public async Task Update(Category category)
    {
        _dbcontext.Update(category);
        //await _dbcontext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbcontext?.Dispose();
    }

    public async Task Add(Category entity)
    {
        await _dbcontext.AddAsync(entity);
    }
}
