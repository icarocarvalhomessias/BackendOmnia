using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleItemTestData
{
    private static readonly Faker<SaleItem> faker = new Faker<SaleItem>()
        .RuleFor(p => p.ProductId, f => Guid.NewGuid())
        .RuleFor(p => p.Quantity, f => f.Random.Number(1, 100))
        .RuleFor(p => p.UnitPrice, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.TotalAmount, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.IsCancelled, f => f.Random.Bool());

}