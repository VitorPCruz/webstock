using WebStock.Models.Entities;

namespace WebStock.Models;

public class Report : Entity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public DateTime Moment { get; set; }
    public Operation Operation { get; set; }
    public int OperationQuantity { get; set; }
    public int BeforeOperation { get; set; }
    public int AfterOperation { get; set; }
    
    public Report() { }

    public Report(Product product, Operation operation, int operationQuantity, int beforeOperation)
    {
        ProductId = product.Id;
        ProductName = product.Name; 
        Moment = DateTime.Now;
        Operation = operation;
        OperationQuantity = operationQuantity;
        BeforeOperation = beforeOperation;
        AfterOperation = product.Quantity;
        ProductCode = product.ProductCode;
    }
}