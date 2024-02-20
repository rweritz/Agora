namespace Agora.Simulator;

public class OrderGenerator(ILogger<OrderGenerator> logger, MarketSimulatorMetrics metrics)
    : BackgroundService, IObservable<Order>
{
    private readonly List<IObserver<Order>> _observers = new();

    public IDisposable Subscribe(IObserver<Order> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new Unsubscriber<Order>(_observers, observer);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var compareMinute = DateTime.UtcNow.Minute;
        var ordersCountLastMinute = 0;
        
        var random = new Random(compareMinute);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            for (var i = 1; i < random.Next(1, 20); i++)
            {
                var order = new Order
                {
                    Instrument = random.Next(1, 100) % 2 == 0 ? "XYZ" : "ABC",
                    Type = "Bid",
                    Price = random.Next(10, 50),
                    Volume = random.Next(1, 10)
                };
            
                _observers.ForEach(o => o.OnNext(order));
            
                logger.OrderGenerated(order);
                metrics.OrdersCountTotal.Add(1);

                var currentMinute = DateTime.UtcNow.Minute;
                if (currentMinute != compareMinute)
                {
                    metrics.OrdersCountLastMinute = ordersCountLastMinute;
                    ordersCountLastMinute = 0;
                    compareMinute = currentMinute;
                }
                else
                {
                    ordersCountLastMinute++;
                }
            }
            
            await Task.Delay(TimeSpan.FromSeconds(random.Next(1, 3)), stoppingToken);
        }
    }
}

internal static partial class Log
{
    [LoggerMessage(LogLevel.Information, "Order generated {order}")]
    public static partial void OrderGenerated(this ILogger<OrderGenerator> logger, [LogProperties] Order order);
}