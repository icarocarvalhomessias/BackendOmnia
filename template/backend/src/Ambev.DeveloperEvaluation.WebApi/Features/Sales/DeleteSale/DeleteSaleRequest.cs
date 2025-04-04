using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteSale;

public class DeleteSaleRequest
{
    public Guid Id { get; set; }

    public DeleteSaleRequest(Guid id)
    {
        Id = id;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new DeleteSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
