using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
        RuleFor(x => x.Page)
            .NotEmpty()
            .When(x => x.Page.HasValue)
            .WithMessage("Page size is required when page is provided");
    }
}
