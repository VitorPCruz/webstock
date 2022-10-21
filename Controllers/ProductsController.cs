using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;

namespace WebStock.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Report> _reportRepository;

        public ProductsController(ApplicationDbContext context, 
                                  IRepository<Product> productRepository,
                                  IRepository<Category> categoryRepository,
                                  IRepository<Report> reportRepository)
        {
            _context = context;
            _productRepository = productRepository;
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
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Active == true), "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(x => x.Active == true), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            product.Id = Guid.NewGuid();

            _productRepository.AddEntity(product);
            _reportRepository.AddEntity(new Report(product, Operation.Added, product.Quantity, 0));
            
            await _context.SaveChangesAsync();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            DefineAvailableEntities();
            var product = await _productRepository.GetEntityById(id);

            if (product == null) return NotFound();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (!ModelState.IsValid) return View(product);

            var oldProduct = await _productRepository.GetEntityById(id);

            Operation operation;
            var difference = 0;

            if (product.Quantity > oldProduct.Quantity)
            {
                operation = Operation.Added;
                difference = product.Quantity - oldProduct.Quantity;
            }
            else
            {
                operation = Operation.Removed;
                difference = oldProduct.Quantity - product.Quantity;
            }

            var report = new Report(product, operation, difference, oldProduct.Quantity);

            _productRepository.UpdateEntity(product);
            _reportRepository.AddEntity(report);

            await _context.SaveChangesAsync();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
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
            return RedirectToAction(nameof(Index));
        }

        private void DefineAvailableEntities()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.Active == true), "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers.Where(x => x.Active == true), "Id", "Name");
        }
    }
}
