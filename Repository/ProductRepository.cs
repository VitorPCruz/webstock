using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class ProductRepository : Repository<Product>, IRepository<Product>
{
    public ProductRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }

    public override async Task<Product> GetEntityById(Guid id)
    {
        return await _dbSet.Include(p => p.Category)
            .Include(p => p.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<List<Product>> GetAllEntities()
    {
        return await _dbSet.Include(p => p.Category)
            .Include(p => p.Supplier)
            .ToListAsync();
    }
}
