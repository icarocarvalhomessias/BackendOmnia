using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddSaleItem;

/// <summary>
/// Represents a request to add a new sale item in the system.
/// </summary>
public class AddSaleItemRequest
{
    /// <summary>
    /// Gets or sets the product ID associated with the sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the sale item.
    /// Must be a positive integer.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the branch where the sale occurred.
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Initializes a new instance of the AddSaletemRequest class with the specified product ID, quantity, and branch.
    /// </summary>
    /// <param name="productId">The product ID associated with the sale item.</param>
    /// <param name="quantity">The quantity of the product in the sale item.</param>
    /// <param name="branch">The branch where the sale occurred.</param>
    public AddSaleItemRequest(Guid productId, int quantity, string branch)
    {
        ProductId = productId;
        Quantity = quantity;
        Branch = branch;
    }

    /// <summary>
    /// Validates the AddSaletemRequest using the AddSaletemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail validationResultDetail()
    {
        var validator = new AddSaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
