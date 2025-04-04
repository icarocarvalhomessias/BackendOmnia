using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSaleById;

public class GetSaleByIdRequest
{
    public Guid Id { get; set; }

    public GetSaleByIdRequest(Guid id)
    {
        Id = id;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new GetSaleByIdValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
