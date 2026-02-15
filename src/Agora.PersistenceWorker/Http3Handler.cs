using System.Net;

namespace Agora.PersistenceWorker;

public class Http3Handler : DelegatingHandler
{
    public Http3Handler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Version = HttpVersion.Version30;
        request.VersionPolicy = HttpVersionPolicy.RequestVersionExact;
        return base.SendAsync(request, cancellationToken);
    }
}