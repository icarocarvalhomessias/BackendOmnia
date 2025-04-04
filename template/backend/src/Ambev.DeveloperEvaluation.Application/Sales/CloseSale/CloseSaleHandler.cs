using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CloseSale;

public class CloseSaleHandler  : IRequestHandler<CloseSaleCommand, CloseSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public CloseSaleHandler(ISaleRepository saleRepository, IUserRepository userRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<CloseSaleResult> Handle(CloseSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.SaleNumber, cancellationToken);

        if (sale == null)
        {
            throw new InvalidOperationException($"Sale with SaleNumber: {request.SaleNumber} not found.");
        }

        if (sale.IsCancelled)
        {
            throw new InvalidOperationException($"Sale with SaleNumber: {request.SaleNumber} is already cancelled.");
        }

        sale.Close();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        var user = await _userRepository.GetByIdAsync(sale.UserId, cancellationToken);

        await _mediator.Publish(new SalesCloseEvent(sale.Id, sale.TotalAmount, user.Email), cancellationToken);


        return new CloseSaleResult
        {
            SaleNumber = sale.Id,
            SaleDate = sale.SaleDate,
            Customer = user.Username,
            TotalAmount = sale.TotalAmount,
            Branch = sale.Branch,
            Products = sale.Items.Select(p => new ProductDetail
            {
                ProductName = p.Product.Title,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
                Discount = (sale.GetDiscutount(p)),
                TotalAmount = p.TotalAmount
            }).ToList(),
            IsCancelled = sale.IsCancelled
        };
    }

}
