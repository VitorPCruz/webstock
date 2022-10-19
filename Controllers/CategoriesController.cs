using Microsoft.AspNetCore.Mvc;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Category> _repositoryCategory;

        public CategoriesController(ApplicationDbContext context, IRepository<Category> repository)
        {
            _context = context;
            _repositoryCategory = repository;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _repositoryCategory.GetAllEntities());
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var register = await _repositoryCategory.GetEntityById(id);
            if (register == null)
                return View();

            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var register = await _repositoryCategory.GetEntityById(category.Id);

            if (register == null)
            {
                category.Id = Guid.NewGuid();
                _repositoryCategory.AddEntity(category);
                await _context.SaveChangesAsync();
            }
            else
            {
                _repositoryCategory.UpdateEntity(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _repositoryCategory.GetEntityById(id);

            if (category == null) 
                return NotFound();

            return View(category);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _repositoryCategory.GetEntityById(id);

            if (category != null)
                _context.Categories.Remove(category);
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
