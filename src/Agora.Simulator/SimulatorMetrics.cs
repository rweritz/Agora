using System.Diagnostics.Metrics;

namespace Agora.Simulator;

public class MarketSimulatorMetrics
{
    public MarketSimulatorMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create("Agora.Simulator");
        ObservableOrdersCountLastMinute = meter.CreateObservableGauge<int>("orders.count.lastminute", GetOrderCountLastMinute);
        OrdersCountTotal = meter.CreateCounter<int>("orders.count.total");
    }
    
    public ObservableGauge<int> ObservableOrdersCountLastMinute { get; private set; }

    public int OrdersCountLastMinute { get; set; }

    private int GetOrderCountLastMinute()
    {
        return OrdersCountLastMinute;
    }
    
    public Counter<int> OrdersCountTotal { get; private set; }
}