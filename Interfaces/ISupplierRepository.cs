using WebStock.Models;

namespace WebStock.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<bool> CheckDocument(string document);
}