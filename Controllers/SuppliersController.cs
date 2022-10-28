using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStock.Data;
using WebStock.Interfaces;
using WebStock.Models;
using WebStock.Utils;

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

            if (supplier == null) return NotFound();

            return View(supplier);
        }

        public async Task<IActionResult> Register(Guid id)
        {
            var register = await _supplierRepository.GetEntityById(id);

            if (register == null) return View();

            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                SendNotification("Por favor, verifique os campos novamente.", NotificationType.Error);
                return RedirectToAction(nameof(Register));
            }

            var message = ValidateDocumentAndSupplierType(supplier);

            if (message.Length > 0)
            {
                SendNotification(message, NotificationType.Error);
                return RedirectToAction(nameof(Register));
            }

            var register = await _supplierRepository.GetEntityById(supplier.Id);

            if (register == null)
            {
                if (await _supplierRepository.CheckDocument(supplier.Document))
                {
                    SendNotification("Este CPF/CNPJ já está cadastrado.", NotificationType.Warning);
                    return RedirectToAction(nameof(Register));
                }

                supplier.Id = Guid.NewGuid();
                await _supplierRepository.AddEntity(supplier);
                await _context.SaveChangesAsync();
                
                SendNotification("Fornecedor Registrado.", NotificationType.Success);
            }

            if (register != null && !supplier.Equals(register))
            {
                await _supplierRepository.UpdateEntity(supplier);
                await _context.SaveChangesAsync();
                
                SendNotification("Fornecedor atualizado.", NotificationType.Success);
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
            var supplier = await _supplierRepository.GetEntityById(id);
            if (supplier != null)
            {
                _supplierRepository.DeleteEntityById(supplier.Id);
                await _context.SaveChangesAsync();
                SendNotification("Fornecedor removido.");
            }
            else
            {
                SendNotification("Não é possível obter o Fornecedor.", NotificationType.Error);
            }
            return RedirectToAction(nameof(Index));
        }

        public string ValidateDocumentAndSupplierType(Supplier supplier)
        {
            if (supplier.SupplierType == SupplierType.PhysicalPerson && !IdentifierValidator.CPFIsValid(supplier.Document))
            {
                return "CPF inváldio. Tente novamente.";
            }
            else if (supplier.SupplierType == SupplierType.LegalPerson && !IdentifierValidator.CNPJIsValid(supplier.Document))
            {
                return "CNPJ inváldio. Tente novamente.";
            }
            else
            {
                return "";
            }
        }
    }
}