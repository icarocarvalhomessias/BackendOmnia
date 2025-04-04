using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales;

public class GetSalesHandler : IRequestHandler<GetSalesQuery, List<GetSalesResult>>
{
    private readonly ISaleRepository _salesRepository;

    public GetSalesHandler(ISaleRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<List<GetSalesResult>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var salesList = await _salesRepository.GetSales(request.Page, request.PageSize, request.Order, cancellationToken);

        if (salesList is null || !salesList.Sales.Any())
        {
            throw new KeyNotFoundException("No sales found");
        }

        return salesList.Select(s => new GetSalesResult
        {
            SalesNumber = s.Id,
            Customer = s.User.Username,
            SaleDate = s.SaleDate,
            TotalAmount = s.TotalAmount,
            Branch = s.Branch,
            Products = s.Items.Select(i => new ProductDetail
            {
                ProductName = i.Product.Title,
                Quantity = i.Quantity,
                UnitPrice = i.Product.Price,
                Discount = s.GetDiscutount(i),
                TotalAmount = i.TotalAmount
            }).ToList()
        }).ToList();


    }
}
