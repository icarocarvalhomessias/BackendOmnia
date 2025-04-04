using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddSaleItem;

public class AddSaleItemValidator : AbstractValidator<AddSaleItemRequest>
{
    public AddSaleItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.Branch).NotEmpty();
    }
}
