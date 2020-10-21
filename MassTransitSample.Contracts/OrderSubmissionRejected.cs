using System;

namespace MassTransitSample.Contracts
{
    public interface OrderSubmissionRejected
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }
        string CustomerNumber { get; }
        string Reason { get; }

    }
}
