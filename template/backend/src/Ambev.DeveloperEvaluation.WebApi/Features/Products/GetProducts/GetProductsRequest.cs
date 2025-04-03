using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

public class GetProductsRequest
{
    public int? Page { get; }
    public int? PageSize { get; }
    public string? OrderBy { get; }

    public GetProductsRequest()
    {
        
    }

    public GetProductsRequest(int? page, int? pageSize, string? order)
    {
        Page = page;
        PageSize = pageSize;
        OrderBy = order;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new GetProductsValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}