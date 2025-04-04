using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale if found, null otherwise</returns>
    public async Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Sales
            .Include(s => s.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of sales with optional ordering
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="order">The ordering string (e.g., "Id asc")</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of sales</returns>
    public async Task<SalesList> GetSales(int page, int pageSize, string order, CancellationToken cancellationToken)
    {
        var query = _context.Sales.AsQueryable();

        // Apply ordering
        if (!string.IsNullOrEmpty(order))
        {
            query = ApplyOrdering(query, order);
        }

        // Apply pagination
        query = query.Skip((page - 1) * pageSize).Take(pageSize);

        var sales = await query
            .Include(u => u.User)
            .Include(s => s.Items)
                .ThenInclude(i => i.Product)
            .ToListAsync(cancellationToken);

        var salesList = new SalesList();
        salesList.AddSales(sales);

        return salesList;
    }

    /// <summary>
    /// Applies ordering to the sales query
    /// </summary>
    /// <param name="query">The sales query</param>
    /// <param name="order">The ordering string (e.g., "Id asc")</param>
    /// <returns>The ordered sales query</returns>
    private IQueryable<Sale> ApplyOrdering(IQueryable<Sale> query, string order)
    {
        var orderParams = order.Split(' ');
        var propertyName = orderParams[0];
        var direction = orderParams.Length > 1 ? orderParams[1] : "asc";

        if (propertyName.Equals("Id", StringComparison.OrdinalIgnoreCase))
        {
            query = direction.ToLower() == "asc"
                ? query.OrderBy(s => s.Id)
                : query.OrderByDescending(s => s.Id);
        }
        else
        {
            query = direction.ToLower() == "asc"
                ? query.OrderBy(e => EF.Property<object>(e, propertyName))
                : query.OrderByDescending(e => EF.Property<object>(e, propertyName));
        }

        return query;
    }

    /// <summary>
    /// Adds a new sale to the database
    /// </summary>
    /// <param name="sale">The sale to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds a new sale item to the database
    /// </summary>
    /// <param name="saleItem">The sale item to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task AddSaleItem(SaleItem saleItem, CancellationToken cancellationToken)
    {
        await _context.SaleItems.AddAsync(saleItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Adds multiple sale items to the database
    /// </summary>
    /// <param name="saleItems">The sale items to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task AddSaleItem(IEnumerable<SaleItem> saleItems, CancellationToken cancellationToken)
    {
        await _context.SaleItems.AddRangeAsync(saleItems, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    /// <param name="sale">The sale to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates multiple sale items in the database
    /// </summary>
    /// <param name="saleItems">The sale items to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task UpdateSaleItem(IEnumerable<SaleItem> saleItems, CancellationToken cancellationToken)
    {
        _context.SaleItems.UpdateRange(saleItems);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Cancels a sale in the database
    /// </summary>
    /// <param name="sale">The sale to cancel</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task DeleteAsync(Sale sale, CancellationToken cancellationToken)
    {
        sale.Cancel();
        await UpdateAsync(sale, cancellationToken);
    }
}
