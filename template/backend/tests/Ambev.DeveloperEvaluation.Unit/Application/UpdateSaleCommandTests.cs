using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class UpdateSaleCommandTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly UpdateSaleHandler _handler;
    private readonly IMediator _mediator;

    public UpdateSaleCommandTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mediator = Substitute.For<IMediator>();
        _handler = new UpdateSaleHandler(_saleRepository, _productRepository, _mediator);
    }

    [Fact(DisplayName = "Add 10 products, verify discount, add another 10 products, verify discount")]
    public async Task AddProducts_VerifyDiscounts()
    {
        // Given
        var product1 = new Product { Id = Guid.NewGuid(), Price = 10 };
        var product2 = new Product { Id = Guid.NewGuid(), Price = 20 };
        var sale = new Sale(Guid.NewGuid(), "Branch1");

        _productRepository.GetByIdAsync(product1.Id, Arg.Any<CancellationToken>()).Returns(product1);
        _productRepository.GetByIdAsync(product2.Id, Arg.Any<CancellationToken>()).Returns(product2);
        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(sale);
        _productRepository.GetProductsByIdsAsync(Arg.Is<List<Guid>>(ids => ids.Contains(product1.Id)), Arg.Any<CancellationToken>()).Returns(new List<Product> { product1 });
        _productRepository.GetProductsByIdsAsync(Arg.Is<List<Guid>>(ids => ids.Contains(product2.Id)), Arg.Any<CancellationToken>()).Returns(new List<Product> { product2 });

        var command1 = new UpdateSaleCommand(sale.Id, new List<ProductQuantity> { new ProductQuantity { ProductId = product1.Id, Quantity = 10 } });
        var command2 = new UpdateSaleCommand(sale.Id, new List<ProductQuantity> { new ProductQuantity { ProductId = product2.Id, Quantity = 10 } });

        // When
        await _handler.Handle(command1, CancellationToken.None);
        sale.CalculateSale();
        var discount1 = sale.TotalDiscount;

        await _handler.Handle(command2, CancellationToken.None);
        sale.CalculateSale();
        var discount2 = sale.TotalDiscount;

        // Then
        discount1.Should().BeGreaterThan(0);
        discount2.Should().BeGreaterThan(discount1);
    }
}
