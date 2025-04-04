using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(saleItem => saleItem.ProductId)
            .NotNull()
            .NotEmpty()
            .WithMessage("Product Id must be informed.");
        RuleFor(saleItem => saleItem.SaleId)
            .NotEmpty().WithMessage("Sale Id must be informed.");
        RuleFor(saleItem => saleItem.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
            .LessThan(21).WithMessage("Quantity must be less or equal than 20.");
        RuleFor(saleItem => saleItem.UnitPrice)
            .GreaterThan(0).WithMessage("Unit Price must be greater than 0.");
    }
}