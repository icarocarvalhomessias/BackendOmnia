using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}
