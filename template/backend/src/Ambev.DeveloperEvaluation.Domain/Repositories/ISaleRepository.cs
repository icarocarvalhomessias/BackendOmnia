using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<SalesList> GetSales(int page, int pageSize, string order, CancellationToken cancellationToken);

    Task AddAsync(Sale sale, CancellationToken cancellationToken);
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken);

    Task AddSaleItem(SaleItem saleItem, CancellationToken cancellationToken);
    Task AddSaleItem(IEnumerable<SaleItem> saleItems, CancellationToken cancellationToken);
    Task UpdateSaleItem(IEnumerable<SaleItem> saleItems, CancellationToken cancellationToken);
    Task DeleteAsync(Sale sale, CancellationToken cancellationToken);

}
