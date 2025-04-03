using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, must not exceed 100 characters
    /// - Description: Required, must not exceed 500 characters
    /// - Price: Must be greater than zero
    /// </remarks>
    public CreateProductValidator()
    {
        RuleFor(product => product.Title).NotEmpty().MaximumLength(100);
        RuleFor(product => product.Description).NotEmpty().MaximumLength(500);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Category).NotEmpty();
        RuleFor(product => product.Rating).GreaterThanOrEqualTo(0);
        RuleFor(product => product.Count).GreaterThanOrEqualTo(0);
    }
}