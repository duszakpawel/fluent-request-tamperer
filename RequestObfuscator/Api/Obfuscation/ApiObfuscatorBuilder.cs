using RequestObfuscator.Api.Tampering;
using RequestObfuscator.Enums;
using System;
using Fiddler;
using RequestObfuscator.Deserializers;
using RequestObfuscator.Api.Parameters.Parameters;
using RequestObfuscator.Api.Parameters.RequestParameters;

namespace RequestObfuscator.Api.Obfuscation
{
    public class ApiObfuscatorBuilder<TRequest> : IApiMethodDefinition, IApiObfuscatorBuilder<TRequest> where TRequest : class
    {
        private RequestTampererBuilder<TRequest> _requestTampererBuilder;
        private IApiSharedState<TRequest> _sharedState;

        public ApiObfuscatorBuilder(IApiSharedState<TRequest> state)
        {
            _sharedState = state;
        }

        public IRequestTampererBuilder<TRequest> BeginTamper(Action<TRequest> tamperFunc)
        {
            _sharedState.ResponseSerializer = _sharedState.RequestContentType == RequestContentTypeEnum.Json
                                                                               ? new JsonResponseSerializer() as IResponseSerializer
                                                                               : new XmlResponseSerializer()  as IResponseSerializer;

            _sharedState.TamperFunc = tamperFunc;

            _requestTampererBuilder = new RequestTampererBuilder<TRequest>(_sharedState);

            return _requestTampererBuilder;
        }

        public void BeforeRequest(Session session)
        {
            _requestTampererBuilder?.Build().BeforeRequest(session);
        }

        public IApiObfuscatorBuilder<TRequest> MethodType(HttpMethodEnum method)
        {
            _sharedState.WhenConditions.Add(new MethodTypeParameter(method));

            return this;
        }

        public IApiObfuscatorBuilder<TRequest> WithPath(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new PathSegmentParameter(isMetPathTransformer));

            return this;
        }

        public IApiObfuscatorBuilder<TRequest> WithQuery(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new QuerySegmentParameter(isMetPathTransformer));

            return this;
        }

        public IApiObfuscatorBuilder<TRequest> WithFragment(string fragment)
        {
            _sharedState.WhenConditions.Add(new FragmentSegmentParameter(fragment));

            return this;
        }

        public IApiObfuscatorBuilder<TRequest> RequestContentType(RequestContentTypeEnum contentType)
        {
            _sharedState.RequestContentType = contentType;

            return this;
        }
    }
}
