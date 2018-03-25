using Fiddler;
using RequestObfuscator.Api.Obfuscation;
using RequestObfuscator.Enums;
using System;
using System.Collections.Generic;

namespace RequestObfuscator.Api
{
    public interface IApiBuilder
    {
        IApiBuilder WithHost(string host);
        IApiBuilder WithPort(int port);
        IApiBuilder WithScheme(string scheme);
        IApiBuilder WithPath(Func<string, bool> isMetPathTransformer);
        IApiBuilder WithQuery(Func<string, bool> isMetPathTransformer);
        IApiBuilder WithFragment(string fragment);
        IApiBuilder TamperHeader(string key, string value);
        IApiBuilder RequestContentType(RequestContentTypeEnum contentType);
        IApiObfuscatorBuilder<TRequest> ForRequest<TRequest>() where TRequest : class;
        IApiObfuscatorBuilder ForRequest();
        IApiObfuscatorBuilder ForRequest(Func<string, bool> condition);
        IApiObfuscatorBuilder<TRequest> ForRequest<TRequest>(Func<TRequest, bool> condition) where TRequest : class;
        IEnumerable<Action<Session>> Build();
    }
}
