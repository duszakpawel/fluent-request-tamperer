using RequestObfuscator.Deserializers;
using System;
using System.Collections.Generic;
using RequestObfuscator.Enums;
using RequestObfuscator.Api.Parameters;

namespace RequestObfuscator.Api
{
    public interface IApiSharedState<TRequest> : IApiSharedState
    {
        Action<TRequest> TamperFunc { get; set; }
        Func<TRequest, bool> WhenCondition { get; set; }
    }

    public interface IApiSharedState
    {
        IResponseSerializer ResponseSerializer { get; set; }
        RequestContentTypeEnum RequestContentType { get; set; }
        Type RequestType { get; set; }
        List<Parameter> WhenConditions { get; set; }
        List<Parameter> TamperRules { get; set; }
        Func<string, bool> StrWhenCondition { get; set; }

        Action<string> StrTamperFunc { get; set; }
    }
}
