using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext dbcontext)
        : base(dbcontext) { }

    public override async Task<List<Category>> GetAllEntities()
    {
        return await _dbSet.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<bool> CheckIfCategoryExists(string name)
    {
        var category = await _dbcontext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name);
        if (category == null)
            return false;

        return true;
    }

    public SelectList GetCategoriesEnabled()
    {
        return new SelectList(_dbcontext.Categories.AsNoTracking().Where(x => x.Active), "Id", "Name");
    }
}
