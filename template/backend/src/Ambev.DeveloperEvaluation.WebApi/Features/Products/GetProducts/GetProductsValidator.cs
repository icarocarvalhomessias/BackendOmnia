using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts
{
    public class GetProductsValidator : AbstractValidator<GetProductsRequest>
    {
        public GetProductsValidator()
        {
            RuleFor(o => o.Page).GreaterThan(0);
            RuleFor(o => o.PageSize).GreaterThan(0);
            RuleFor(o => o.OrderBy).NotEmpty();
        }
    }
}
