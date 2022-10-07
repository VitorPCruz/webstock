using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebStock.Data;
using WebStock.Intefaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _repository;
        private readonly ApplicationDbContext _context;

        public CategoryController(IRepository<Category> repository, ApplicationDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await GetAll());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Id = Guid.NewGuid();
                await _repository.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            return View(await GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            return View(await GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<Category> GetById(Guid id)
        {
           return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task DeleteById(Guid id)
        {
            await _repository.Delete(id);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category entity)
        {
            await _repository.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
