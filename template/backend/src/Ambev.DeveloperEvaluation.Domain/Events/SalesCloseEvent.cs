using Ambev.DeveloperEvaluation.Domain.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SalesCloseEvent : Event
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }
    public string UserEmail { get; set; }

    public SalesCloseEvent(Guid saleId, decimal totalAmount, string userEmail)
    {
        SaleId = saleId;
        TotalAmount = totalAmount;
        UserEmail = userEmail;
    }
}

public class SalesCloseEventHander : INotificationHandler<SalesCloseEvent>
{
    public Task Handle(SalesCloseEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} closed at {notification.Timestamp} with Total Amount: {notification.TotalAmount}");
        return Task.CompletedTask;
    }
}
