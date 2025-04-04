using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, GetSalesResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;

    public GetSaleByIdHandler(ISaleRepository saleRepository, IUserRepository userRepository)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
    }

    public async Task<GetSalesResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.Id} not found");
        }

        var customer = await _userRepository.GetByIdAsync(sale.UserId, cancellationToken);

        return new GetSalesResult
        {
            SalesNumber = sale.Id,
            SaleDate = sale.SaleDate,
            Customer = customer?.Username ?? "Unknown",
            TotalAmount = sale.TotalAmount,
            Branch = sale.Branch,
            Products = sale.Items.Select(i => new ProductDetail
            {
                Id = i.ProductId,
                ProductName = i.Product.Title,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = sale.GetDiscutount(i),
                TotalAmount = i.Quantity * i.UnitPrice - sale.GetDiscutount(i)
            }).ToList(),
            IsCancelled = sale.IsCancelled,
            Status = sale.GetSaleStatus
        };
    }
}
