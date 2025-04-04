using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent : Event
{
    public Guid SaleId { get; set; }

    public SaleCreatedEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}

public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} created at {notification.Timestamp}");
        return Task.CompletedTask;
    }
}
