namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsResult
{
    public IEnumerable<ProductsResult> Products { get; set; } = new List<ProductsResult>();

    public GetProductsResult(IEnumerable<ProductsResult?> products)
    {
        Products = products.Any() ? products : new List<ProductsResult>();
    }
}

public class ProductsResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public string Image { get; set; } = string.Empty;

    public int Count { get; set; }

}

