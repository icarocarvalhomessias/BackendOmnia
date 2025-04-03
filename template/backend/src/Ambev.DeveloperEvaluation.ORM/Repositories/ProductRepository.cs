using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new product in the database
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    /// <summary>
    /// Deletes a product from the database
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FindAsync(id, cancellationToken);
        if (product == null)
        {
            return false;
        }
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves a product by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FindAsync(id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a product by its name
    /// </summary>
    /// <param name="name">The name of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Title == name, cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of products with optional ordering
    /// </summary>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The number of items per page</param>
    /// <param name="order">The ordering string (e.g., "Title asc")</param>
    /// <returns>A paginated list of products</returns>
    public async Task<IEnumerable<Product>> GetProducts(int page, int pageSize, string? order)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(order))
        {
            query = query.OrderBy(p => EF.Property<object>(p, order));
        }
        return await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all products from the database
    /// </summary>
    /// <returns>A list of products</returns>
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    /// <summary>
    /// Retrieves products by their unique identifiers
    /// </summary>
    /// <param name="productIds">The list of product IDs to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of products</returns>
    public async Task<IEnumerable<Product>> GetProductsByIdsAsync(List<Guid> productIds, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => productIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing product in the database
    /// </summary>
    /// <param name="product">The product to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
}
