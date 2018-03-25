using RequestObfuscator.Api.Tampering;
using System;
using RequestObfuscator.Enums;

namespace RequestObfuscator.Api.Obfuscation
{
    public interface IApiObfuscatorBuilder<TRequest> where TRequest : class
    {
        IApiObfuscatorBuilder<TRequest> WithPath(Func<string, bool> isMetPathTransformer);
        IApiObfuscatorBuilder<TRequest> WithQuery(Func<string, bool> isMetPathTransformer);
        IApiObfuscatorBuilder<TRequest> WithFragment(string fragment);
        IApiObfuscatorBuilder<TRequest> MethodType(HttpMethodEnum method);
        IApiObfuscatorBuilder<TRequest> RequestContentType(RequestContentTypeEnum contentType);
        IRequestTampererBuilder<TRequest> BeginTamper(Action<TRequest> tamperBodyFunc = null);
    }
}
