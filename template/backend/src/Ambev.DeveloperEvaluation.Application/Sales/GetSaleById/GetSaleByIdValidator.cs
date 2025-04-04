using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdValidator : AbstractValidator<GetSaleByIdQuery>
{
    public GetSaleByIdValidator()
    {
        RuleFor(sale => sale.Id).NotEmpty();
    }
}
