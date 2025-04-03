using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(sale => sale.SaleDate)
            .NotNull()
            .NotEmpty()
            .WithMessage("Sale Date must be informed.");

        RuleFor(sale => sale.UserId)
            .NotEmpty()
            .WithMessage("User Id must be informed.");

        RuleFor(sale => sale.Branch)
            .NotNull()
            .NotEmpty()
            .WithMessage("Branch must be informed.");
    }
}