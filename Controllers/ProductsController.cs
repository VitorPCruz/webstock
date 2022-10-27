using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository<Report> _reportRepository;

        public ProductsController(ApplicationDbContext context, IProductRepository productRepository, 
            ISupplierRepository supplierRepository, ICategoryRepository categoryRepository, 
            IRepository<Report> reportRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _categoryRepository = categoryRepository;
            _reportRepository = reportRepository;
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
            GetCategoriesEnabled();
            GetSuppliersEnabled();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)

        {
            if (!ModelState.IsValid)
            {
                SendNotification("Please, check the fields.", NotificationType.Warning);
                GetCategoriesEnabled();
                GetSuppliersEnabled();
                return View(product);
            }

            if (product.Quantity < 1)
            {
                SendNotification("Can not possible register product with 0 products.", NotificationType.Warning);
                GetCategoriesEnabled();
                GetSuppliersEnabled();
                return View(product);
            }

            product.Id = Guid.NewGuid();

            await _productRepository.AddEntity(product);
            await _reportRepository.AddEntity(new Report(product, Operation.Added, product.Quantity, 0));
            await _context.SaveChangesAsync();

            GetCategoriesEnabled();
            GetSuppliersEnabled();
            SendNotification("New product created.", NotificationType.Success);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productRepository.GetEntityById(id);

            if (product == null) 
                return NotFound();

            GetCategoriesEnabled();
            GetSuppliersEnabled();
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
                SendNotification("Please, check the fields.", NotificationType.Warning);
                return View(product);
            }

            var oldProduct = await _productRepository.GetEntityById(id);
            var difference = 0;
            string message;
            Operation operation;

            if (product.Quantity > oldProduct.Quantity)
            {
                operation = Operation.Added;
                difference = product.Quantity - oldProduct.Quantity;
                message = $"Added x{difference} of {product.Name}.";

                SendNotification(message, NotificationType.Success);
            }
            else if (product.Quantity < oldProduct.Quantity)
            {
                operation = Operation.Removed;
                difference = oldProduct.Quantity - product.Quantity;
                message = $"Removed x{difference} of {product.Name}.";

                SendNotification(message, NotificationType.Success);
            }
            else
            {
                operation = Operation.Updated;
                difference = 0;
            }

            var report = new Report(product, operation, difference, oldProduct.Quantity);

            await _productRepository.UpdateEntity(product);

            if (difference > 0)
                await _reportRepository.AddEntity(report);

            if (!product.Equals(oldProduct))
                SendNotification("Product updated.", NotificationType.Success);

            await _context.SaveChangesAsync();

            GetCategoriesEnabled();
            GetSuppliersEnabled();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _productRepository.GetEntityById(id);
            string message;

            if (product != null)
            {
                if (product.Quantity > 0)
                {
                    message = $"Not is possible delete the product {product.Name} because it has x{product.Quantity} on stock. Remove them before delete the product.";
                    SendNotification(message, NotificationType.Warning);
                }
                else
                { 
                    _productRepository.DeleteEntityById(product.Id);
                    await _context.SaveChangesAsync();
                    SendNotification("Product removed.", NotificationType.Success);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private void GetCategoriesEnabled()
        {
            ViewData["CategoryId"] = _categoryRepository.GetCategoriesEnabled();
        }

        private void GetSuppliersEnabled()
        {
            ViewData["SupplierId"] = _supplierRepository.GetSuppliersEnabled();
        }
    }
}
