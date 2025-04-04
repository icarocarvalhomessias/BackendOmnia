using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSaleById;

public class GetSaleByIdValidator : AbstractValidator<GetSaleByIdRequest>
{
    public GetSaleByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
