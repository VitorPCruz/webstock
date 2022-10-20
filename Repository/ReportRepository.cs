using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class ReportRepository : Repository<Report>, IRepository<Report>
{
    public ReportRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }

    public override async Task<List<Report>> GetAllEntities()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Product)
            .ToListAsync();
    }
}
