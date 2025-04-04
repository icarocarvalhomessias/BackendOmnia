using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSales;

public class GetSalesValidator : AbstractValidator<GetSalesRequest>
{
    public GetSalesValidator()
    {
        RuleFor(o => o.Page).GreaterThan(0);
        RuleFor(o => o.PageSize).GreaterThan(0);
        RuleFor(o => o.OrderBy).NotEmpty();
    }
}
