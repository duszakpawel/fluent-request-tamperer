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
        IApiObfuscatorBuilder<TRequest> When<TRequest>() where TRequest : class;
        IApiObfuscatorBuilder<object> When();
        IApiObfuscatorBuilder<TRequest> When<TRequest>(Func<TRequest, bool> condition) where TRequest : class;
        IEnumerable<Action<Session>> Build();
    }
}
