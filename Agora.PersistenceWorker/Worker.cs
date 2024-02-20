using Agora.Simulator;
using Grpc.Net.Client;

namespace Agora.PersistenceWorker;

public class Worker(ILogger<Worker> logger, Market.MarketClient marketClient) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        var response = marketClient.SubscribeToOrders(new OrderSubscriptionRequest() { Instrument = "ABC" });
        
        while (await response.ResponseStream.MoveNext(stoppingToken))
        {
            var order = response.ResponseStream.Current;
            logger.OrderReceived(order);
        }
    }
}

internal static partial class Log
{
    [LoggerMessage(LogLevel.Information, "Order received {order}")]
    public static partial void OrderReceived(this ILogger<Worker> logger, [LogProperties] Order order);
}