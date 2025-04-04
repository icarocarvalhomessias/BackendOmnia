using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesQuery : IRequest<List<GetSalesResult>>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string Order { get; set; }

    public GetSalesQuery(int page, int pageSize, string order)
    {
        Page = page;
        PageSize = pageSize;
        Order = order;
    }

    public GetSalesQuery() { }
}
