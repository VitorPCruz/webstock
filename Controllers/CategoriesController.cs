using Microsoft.AspNetCore.Mvc;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Category> _categoryRepository;

        public CategoriesController(ApplicationDbContext context, IRepository<Category> repository)
        {
            _context = context;
            _categoryRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryRepository.GetAllEntities());
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var register = await _categoryRepository.GetEntityById(id);

            if (register == null)
                return View();

            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Category category)
        {
            if (!ModelState.IsValid)
            {
                TempData["notification"] = Notification.ToSerial("Please, chech the fields.", NotificationType.Warning);
                return View(category);
            }

            var register = await _categoryRepository.GetEntityById(category.Id);

            if (register == null)
            {
                category.Id = Guid.NewGuid();
                _categoryRepository.AddEntity(category);
                
                await _context.SaveChangesAsync();
                
                TempData["notification"] = Notification.ToSerial("Category created.");

            }

            if (!category.Equals(register) && register != null)
            {
                _categoryRepository.UpdateEntity(category);
                await _context.SaveChangesAsync();
                
                TempData["notification"] = Notification.ToSerial("Category updated.");
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryRepository.GetEntityById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _categoryRepository.GetEntityById(id);

            if (category != null)
            {
                _categoryRepository.DeleteEntityById(id);
                TempData["notification"] = Notification.ToSerial("Category removed.");
            }
            else
            {
                TempData["notification"] = Notification.ToSerial("Not is possible get the category.", NotificationType.Error);
                return RedirectToAction(nameof(Index));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
