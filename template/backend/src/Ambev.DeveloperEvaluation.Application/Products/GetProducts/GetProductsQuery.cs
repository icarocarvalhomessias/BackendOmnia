using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsQuery : IRequest<GetProductsResult>
{
    public int? Page { get; }
    public int? PageSize { get; }
    public string? Order { get; }

    public GetProductsQuery()
    {

    }

    public GetProductsQuery(int? page, int? pageSize, string? order)
    {
        Page = page;
        PageSize = pageSize;
        Order = order;
    }
}
