using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public partial class SaleModifiedEvent : Event
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }

    public SaleModifiedEvent(Guid saleId, decimal totalAmount)
    {
        SaleId = saleId;
        TotalAmount = totalAmount;
    }
}

public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
{
    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} modified to Total Amount: {notification.TotalAmount} at {notification.Timestamp} ");
        return Task.CompletedTask;
    }
}
