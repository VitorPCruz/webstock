using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ApplicationDbContext context, ICategoryRepository repository)
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
                TempData["notification"] = Notification.SendNotification("Please, chech the fields.", NotificationType.Warning);
                return View(category);
            }

            var register = await _categoryRepository.GetEntityById(category.Id);
            var nameExists = await _categoryRepository.CheckIfCategoryExists(category.Name);

            if (register == null)
            {
                if (nameExists)
                {
                    TempData["notification"] = Notification.SendNotification("This name exists. Try other name.", NotificationType.Warning);
                    return View();
                }

                category.Id = Guid.NewGuid();
                _categoryRepository.AddEntity(category);
                TempData["notification"] = Notification.SendNotification("Category created.");
                await _context.SaveChangesAsync();
            }

            if (!category.Equals(register) && register != null)
            {
                _categoryRepository.UpdateEntity(category);
                TempData["notification"] = Notification.SendNotification("Category updated.");
                await _context.SaveChangesAsync();
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

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = await _categoryRepository.GetEntityById(id);

            if (category != null)
            {
                _categoryRepository.DeleteEntityById(id);
                TempData["notification"] = Notification.SendNotification("Category removed.");
            }
            else
            {
                TempData["notification"] = Notification.SendNotification("Not is possible get the category.", NotificationType.Error);
                return RedirectToAction(nameof(Index));
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
