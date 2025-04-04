using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditSale;

public class EditSaleRequest 
{
    public Guid SaleId { get; set; }
    public List<ProductQuantity> Products { get; set; }

    public EditSaleRequest(Guid saleId, List<ProductQuantity> products)
    {
        Products = products;
        SaleId = saleId;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new EditSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}
