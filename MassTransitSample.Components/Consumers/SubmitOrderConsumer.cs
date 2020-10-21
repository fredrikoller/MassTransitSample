using MassTransitSample.Contracts;
using MassTransit;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace MassTransitSample.Components.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly ILogger<SubmitOrderConsumer> _logger;

        public SubmitOrderConsumer(ILogger<SubmitOrderConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            _logger.LogDebug("SubmitOrderConsumer: {CustomerNumber}", context.Message.CustomerNumber);

            if (context.Message.CustomerNumber.Contains("Test"))
            {
                await context.RespondAsync<OrderSubmissionRejected>(new
                {
                    context.Message.OrderId,
                    InVar.Timestamp,
                    context.Message.CustomerNumber,
                    Reason = $"Test Customer cannot submit orders: {context.Message.CustomerNumber}"
                });

                return;
            }

            await context.RespondAsync<OrderSubmissionAccepted>(new
            {
                InVar.Timestamp,
                context.Message.OrderId,
                context.Message.CustomerNumber
            });
        }
    }
}