using Grpc.Core;

namespace Agora.Simulator;

public sealed class OrderSubscriber : IObserver<Order>
{
    private readonly IDisposable _unsubscriber;
    private readonly IServerStreamWriter<Order> _responseStream;

    public OrderSubscriber(OrderGenerator orderGenerator, IServerStreamWriter<Order> responseStream)
    {
        _unsubscriber = orderGenerator.Subscribe(this);
        _responseStream = responseStream;
    }
    
    public void Unsubscribe()
    {
        _unsubscriber.Dispose();
    }
    
    public void OnCompleted()
    {
        //Do nothing
    }

    public void OnError(Exception error)
    {
        //Do nothing
    }

    public void OnNext(Order value)
    {
        _responseStream.WriteAsync(value);
    }
}