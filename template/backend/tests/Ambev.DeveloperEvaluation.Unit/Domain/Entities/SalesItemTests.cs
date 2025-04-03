using Ambev.DeveloperEvaluation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SalesItemTests
{

    [Fact]
    public void SaleItem_WhenCreateSaleItemWithProductAndQuantity_ShouldCreateSaleItem()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        // Act
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        // Assert
        Assert.Equal(product.Id, saleItem.ProductId);
        Assert.Equal(2, saleItem.Quantity);
        Assert.Equal(10, saleItem.UnitPrice);
        Assert.Equal(20, saleItem.TotalAmount);
    }

    [Fact(DisplayName = "Quantaty change, should also change total amount")]
    public void SaleItem_WhenChangeQuantity_ShouldChangeQuantityAndTotalAmount()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        // Act
        saleItem.ChangeQuantity(3);
        // Assert
        Assert.Equal(3, saleItem.Quantity);
        Assert.Equal(30, saleItem.TotalAmount);
    }

    [Fact(DisplayName = "Cancel sale item")]
    public void SaleItem_WhenCancelSaleItem_ShouldCancelSaleItem()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        // Act
        saleItem.Cancel();
        // Assert
        Assert.True(saleItem.IsCancelled);
    }

    [Fact(DisplayName = "Validate sale item")]
    public void SaleItem_WhenValidateSaleItem_ShouldReturnValid()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        // Act
        var result = saleItem.Validate();
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Validate sale item with invalid quantity")]
    public void SaleItem_WhenValidateSaleItemWithInvalidQuantity_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 0, Guid.NewGuid());
        // Act
        var result = saleItem.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Validate sale item with invalid unit price")]
    public void SaleItem_WhenValidateSaleItemWithInvalidUnitPrice_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 0
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        // Act
        var result = saleItem.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Validate sale item with invalid total amount")]
    public void SaleItem_WhenValidateSaleItemWithInvalidTotalAmount_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        saleItem.TotalAmount = 0;
        // Act
        var result = saleItem.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Validate sale item with invalid sale id")]
    public void SaleItem_WhenValidateSaleItemWithInvalidSaleId_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        saleItem.SaleId = Guid.Empty;
        // Act
        var result = saleItem.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Validate sale item with invalid product id")]
    public void SaleItem_WhenValidateSaleItemWithInvalidProductId_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = "Product 1",
            Price = 10
        };
        var saleItem = new SaleItem(product, 2, Guid.NewGuid());
        saleItem.ProductId = Guid.Empty;
        // Act
        var result = saleItem.Validate();
        // Assert
        Assert.False(result.IsValid);
    }
}
