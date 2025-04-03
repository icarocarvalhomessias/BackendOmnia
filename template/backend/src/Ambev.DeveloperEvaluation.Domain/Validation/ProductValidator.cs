using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Title cannot be longer than 50 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(product => product.Description)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

        RuleFor(product => product.Category)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Category must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Category cannot be longer than 50 characters.");

        RuleFor(product => product.Image)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Image must be at least 3 characters long.")
            .MaximumLength(500).WithMessage("Image cannot be longer than 500 characters.");

        RuleFor(product => product.Rating.Rate)
            .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5.");

        RuleFor(product => product.Rating.Count)
            .GreaterThanOrEqualTo(0).WithMessage("Count must be greater than or equal to 0.");

        RuleFor(product => product.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Created at must be less than or equal to current date.");

        RuleFor(product => product.UpdatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Updated at must be less than or equal to current date.");
    }
}