using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        RuleFor(saleItem => saleItem.TotalAmount)
            .GreaterThan(0).WithMessage("Total Amount must be greater than 0.");
    }
}