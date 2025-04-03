using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item in a sale transaction.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets or sets the product ID associated with the sale item.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the sale ID associated with the sale item.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the sale item.
    /// Must be a positive integer.
    /// </summary>
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the unit price of the product in the sale item.
    /// Must be a positive value.
    /// </summary>
    public decimal UnitPrice { get; set; } = 0;

    /// <summary>
    /// Gets or sets the total amount for the sale item.
    /// Calculated as UnitPrice * Quantity.
    /// </summary>
    public decimal TotalAmount { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether the sale item is cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;

    /// <summary>
    /// Gets or sets the sale associated with the sale item.
    /// </summary>
    public Sale Sale { get; set; }

    /// <summary>
    /// Gets or sets the product associated with the sale item.
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Initializes a new instance of the SaleItem class.
    /// </summary>
    public SaleItem() { }

    /// <summary>
    /// Initializes a new instance of the SaleItem class with the specified product, quantity, and sale ID.
    /// </summary>
    /// <param name="product">The product associated with the sale item.</param>
    /// <param name="quantity">The quantity of the product in the sale item.</param>
    /// <param name="saleId">The sale ID associated with the sale item.</param>
    public SaleItem(Product product, int quantity, Guid saleId)
    {
        ProductId = product.Id;
        Product = product;
        UnitPrice = product.Price;
        SaleId = saleId;
        Quantity = quantity;
        CalculateTotalAmount();
    }

    public void SetProduct(Product product, int quantity)
    {
        ProductId = product.Id;
        Product = product;
        UnitPrice = product.Price;
        Quantity = quantity;

        CalculateTotalAmount();
    }

    /// <summary>
    /// Calculates the total amount for the sale item.
    /// </summary>
    /// <returns>The total amount for the sale item.</returns>
    public decimal CalculateTotalAmount()
    {
        TotalAmount = (UnitPrice * Quantity);
        return TotalAmount;
    }

    /// <summary>
    /// Changes the quantity of the product in the sale item and recalculates the total amount.
    /// </summary>
    /// <param name="quantity">The new quantity of the product in the sale item.</param>
    public void ChangeQuantity(int quantity)
    {
        Quantity = quantity;
        if (quantity == 0)
        {
            IsCancelled = true;
        }

        CalculateTotalAmount();
    }

    /// <summary>
    /// Cancels the sale item.
    /// </summary>
    public void Cancel()
    {
        IsCancelled = true;
    }

    /// <summary>
    /// Performs validation of the sale item entity using the SaleItemValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}

