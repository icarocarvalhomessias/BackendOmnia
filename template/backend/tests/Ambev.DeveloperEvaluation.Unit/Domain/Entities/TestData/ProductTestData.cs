using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Price, f => f.Random.Decimal(1, 1000))
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
        .RuleFor(p => p.Image, f => f.Image.PicsumUrl())
        .RuleFor(p => p.IsAvailable, f => f.Random.Bool())
        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
        .RuleFor(p => p.Rating, f => new Rating
        {
            Rate = f.Random.Decimal(1, 5),
            Count = f.Random.Number(1, 1000)
        });


    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }
}