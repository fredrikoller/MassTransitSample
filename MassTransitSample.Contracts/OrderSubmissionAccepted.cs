using System;

namespace MassTransitSample.Contracts
{
    public interface OrderSubmissionAccepted
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }
        string CustomerNumber { get; }
    }
}
