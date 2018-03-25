using RequestObfuscator.Api.Tampering;
using System;
using RequestObfuscator.Enums;

namespace RequestObfuscator.Api.Obfuscation
{
    public interface IApiObfuscatorBuilder
    {
        IApiObfuscatorBuilder WithPath(Func<string, bool> isMetPathTransformer);
        IApiObfuscatorBuilder WithQuery(Func<string, bool> isMetPathTransformer);
        IApiObfuscatorBuilder WithFragment(string fragment);
        IApiObfuscatorBuilder MethodType(HttpMethodEnum method);
        IApiObfuscatorBuilder RequestContentType(RequestContentTypeEnum contentType);
        IRequestTampererBuilder BeginTamper();
        IRequestTampererBuilder BeginTamper(Action<string> tamperBodyFunc = null);

    }
    public interface IApiObfuscatorBuilder<TRequest> where TRequest : class
    {
        IRequestTampererBuilder<TRequest> BeginTamper(Action<TRequest> tamperBodyFunc = null);
        IApiObfuscatorBuilder<TRequest> WithPath(Func<string, bool> isMetPathTransformer);
        IApiObfuscatorBuilder<TRequest> WithQuery(Func<string, bool> isMetPathTransformer);
        IApiObfuscatorBuilder<TRequest> WithFragment(string fragment);
        IApiObfuscatorBuilder<TRequest> MethodType(HttpMethodEnum method);
        IApiObfuscatorBuilder<TRequest> RequestContentType(RequestContentTypeEnum contentType);
    }
}
