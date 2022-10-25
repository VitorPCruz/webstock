using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Models;

namespace WebStock.Repository;

public class CategoryRepository : Repository<Category>
{
    public CategoryRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }

    public override async Task<List<Category>> GetAllEntities()
    {
        return await _dbSet.OrderByDescending(x => x.Name).ToListAsync();
    }
}
