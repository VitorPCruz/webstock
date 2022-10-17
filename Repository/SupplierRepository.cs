using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class SupplierRepository : Repository<Supplier>, IRepository<Supplier>
{
    public SupplierRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }
}
