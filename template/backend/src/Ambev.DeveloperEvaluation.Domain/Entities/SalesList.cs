using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a list of sales transactions in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SalesList
{
    private readonly List<Sale> _sales;

    /// <summary>
    /// Gets the collection of sales.
    /// </summary>
    public IReadOnlyCollection<Sale> Sales => _sales;

    public SalesList()
    {
        _sales = new List<Sale>();
    }

    /// <summary>
    /// Adds a sale to the list.
    /// </summary>
    /// <param name="sale">The sale to add.</param>
    public void AddSale(Sale sale)
    {
        if (!sale.Validate().IsValid) throw new DomainException("Sale is not valid.");
        _sales.Add(sale);
    }

    /// <summary>
    /// Adds multiple sales to the list.
    /// </summary>
    /// <param name="sales">The sales to add.</param>
    public void AddSales(IEnumerable<Sale> sales)
    {
        foreach (var sale in sales)
        {
            AddSale(sale);
        }
    }

    /// <summary>
    /// Removes a sale from the list.
    /// </summary>
    /// <param name="sale">The sale to remove.</param>
    public void RemoveSale(Sale sale)
    {
        if (!_sales.Contains(sale)) throw new DomainException("Sale not found.");
        _sales.Remove(sale);
    }

    /// <summary>
    /// Updates a sale in the list.
    /// </summary>
    /// <param name="sale">The sale to update.</param>
    public void UpdateSale(Sale sale)
    {
        var existingSale = _sales.FirstOrDefault(s => s.Id == sale.Id);
        if (existingSale == null) throw new DomainException("Sale not found.");

        _sales.Remove(existingSale);
        _sales.Add(sale);
    }

    /// <summary>
    /// Filters sales by date range.
    /// </summary>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>A list of sales within the specified date range.</returns>
    public IEnumerable<Sale> FilterByDateRange(DateTime startDate, DateTime endDate)
    {
        return _sales.Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate);
    }

    /// <summary>
    /// Filters sales by status.
    /// </summary>
    /// <param name="status">The status to filter by.</param>
    /// <returns>A list of sales with the specified status.</returns>
    public IEnumerable<Sale> FilterByStatus(SaleStatus status)
    {
        return _sales.Where(s => s.SaleStatus == status);
    }

    /// <summary>
    /// Filters sales by user ID.
    /// </summary>
    /// <param name="userId">The user ID to filter by.</param>
    /// <returns>A list of sales associated with the specified user ID.</returns>
    public IEnumerable<Sale> FilterByUserId(Guid userId)
    {
        return _sales.Where(s => s.UserId == userId);
    }

    /// <summary>
    /// Searches sales by branch.
    /// </summary>
    /// <param name="branch">The branch to search by.</param>
    /// <returns>A list of sales that occurred in the specified branch.</returns>
    public IEnumerable<Sale> SearchByBranch(string branch)
    {
        return _sales.Where(s => s.Branch.Equals(branch, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Calculates the total amount of all sales.
    /// </summary>
    /// <returns>The total amount of all sales.</returns>
    public decimal CalculateTotalAmount()
    {
        return _sales.Sum(s => s.TotalAmount);
    }

    /// <summary>
    /// Calculates the total discount of all sales.
    /// </summary>
    /// <returns>The total discount of all sales.</returns>
    public decimal CalculateTotalDiscount()
    {
        return _sales.Sum(s => s.TotalDiscount);
    }

    /// <summary>
    /// Calculates the average sale amount.
    /// </summary>
    /// <returns>The average sale amount.</returns>
    public decimal CalculateAverageSaleAmount()
    {
        return _sales.Average(s => s.TotalAmount);
    }

    /// <summary>
    /// Gets the total count of sales.
    /// </summary>
    /// <returns>The total count of sales.</returns>
    public int GetTotalSalesCount()
    {
        return _sales.Count;
    }

    /// <summary>
    /// Gets the count of sales by status.
    /// </summary>
    /// <param name="status">The status to count by.</param>
    /// <returns>The count of sales with the specified status.</returns>
    public int GetSalesCountByStatus(SaleStatus status)
    {
        return _sales.Count(s => s.SaleStatus == status);
    }

    /// <summary>
    /// Gets the count of sales by branch.
    /// </summary>
    /// <param name="branch">The branch to count by.</param>
    /// <returns>The count of sales in the specified branch.</returns>
    public int GetSalesCountByBranch(string branch)
    {
        return _sales.Count(s => s.Branch.Equals(branch, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Validates all sales in the list.
    /// </summary>
    /// <returns>A list of validation results for each sale.</returns>
    public IEnumerable<ValidationResultDetail> ValidateAllSales()
    {
        return _sales.Select(s => s.Validate());
    }

    /// <summary>
    /// Projects each element of a sequence into a new form.
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the projection function.</typeparam>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>A list of the projected form.</returns>
    public List<TResult> Select<TResult>(Func<Sale, TResult> selector)
    {
        return _sales.Select(selector).ToList();
    }
}
