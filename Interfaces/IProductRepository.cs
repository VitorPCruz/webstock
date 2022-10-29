using WebStock.Models;

namespace WebStock.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task RemoveProduct(Product product);
}
