using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Handler for processing GetProductsQuery requests.
/// </summary>
public class GetProductsHandler : IRequestHandler<GetProductsQuery, GetProductsResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GetProductsHandler class.
    /// </summary>
    /// <param name="productRepository">The product repository.</param>
    public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductsQuery request.
    /// </summary>
    /// <param name="request">The GetProducts query.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of GetProductsResult containing the product details.</returns>
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Product> products;

        if (request.Page.HasValue && request.PageSize.HasValue)
        {
            products = await _productRepository.GetProducts(request.Page.Value, request.PageSize.Value, request.Order);
        }
        else
        {
            products = await _productRepository.GetProducts();
        }

        if (products == null)
        {
            throw new KeyNotFoundException("No products found");
        }

        var productsResults = _mapper.Map<IEnumerable<ProductsResult>>(products);

        return new GetProductsResult(productsResults);
    }
}
