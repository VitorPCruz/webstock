using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }

    public override async Task<List<Supplier>> GetAllEntities()
    {
        return await _dbSet.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<bool> CheckDocument(string document)
    {
         var check = await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Document == document);

        if (check == null)
            return false;

        return true;
    }

    public SelectList GetSuppliersEnabled()
    {
        return new SelectList(_dbcontext.Suppliers.AsNoTracking().Where(x => x.Active), "Id", "Name");
    }
}
