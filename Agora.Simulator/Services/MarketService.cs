using Grpc.Core;

namespace Agora.Simulator.Services;

public class MarketService(ILogger<MarketService> logger, IServiceProvider serviceProvider) : Market.MarketBase
{
    public override Task SubscribeToOrders(OrderSubscriptionRequest request, IServerStreamWriter<Order> responseStream, ServerCallContext context)
    {
        var orderGenerator = serviceProvider
            .GetServices<IHostedService>()
            .OfType<OrderGenerator>()
            .Single();
        
        var orderSubscriber = new OrderSubscriber(orderGenerator, responseStream);
        
        while (!context.CancellationToken.IsCancellationRequested)
        {

        }
        
        logger.SubscriptionCanceled(context);
        orderSubscriber.Unsubscribe();
        return Task.CompletedTask;
    }
    
}

internal static partial class Log
{
    [LoggerMessage(LogLevel.Information, "Subscription canceled")]
    public static partial void SubscriptionCanceled(this ILogger<MarketService> logger, [LogProperties] ServerCallContext context);
}