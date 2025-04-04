using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="productRepository">The product repository</param>
    /// <param name="mediator">The mediator instance</param>
    public UpdateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mediator = mediator;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    /// <param name="request">The UpdateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        // Validate the request
        ValidateRequest(request);

        // Retrieve the sale by ID
        var sale = await GetSaleByIdAsync(request.SaleId, cancellationToken);

        // Retrieve the products by their IDs
        var products = await GetProductsByIdsAsync(request.Products.Select(p => p.ProductId).ToList(), cancellationToken);

        // Create dictionaries for quick lookup
        var saleItemsByProductId = sale.Items.ToDictionary(x => x.ProductId);
        var productsById = products.ToDictionary(x => x.Id);
        var addSaleItemList = new List<SaleItem>();
        var updateSaleItemList = new List<SaleItem>();

        foreach (var productQuantity in request.Products)
        {
            // Update the sale items
            var (updateType, produtUpdate) = await UpdateSaleItemsAsync(sale, productQuantity, saleItemsByProductId, productsById, cancellationToken);

            switch (updateType)
            {
                case UpdateSaleItemTypes.Add:
                    addSaleItemList.Add(produtUpdate);
                    break;
                case UpdateSaleItemTypes.Update:
                    updateSaleItemList.Add(produtUpdate);
                    break;
                case UpdateSaleItemTypes.Remove:
                    updateSaleItemList.Add(produtUpdate);
                    break;
            }
        }

        if (addSaleItemList.Any()) await _saleRepository.AddSaleItem(addSaleItemList, cancellationToken);

        if (updateSaleItemList.Any()) await _saleRepository.UpdateSaleItem(updateSaleItemList, cancellationToken);

        // Recalculate the sale total amount
        sale.CalculateSale();

        // Update the sale in the repository
        await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Publish an event for the modified sale
        await _mediator.Publish(new SaleModifiedEvent(sale.Id, sale.TotalAmount), cancellationToken);

        // Return the result
        return new UpdateSaleResult
        {
            TotalAmount = sale.TotalAmount,
            SaleId = sale.Id,
        };
    }

    /// <summary>
    /// Validates the UpdateSaleCommand request
    /// </summary>
    /// <param name="request">The UpdateSale command</param>
    private void ValidateRequest(UpdateSaleCommand request)
    {
        var validationResult = request.validationResultDetail();
        if (validationResult == null || !validationResult.IsValid)
        {
            throw new InvalidOperationException(string.Join(", ", validationResult.Errors));
        }
    }

    /// <summary>
    /// Retrieves the sale by ID
    /// </summary>
    /// <param name="saleId">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale entity</returns>
    private async Task<Sale> GetSaleByIdAsync(Guid saleId, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(saleId, cancellationToken);
        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {saleId} not found");
        }
        return sale;
    }

    /// <summary>
    /// Retrieves the products by their IDs
    /// </summary>
    /// <param name="productIds">The list of product IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The list of products</returns>
    private async Task<IEnumerable<Product>> GetProductsByIdsAsync(List<Guid> productIds, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetProductsByIdsAsync(productIds, cancellationToken);
        if (productIds.Count != products.Count())
        {
            var notFoundProducts = productIds.Except(products.Select(p => p.Id)).ToList();
            throw new InvalidOperationException("Products not found: " + string.Join(", ", notFoundProducts));
        }
        return products;
    }

    /// <summary>
    /// Updates the sale items based on the product quantity
    /// </summary>
    /// <param name="sale">The sale entity</param>
    /// <param name="productQuantity">The product quantity</param>
    /// <param name="saleItemsByProductId">Dictionary of sale items by product ID</param>
    /// <param name="productsById">Dictionary of products by ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The update type and sale item</returns>
    private async Task<(UpdateSaleItemTypes, SaleItem)> UpdateSaleItemsAsync(Sale sale, ProductQuantity productQuantity, Dictionary<Guid, SaleItem> saleItemsByProductId, Dictionary<Guid, Product> productsById, CancellationToken cancellationToken)
    {
        if (!saleItemsByProductId.TryGetValue(productQuantity.ProductId, out var item))
        {
            // Add new item to the sale
            var saleItem = new SaleItem(productsById[productQuantity.ProductId], productQuantity.Quantity, sale.Id);
            sale.AddItem(saleItem);
            return (UpdateSaleItemTypes.Add, saleItem);
        }

        if (productQuantity.Quantity == 0)
        {
            // Remove the existing item
            sale.RemoveItem(item);
            await _mediator.Publish(new ItemRemovedEvent(sale.Id, item.Id), cancellationToken);
            return (UpdateSaleItemTypes.Remove, item);
        }

        // Update the quantity and re-add the item
        sale.RemoveItem(item);
        item.Quantity = productQuantity.Quantity;
        sale.AddItem(item);
        return (UpdateSaleItemTypes.Update, item);
    }

    /// <summary>
    /// Enum for update sale item types
    /// </summary>
    private enum UpdateSaleItemTypes
    {
        Add,
        Update,
        Remove
    }
}
