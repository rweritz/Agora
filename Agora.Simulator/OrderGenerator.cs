namespace Agora.Simulator;

public class OrderGenerator(ILogger<OrderGenerator> logger, MarketSimulatorMetrics metrics)
    : IObservable<Order>
{
    
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private readonly List<IObserver<Order>> _observers = new();

    private Task? _task;

    public void Start()
    {
        _task ??= Task.Factory.StartNew(GenerateOrder, _cancellationTokenSource.Token);
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
    
    private void GenerateOrder()
    {
        var compareMinute = DateTime.UtcNow.Minute;
        var ordersCountLastMinute = 0;
        
        var random = new Random(compareMinute);
        
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
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
            
            Thread.Sleep(TimeSpan.FromSeconds(random.Next(1, 3)));
        }
    }

    public IDisposable Subscribe(IObserver<Order> observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);

        return new Unsubscriber<Order>(_observers, observer);
    }
}

internal static partial class Log
{
    [LoggerMessage(LogLevel.Information, "Order generated {order}")]
    public static partial void OrderGenerated(this ILogger<OrderGenerator> logger, [LogProperties] Order order);
}