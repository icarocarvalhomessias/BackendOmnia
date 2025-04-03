using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    [Fact]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        // Act
        var result = product.Validate();
        // Assert
        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Title is required")]
    public void Given_ProductWithEmptyTitle_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = string.Empty;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Title must be at least 3 characters long")]
    public void Given_ProductWithShortTitle_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = "a";
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Title cannot be longer than 50 characters")]
    public void Given_ProductWithLongTitle_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = new string('a', 51);
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Price must be greater than 0")]
    public void Given_ProductWithZeroPrice_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Price = 0;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Description is required")]
    public void Given_ProductWithEmptyDescription_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Description = string.Empty;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Description must be at least 3 characters long")]
    public void Given_ProductWithShortDescription_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Description = "a";
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Description cannot be longer than 500 characters")]
    public void Given_ProductWithLongDescription_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Description = new string('a', 501);
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Category is required")]
    public void Given_ProductWithEmptyCategory_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Category = string.Empty;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Category must be at least 3 characters long")]
    public void Given_ProductWithShortCategory_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Category = "a";
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Category cannot be longer than 50 characters")]
    public void Given_ProductWithLongCategory_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Category = new string('a', 51);
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Image is required")]
    public void Given_ProductWithEmptyImage_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Image = string.Empty;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Image must be at least 3 characters long")]
    public void Given_ProductWithShortImage_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Image = "a";
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Image cannot be longer than 500 characters")]
    public void Given_ProductWithLongImage_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Image = new string('a', 501);
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Rate must be between 0 and 5")]
    public void Given_ProductWithInvalidRate_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Rating.Rate = -1;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

    [Fact(DisplayName = "Count must be greater than or equal to 0")]
    public void Given_ProductWithNegativeCount_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Rating.Count = -1;
        // Act
        var result = product.Validate();
        // Assert
        Assert.False(result.IsValid);
    }

}