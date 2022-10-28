using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                SendNotification("Por favor, verifique os campos novamente.", NotificationType.Warning);
                GetCategoriesEnabled();
                GetSuppliersEnabled();
                return View(product);
            }

            if (product.Quantity < 1)
            {
                SendNotification("Não é possível registrar um produto com 0 de quantidade.", NotificationType.Warning);
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
            SendNotification("Produto criado.", NotificationType.Success);

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
                SendNotification("Por favor, verifique os campos novamente.", NotificationType.Warning);
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
                message = $"Adicionado x{difference} de {product.Name}.";

                SendNotification(message, NotificationType.Success);
            }
            else if (product.Quantity < oldProduct.Quantity)
            {
                operation = Operation.Removed;
                difference = oldProduct.Quantity - product.Quantity;
                message = $"Removido x{difference} de {product.Name}.";

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
                SendNotification("Produto atualizado.", NotificationType.Success);

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
                    message = $"Não é possível remover o produto {product.Name} pois existem x{product.Quantity} cadastrados no estoque. Remova-os antes de prosseguir.";
                    SendNotification(message, NotificationType.Warning);
                }
                else
                { 
                    _productRepository.DeleteEntityById(product.Id);
                    await _context.SaveChangesAsync();
                    SendNotification("Produto removido.", NotificationType.Success);
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
