using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product in the system with details such as title, price, description, and availability.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Gets or sets the product title.
    /// Must not be null or empty.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product price.
    /// Must be a positive value.
    /// </summary>
    public decimal Price { get; set; } = 0;

    /// <summary>
    /// Gets or sets the product description.
    /// Provides detailed information about the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product category.
    /// Must not be null or empty.
    /// </summary>
    public string Category { get; set; } = string.Empty;


    /// <summary>
    /// Gets or sets the product image URL.
    /// Must be a valid URL format.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product rating.
    /// Includes the rate and count of ratings.
    /// </summary>
    public Rating Rating { get; set; } = new Rating();


    /// <summary>
    /// Gets a value indicating whether the product is available.
    /// </summary>
    public bool IsAvailable { get; protected set; } = false;

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; } = null;

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    public Product()
    {
        CreatedAt = DateTime.Now;
    }

    /// <summary>
    /// Activates the product.
    /// Changes the product's availability to true.
    /// </summary>
    public void Activate()
    {
        UpdatedAt = DateTime.Now;
        IsAvailable = true;
    }

    /// <summary>
    /// Deactivates the product.
    /// Changes the product's availability to false.
    /// </summary>
    public void Deactivate()
    {
        UpdatedAt = DateTime.Now;
        IsAvailable = false;
    }

    /// <summary>
    /// Performs validation of the product entity using the ProductValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

/// <summary>
/// Represents the rating of a product.
/// Includes the rate and count of ratings.
/// </summary>
public class Rating : BaseEntity
{
    /// <summary>
    /// Gets or sets the rate of the product.
    /// </summary>
    public decimal Rate { get; set; }

    /// <summary>
    /// Gets or sets the count of ratings.
    /// </summary>
    public int Count { get; set; }
}