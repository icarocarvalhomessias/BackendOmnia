using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public CreateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mediator = mediator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new InvalidOperationException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var sale = new Sale(request.CustomerId, request.Branch);

        await _saleRepository.AddAsync(sale, cancellationToken);

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
        {
            throw new InvalidOperationException("Product not found.");
        }

        var saleItem = new SaleItem(product, request.Quantity, sale.Id);

        await _saleRepository.AddSaleItem(saleItem, cancellationToken);

        sale.AddItem(saleItem);

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleCreatedEvent(sale.Id), cancellationToken);

        return new CreateSaleResult
        {
            TotalAmount = sale.TotalAmount,
            SaleId = sale.Id
        };
    }
}
