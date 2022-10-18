using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Supplier> _repository;


        public SupplierController(IRepository<Supplier> repository, ApplicationDbContext context)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _repository.GetAllEntities());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await _repository.GetEntityById(id);

            if (supplier == null) return NotFound();

            return View(supplier);
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var register = await _repository.GetEntityById(id);

            if (register == null) return View();

            return View(await _repository.GetEntityById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Supplier supplier)
        {
            var register = await _repository.GetEntityById(supplier.Id);
            
            if (!ModelState.IsValid) return View(supplier);

            if (register == null)
            {
                supplier.Id = Guid.NewGuid();

                _repository.AddEntity(supplier);
                await _context.SaveChangesAsync();
            }
            else
            { 
                _repository.UpdateEntity(supplier);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var supplier = await _repository.GetEntityById(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _repository.DeleteEntityById(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
