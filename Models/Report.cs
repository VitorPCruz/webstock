using WebStock.Models.Entities;

namespace WebStock.Models;

public class Report : Entity
{
    public Report() { }
    public Report(Product product, Operation operation)
    {
        Product = product;
        ProductId = product.Id;
        ProductCode = product.ProductCode;
        ProductQuantity = product.Quantity;
        Operation = operation;
        Moment = DateTime.Now;
    }

    public Guid ProductId { get; set; }
    public string ProductCode { get; set; }
    public int ProductQuantity { get; set; }
    public Operation Operation { get; set; }
    public DateTime Moment { get; set; }
    public Product Product { get; set; }
}
