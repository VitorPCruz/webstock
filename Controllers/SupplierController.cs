using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.Suppliers.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        public async Task<IActionResult> Register(Guid? id)
        {
            if (!id.HasValue) 
                return View();

            return View(await _context.Suppliers.AsNoTracking().FirstAsync(s => s.Id == id)); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Supplier supplier)
        {
            var checkExist = await _context.Suppliers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == supplier.Id);
            
            if (!ModelState.IsValid)
                return View(supplier);

            if (checkExist == null)
            {
                supplier.Id = Guid.NewGuid();
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.Suppliers.Update(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            return View(nameof(Register), supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Supplier supplier)
        {
            if (!ModelState.IsValid)
                return View(supplier);
           
            _context.Update(supplier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            var supplier = await _context.Suppliers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
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
