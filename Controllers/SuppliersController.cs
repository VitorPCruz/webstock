using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    [Authorize]
    public class SuppliersController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ApplicationDbContext context, ISupplierRepository repository)
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
            var supplier = await _supplierRepository.GetEntityById(id);

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
            {
                TempData["notification"] = Notification
                    .SendNotification("Please, check the fields.", NotificationType.Error);

                return RedirectToAction(nameof(Register));
            }

            var register = await _supplierRepository.GetEntityById(supplier.Id);

            if (register == null)
            {
                supplier.Id = Guid.NewGuid();
                _supplierRepository.AddEntity(supplier);

                TempData["notification"] = Notification.SendNotification("Supplier registered.");
                await _context.SaveChangesAsync();
            }
            
            if (register != null && !supplier.Equals(register))
            {
                _supplierRepository.UpdateEntity(supplier);

                TempData["notification"] = Notification.SendNotification("Supplier updated.");
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
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                TempData["notification"] = Notification.SendNotification("Supplier removed.");
            }
            else 
            {
                TempData["notification"] = Notification.SendNotification("Not is possible get the supplier.", 
                    NotificationType.Error);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
