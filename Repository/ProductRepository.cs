using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
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

    public async Task<Product> GetProductByProductCode(string productCode)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.ProductCode == productCode);
    }

    public async Task<Product> GetProductByCodeBar(string codeBar)
    {
        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.CodeBar == codeBar);
    }
}
