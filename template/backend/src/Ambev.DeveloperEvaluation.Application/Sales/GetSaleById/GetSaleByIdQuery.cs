using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

public class GetSaleByIdQuery : IRequest<GetSalesResult>  
{
    public Guid Id { get; set; }

    public GetSaleByIdQuery(Guid id)
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
