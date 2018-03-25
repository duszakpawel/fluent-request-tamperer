using System;
using System.Collections.Generic;
using RequestObfuscator.Api.Parameters;
using RequestObfuscator.Deserializers;
using RequestObfuscator.Enums;

namespace RequestObfuscator.Api
{
    public class ApiSharedState : IApiSharedState
    {
        public IResponseSerializer ResponseSerializer { get; set; }
        public Type RequestType { get; set; }
        public RequestContentTypeEnum RequestContentType { get; set; }
        public List<Parameter> WhenConditions { get; set; }
        public List<Parameter> TamperRules { get; set; }
        public Func<string, bool> StrWhenCondition { get; set; }
        public Action<string> StrTamperFunc { get; set; }

        public ApiSharedState() { }
        public ApiSharedState(ApiSharedState sharedState)
        {
            ResponseSerializer = sharedState.ResponseSerializer;
            RequestType = sharedState.RequestType;
            RequestContentType = sharedState.RequestContentType;
            WhenConditions = new List<Parameter>(sharedState.WhenConditions);
            TamperRules = new List<Parameter>(sharedState.TamperRules);
        }
    }

    public class ApiSharedState<TRequest> : ApiSharedState, IApiSharedState<TRequest> where TRequest : class
    {
        public Action<TRequest> TamperFunc { get; set; }
        public Func<TRequest, bool> WhenCondition { get; set; }

        public ApiSharedState()
        {
        }

        public ApiSharedState(ApiSharedState sharedState) : base(sharedState)
        {
        }

        public ApiSharedState(ApiSharedState<TRequest> sharedState) : this(sharedState as ApiSharedState)
        {
            WhenCondition = sharedState.WhenCondition;
            TamperFunc = sharedState.TamperFunc;
        }
    }
}
