using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CloseSale;

public class CloseSaleCommand : IRequest<CloseSaleResult>
{
    public Guid SaleNumber { get; set; }

    public CloseSaleCommand(Guid saleNumber)
    {
        SaleNumber = saleNumber;
    }

    public ValidationResultDetail Validate()
    {
        var validator = new CloseSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
