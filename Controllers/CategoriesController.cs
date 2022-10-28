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
                SendNotification("Por favor, verifique os campos novamente.", NotificationType.Warning);
                return View(category);
            }

            var register = await _categoryRepository.GetEntityById(category.Id);
            var nameExists = await _categoryRepository.CheckIfCategoryExists(category.Name);

            if (register == null)
            {
                if (nameExists)
                {
                    SendNotification("Este nome já existe. Tente outro nome.", NotificationType.Warning);
                    return View();
                }

                category.Id = Guid.NewGuid();
                await _categoryRepository.AddEntity(category);
                await _context.SaveChangesAsync();
                SendNotification("Categoria criada.", NotificationType.Success);
            }

            if (!category.Equals(register) && register != null)
            {
                await _categoryRepository.UpdateEntity(category);
                await _context.SaveChangesAsync();
                SendNotification("Categoria atualizada.", NotificationType.Success);
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
                await _context.SaveChangesAsync();
                SendNotification("Categoria removida.", NotificationType.Success);
            }
            else
            {
                SendNotification("Não é possível obter a categoria.", NotificationType.Error);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
