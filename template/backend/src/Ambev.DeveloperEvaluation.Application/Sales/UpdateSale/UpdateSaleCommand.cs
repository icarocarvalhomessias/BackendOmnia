using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating an existing sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a sale,
/// including the sale ID and a list of products with their quantities.
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="UpdateSaleResult"/>.
/// 
/// The data provided in this command is validated using the
/// <see cref="UpdateSaleValidator"/> which extends
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly
/// populated and follow the required rules.
/// </remarks>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale to be updated.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the list of products with their quantities to be updated in the sale.
    /// </summary>
    public List<ProductQuantity> Products { get; set; }

    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommand class with the specified sale ID and products.
    /// </summary>
    /// <param name="saleId">The unique identifier of the sale to be updated.</param>
    /// <param name="products">The list of products with their quantities to be updated in the sale.</param>
    public UpdateSaleCommand(Guid saleId, List<ProductQuantity> products)
    {
        SaleId = saleId;
        Products = products;
    }

    /// <summary>
    /// Validates the UpdateSaleCommand using the UpdateSaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail validationResultDetail()
    {
        var validator = new UpdateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents a product with its quantity in a sale.
/// </summary>
public class ProductQuantity
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the sale.
    /// </summary>
    public int Quantity { get; set; }
}
