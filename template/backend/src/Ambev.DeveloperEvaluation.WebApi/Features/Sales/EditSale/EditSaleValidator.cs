using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditSale;

public class EditSaleValidator : AbstractValidator<EditSaleRequest>
{
    public EditSaleValidator()
    {
        RuleFor(x => x.SaleId).NotEmpty();
        RuleForEach(x => x.Products).ChildRules(products =>
        {
            products.RuleFor(p => p.ProductId).NotEmpty();
            products.RuleFor(p => p.Quantity).InclusiveBetween(0, int.MaxValue);
        });
    }
}
