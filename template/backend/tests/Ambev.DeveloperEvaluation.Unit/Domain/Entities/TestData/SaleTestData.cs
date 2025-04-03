using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleDate, f => f.Date.Past(1))
        .RuleFor(s => s.UserId, f => f.Random.Guid())
        .RuleFor(s => s.TotalAmount, f => f.Random.Decimal(1, 1000))
        .RuleFor(s => s.Branch, f => f.Company.CompanyName())
        .RuleFor(s => s.TotalDiscount, f => f.Random.Decimal(1, 100))
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool());

    private static readonly Faker<SaleItem> SaleItemfaker = new Faker<SaleItem>()
        .RuleFor(s => s.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleId, f => f.Random.Guid())
        .RuleFor(s => s.Quantity, f => f.Random.Number(1, 10))
        .RuleFor(s => s.UnitPrice, f => f.Random.Decimal(1, 1000));

    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000));

    public static Sale GenerateValidSale(int quantity, decimal unitPrice)
    {
        var sale = SaleFaker.Generate();
        var saleItem = SaleItemfaker.Generate();
        saleItem.SetProduct(GenerateValidProduct(unitPrice), quantity);
        sale.AddItem(saleItem);
        return sale;
    }

    private static Product GenerateValidProduct(decimal unitPrice)
    {
        var prodcut = ProductFaker.Generate();
        prodcut.Price = unitPrice;
        return prodcut;
    }

    public static Sale GenerateValidSale(int quantity)
    {
        var unitPrice = new Faker().Random.Decimal(1, 100);
        return GenerateValidSale(quantity, unitPrice);
    }
}
