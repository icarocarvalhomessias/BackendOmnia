using Ambev.DeveloperEvaluation.Application.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

public class GetProductsResponse
{
    public IEnumerable<ProductsResult> Products { get; set; } = new List<ProductsResult>();

    public GetProductsResponse(IEnumerable<ProductsResult?> products)
    {
        Products = products.Any() ? products : new List<ProductsResult>();
    }
}

