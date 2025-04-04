using Ambev.DeveloperEvaluation.Application.Sales.CloseSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleCommandTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly CloseSaleHandler _closeSaleHandler;

    public CreateSaleCommandTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _userRepository = Substitute.For<IUserRepository>();
        _mediator = Substitute.For<IMediator>();
        _closeSaleHandler = new CloseSaleHandler(_saleRepository, _userRepository, _mediator);
    }

    [Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
    public void CreateSaleCommand_ValidData_ReturnsSuccessResponse()
    {
        // Given
        var command = new CreateSaleCommand
        {
            Branch = "Branch1",
            ProductId = Guid.NewGuid(),
            Quantity = 10,
            CustomerId = Guid.NewGuid()
        };

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact(DisplayName = "Given invalid sale data When creating sale Then returns validation errors")]
    public void CreateSaleCommand_InvalidData_ReturnsValidationErrors()
    {
        // Given
        var command = new CreateSaleCommand();

        // When
        var validationResult = command.Validate();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Given valid sale data When closing sale Then returns success response")]
    public async Task CloseSaleHandler_ValidData_ReturnsSuccessResponse()
    {
        var sale = SaleTestData.GenerateValidSale(2);
        var saleItem = SaleTestData.GenerateValidSale(2).Items.First();

        sale.AddItem(saleItem);

        var user = new User
        {
            Id = sale.UserId,
            Username = "testuser",
            Email = "testuser@example.com"
        };

        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(sale);
        _userRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(user);

        var command = new CloseSaleCommand(sale.Id);

        // When
        var result = await _closeSaleHandler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.SaleNumber.Should().Be(sale.Id);
        await _saleRepository.Received(1).UpdateAsync(sale, Arg.Any<CancellationToken>());
        await _mediator.Received(1).Publish(Arg.Any<SalesCloseEvent>(), Arg.Any<CancellationToken>());
    }

    [Fact(DisplayName = "Given invalid sale data When closing sale Then throws exception")]
    public async Task CloseSaleHandler_InvalidData_ThrowsException()
    {
        // Given
        _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns((Sale)null);

        var command = new CloseSaleCommand(Guid.NewGuid() );

        // When
        Func<Task> act = async () => await _closeSaleHandler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact(DisplayName = "Given valid sale data When updating sale Then returns success response")]
    public void UpdateSaleCommand_ValidData_ReturnsSuccessResponse()
    {
        var products = new List<ProductQuantity>
        {
            new ProductQuantity { ProductId = Guid.NewGuid(), Quantity = 10 }
        };
        // Given
        var command = new UpdateSaleCommand(Guid.NewGuid(), products);

        // When
        var validationResult = command.validationResultDetail();

        // Then
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact(DisplayName = "Given invalid sale data When updating sale Then returns validation errors")]
    public void UpdateSaleCommand_InvalidData_ReturnsValidationErrors()
    {
        var products = new List<ProductQuantity>
        {
            new ProductQuantity { ProductId = Guid.NewGuid(), Quantity = 0 }
        };
        // Given
        var command = new UpdateSaleCommand(Guid.Empty, products);

        // When
        var validationResult = command.validationResultDetail();

        // Then
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().NotBeEmpty();
    }

}
