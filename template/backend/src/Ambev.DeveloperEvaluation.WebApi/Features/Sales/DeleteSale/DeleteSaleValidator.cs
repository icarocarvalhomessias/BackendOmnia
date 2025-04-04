using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteSale;

public class DeleteSaleValidator : AbstractValidator<DeleteSaleRequest>
{
    public DeleteSaleValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
    }
}
