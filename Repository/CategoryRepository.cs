using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Repository;

public class CategoryRepository : Repository<Category>, IRepository<Category>
{
    public CategoryRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }
}
