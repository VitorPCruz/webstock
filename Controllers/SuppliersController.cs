using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Supplier> _supplierRepository;

        public SuppliersController(ApplicationDbContext context, IRepository<Supplier> repository)
        {
            _context = context;
            _supplierRepository = repository;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _supplierRepository.GetAllEntities());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supplier == null)
                return NotFound();

            return View(supplier);
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var register = await _supplierRepository.GetEntityById(id);
            if (register == null)
                return View();

            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Supplier supplier)
        {
            if (!ModelState.IsValid)
                return View(supplier);

            var register = await _supplierRepository.GetEntityById(supplier.Id);

            if (register == null)
            {
                supplier.Id = Guid.NewGuid();
                _supplierRepository.AddEntity(supplier);
                await _context.SaveChangesAsync();
            }
            else
            {
                _supplierRepository.UpdateEntity(supplier);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var register = await _supplierRepository.GetEntityById(id);
            
            if (register == null)
                return NotFound();

            return View(register);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Suppliers'  is null.");
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
