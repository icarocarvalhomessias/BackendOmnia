using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public partial class ItemRemovedEvent : Event
{
    public Guid SaleId { get; set; }
    public Guid ItemId { get; set; }

    public ItemRemovedEvent(Guid saleId, Guid itemId)
    {
        SaleId = saleId;
        ItemId = itemId;
    }
}

public class ItemRemovedEventHandler : INotificationHandler<ItemRemovedEvent>
{
    public Task Handle(ItemRemovedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Item {notification.ItemId} removed from sale {notification.SaleId} at {notification.Timestamp}");
        return Task.CompletedTask;
    }
}

