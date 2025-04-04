using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseSale;

public class CloseSaleValidator : AbstractValidator<CloseSaleRequest>
{
    public CloseSaleValidator()
    {
        RuleFor(o => o.SaleNumber).NotEmpty();
    }
}
