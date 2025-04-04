using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSales;

public class GetSalesRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public string OrderBy { get; set; }


    public GetSalesRequest(int page, int pageSize, string orderBy)
    {
        Page = page;
        PageSize = pageSize;
        OrderBy = orderBy;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new GetSalesValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };

    }

}
