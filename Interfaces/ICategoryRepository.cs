using Microsoft.AspNetCore.Mvc.Rendering;
using WebStock.Models;

namespace WebStock.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<bool> CheckIfCategoryExists(string name);
    SelectList GetCategoriesEnabled();
}
