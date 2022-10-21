using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Models;

namespace WebStock.Repository;

public class CategoryRepository : Repository<Category>
{
    public CategoryRepository(ApplicationDbContext dbcontext) : base(dbcontext)
    { }
}
