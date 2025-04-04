using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{

    [Theory]
    [InlineData(2, 0)]
    [InlineData(4, 0.1)]
    [InlineData(10, 0.2)]
    [InlineData(20, 0.2)]
    public void CalculateDiscount_ShouldFindCorrectDiscount(int quantity, decimal expectedDiscount)
    {
        var sale = SaleTestData.GenerateValidSale(quantity);

        // Act
        var discount = sale.GetDiscount(quantity);

        // Assert
        discount.Should().Be(expectedDiscount);
    }

    [Theory]
    [InlineData(2, 100, 200)]
    [InlineData(4, 100, 360)] // 10% discount
    [InlineData(10, 100, 800)] // 20% discount
    [InlineData(20, 100, 1600)] // 20% discount
    public void CalculateSale_ShouldCalculateCorrectTotalAmount(int quantity, decimal unitPrice, decimal expectedTotalAmount)
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(quantity, unitPrice);


        // Assert
        sale.TotalAmount.Should().Be(expectedTotalAmount);
    }


    [Theory]
    [InlineData(4, 100, 40)] // 10% discount on 4 items with unit price 100
    [InlineData(10, 100, 200)] // 20% discount on 10 items with unit price 100
    public void GetDiscutount_ShouldReturnCorrectDiscountValue(int quantity, decimal unitPrice, decimal expectedDiscount)
    {
        // Arrange
        var saleItem = new SaleItem
        {
            Quantity = quantity,
            UnitPrice = unitPrice
        };
        var sale = new Sale();

        // Act
        var discount = sale.GetDiscutount(saleItem);

        // Assert
        discount.Should().Be(expectedDiscount);
    }

    [Fact(DisplayName = "Add item to sale")]
    public void Sale_WhenAddItem_ShouldAddItemToSale()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale(2);
        var saleItem = SaleTestData.GenerateValidSale(2).Items.First();
        // Act
        sale.AddItem(saleItem);
        // Assert
        sale.Items.Should().Contain(saleItem);
    }

}