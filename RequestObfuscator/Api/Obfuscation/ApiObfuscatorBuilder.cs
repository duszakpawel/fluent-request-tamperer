using RequestObfuscator.Api.Tampering;
using RequestObfuscator.Enums;
using System;
using Fiddler;
using RequestObfuscator.Deserializers;
using RequestObfuscator.Api.Parameters.Parameters;
using RequestObfuscator.Api.Parameters.RequestParameters;

namespace RequestObfuscator.Api.Obfuscation
{
    internal class ApiObfuscatorBuilder<TRequest> : ApiObfuscatorBuilder, IApiMethodDefinition, IApiObfuscatorBuilder<TRequest> where TRequest : class
    {
        private RequestTampererBuilder<TRequest> _requestTampererBuilder;
        private IApiSharedState<TRequest> _sharedState;

        internal ApiObfuscatorBuilder(IApiSharedState<TRequest> state)
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

        void IApiMethodDefinition.BeforeRequest(Session session)
        {
            _requestTampererBuilder?.Build().BeforeRequest(session);
        }

        IApiObfuscatorBuilder<TRequest> IApiObfuscatorBuilder<TRequest>.MethodType(HttpMethodEnum method)
        {
            _sharedState.WhenConditions.Add(new MethodTypeParameter(method));

            return this;
        }

        IApiObfuscatorBuilder<TRequest> IApiObfuscatorBuilder<TRequest>.WithPath(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new PathSegmentParameter(isMetPathTransformer));

            return this;
        }

        IApiObfuscatorBuilder<TRequest> IApiObfuscatorBuilder<TRequest>.WithQuery(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new QuerySegmentParameter(isMetPathTransformer));

            return this;
        }

        IApiObfuscatorBuilder<TRequest> IApiObfuscatorBuilder<TRequest>.WithFragment(string fragment)
        {
            _sharedState.WhenConditions.Add(new FragmentSegmentParameter(fragment));

            return this;
        }

        IApiObfuscatorBuilder<TRequest> IApiObfuscatorBuilder<TRequest>.RequestContentType(RequestContentTypeEnum contentType)
        {
            _sharedState.RequestContentType = contentType;

            return this;
        }
    }

    internal class ApiObfuscatorBuilder : IApiMethodDefinition, IApiObfuscatorBuilder
    {
        private RequestTampererBuilder _requestTampererBuilder;
        private IApiSharedState<string> _sharedState;

        internal ApiObfuscatorBuilder()
        {

        }

        internal ApiObfuscatorBuilder(IApiSharedState<string> state)
        {
            _sharedState = state;
        }

        public void BeforeRequest(Session session)
        {
            _requestTampererBuilder?.Build().BeforeRequest(session);
        }

        public virtual IApiObfuscatorBuilder MethodType(HttpMethodEnum method)
        {
            _sharedState.WhenConditions.Add(new MethodTypeParameter(method));

            return this;
        }

        public IApiObfuscatorBuilder WithPath(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new PathSegmentParameter(isMetPathTransformer));

            return this;
        }

        public IApiObfuscatorBuilder WithQuery(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new QuerySegmentParameter(isMetPathTransformer));

            return this;
        }

        public IApiObfuscatorBuilder WithFragment(string fragment)
        {
            _sharedState.WhenConditions.Add(new FragmentSegmentParameter(fragment));

            return this;
        }

        public IApiObfuscatorBuilder RequestContentType(RequestContentTypeEnum contentType)
        {
            _sharedState.RequestContentType = contentType;

            return this;
        }

        public IRequestTampererBuilder BeginTamper()
        {
            _requestTampererBuilder = new RequestTampererBuilder(_sharedState);

            return _requestTampererBuilder;
        }

        public IRequestTampererBuilder BeginTamper(Action<string> tamperBodyFunc = null)
        {
            _sharedState.StrTamperFunc = tamperBodyFunc;

            _requestTampererBuilder = new RequestTampererBuilder(_sharedState);

            return _requestTampererBuilder;
        }

        public IRequestTamperer AbortRequest()
        {
            _sharedState.AbortRequest = true;

            _requestTampererBuilder = new RequestTampererBuilder(_sharedState);

            return new RequestTamperer(_sharedState);
        }
    }
}
