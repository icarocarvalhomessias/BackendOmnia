using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty();
        RuleForEach(x => x.Products).ChildRules(products =>
        {
            products.RuleFor(p => p.ProductId).NotEmpty();
            products.RuleFor(p => p.Quantity).InclusiveBetween(0, int.MaxValue);
        });
    }
}
