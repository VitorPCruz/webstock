using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Report> _reportRepository;

        public ProductsController(ApplicationDbContext context,
                                  IRepository<Product> productRepository,
                                  IRepository<Category> categoryRepository,
                                  IRepository<Report> reportRepository,
                                  IRepository<Supplier> supplierRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reportRepository = reportRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productRepository.GetAllEntities());
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var product = await _productRepository.GetEntityById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Active == true), "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(x => x.Active == true), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                TempData["notification"] = Notification.SendNotification("Please, check the fields.", NotificationType.Warning);
                return View(product);
            }

            product.Id = Guid.NewGuid();

            await _productRepository.AddEntity(product);
            await _reportRepository.AddEntity(new Report(product, Operation.Added, product.Quantity, 0));

            await _context.SaveChangesAsync();

            DefineAvailableEntities();

            TempData["notification"] = Notification.SendNotification("New product created.", NotificationType.Success);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productRepository.GetEntityById(id);

            if (product == null) 
                return NotFound();

            DefineAvailableEntities();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                TempData["notification "] = Notification.SendNotification(
                    "Please, check the fields.", NotificationType.Warning);

                return View(product);
            }

            var oldProduct = await _productRepository.GetEntityById(id);
            var difference = 0;
            Operation operation;

            if (product.Quantity > oldProduct.Quantity)
            {
                operation = Operation.Added;
                difference = product.Quantity - oldProduct.Quantity;

                TempData["notification"] = Notification.SendNotification(
                    $"Added x{difference} of {product.Name}.", NotificationType.Success);
            }
            else if (product.Quantity < oldProduct.Quantity)
            {
                operation = Operation.Removed;
                difference = oldProduct.Quantity - product.Quantity;

                TempData["notification"] = Notification.SendNotification(
                    $"Removed x{difference} of {product.Name}.", NotificationType.Success);
            }
            else
            {
                operation = Operation.Updated;
                difference = 0;
            }

            var report = new Report(product, operation, difference, oldProduct.Quantity);

            _productRepository.UpdateEntity(product);

            if (difference > 0)
                _reportRepository.AddEntity(report);

            if (!product.Equals(oldProduct))
                TempData["notification"] = Notification.SendNotification("Product updated.", NotificationType.Success);

            await _context.SaveChangesAsync();

            DefineAvailableEntities();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await _productRepository.GetEntityById(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productRepository.GetEntityById(id);

            if (product != null)
                _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            TempData["notification"] = Notification.SendNotification("Product removed.");
            return RedirectToAction(nameof(Index));
        }

        private void DefineAvailableEntities()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Active == true), "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(x => x.Active == true), "Id", "Name");
        }
    }
}
