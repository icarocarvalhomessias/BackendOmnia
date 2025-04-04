namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleResult
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }
    public List<ProductQuantity> UpdatedProducts { get; set; }
}
