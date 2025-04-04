using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CloseSale;

public class CloseSaleValidator : AbstractValidator<CloseSaleCommand>
{
    public CloseSaleValidator()
    {
        RuleFor(o => o.SaleNumber).NotEmpty();
    }
}
