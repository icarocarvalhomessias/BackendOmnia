namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesResult
{
    public Guid SalesNumber { get; set; }
    public DateTime SaleDate { get; set; }
    public string Customer { get; set; }
    public decimal TotalAmount { get; set; }
    public string Branch { get; set; }
    public List<ProductDetail> Products { get; set; }
    public bool IsCancelled { get; set; }

    public string Status { get; set; }

}

public class ProductDetail
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}

