using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleValidator()
    {
        RuleFor(sale => sale.Branch).NotEmpty().MaximumLength(50);
        RuleFor(sale => sale.ProductId).NotEmpty();
        RuleFor(sale => sale.Quantity).GreaterThan(0);
        RuleFor(sale => sale.CustomerId).NotEmpty();
    }
}
