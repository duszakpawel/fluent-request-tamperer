using RequestObfuscator.Enums;
using System;

namespace RequestObfuscator.Api.Tampering
{
    public interface IRequestTampererBuilder<TRequest> where TRequest : class
    {
        IRequestTampererBuilder<TRequest> TamperMethodType(HttpMethodEnum methodType);
        IRequestTampererBuilder<TRequest> TamperHost(string host);
        IRequestTampererBuilder<TRequest> TamperPort(int port);
        IRequestTampererBuilder<TRequest> TamperScheme(string scheme);
        IRequestTampererBuilder<TRequest> TamperHeader(string key, string value);
        IRequestTampererBuilder<TRequest> TamperPath(Func<string, string> pathTransformer);
        IRequestTampererBuilder<TRequest> TamperQuery(Func<string, string> pathTransformer);
        IRequestTampererBuilder<TRequest> TamperFragment(string fragment);
        IRequestTamperer Build();
    }

    public interface IRequestTampererBuilder
    {
        IRequestTampererBuilder TamperMethodType(HttpMethodEnum methodType);
        IRequestTampererBuilder TamperHost(string host);
        IRequestTampererBuilder TamperPort(int port);
        IRequestTampererBuilder TamperScheme(string scheme);
        IRequestTampererBuilder TamperHeader(string key, string value);
        IRequestTampererBuilder TamperPath(Func<string, string> pathTransformer);
        IRequestTampererBuilder TamperQuery(Func<string, string> pathTransformer);
        IRequestTampererBuilder TamperFragment(string fragment);
        IRequestTamperer Build();
    }
}