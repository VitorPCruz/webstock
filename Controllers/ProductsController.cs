using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;
using WebStock.Repository;

namespace WebStock.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Report> _reportRepository;

        public ProductsController(ApplicationDbContext context, IRepository<Product> productRepository, IRepository<Report> reportRepository)
        {
            _context = context;
            _productRepository = productRepository;
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            product.Quantity = 0;
            if (!ModelState.IsValid) return View(product);

            product.Id = Guid.NewGuid();

            _productRepository.AddEntity(product);
            _reportRepository.AddEntity(new Report(product, Operation.Added));
            
            await _context.SaveChangesAsync();

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", product.SupplierId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
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
         
            _productRepository.UpdateEntity(product);
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

    }
}
