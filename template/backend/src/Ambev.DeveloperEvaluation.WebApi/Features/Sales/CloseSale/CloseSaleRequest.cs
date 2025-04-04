using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseSale;

public class CloseSaleRequest
{
    public Guid SaleNumber { get; set; }

    public CloseSaleRequest(Guid saleNumber)
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
