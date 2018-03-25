using System;
using System.Collections.Generic;
using RequestObfuscator.Enums;
using RequestObfuscator.Deserializers;
using RequestObfuscator.Api.Obfuscation;
using Fiddler;
using System.Linq;
using RequestObfuscator.Api.Parameters;
using RequestObfuscator.Api.Parameters.Parameters;
using RequestObfuscator.Api.Parameters.RequestParameters;

namespace RequestObfuscator.Api
{
    public class ApiBuilder : IApiBuilder
    {
        private ApiSharedState _sharedState;
        private List<IApiMethodDefinition> _methodDefinitions;

        public ApiBuilder()
        {
            _sharedState = new ApiSharedState()
            {
                ResponseSerializer = new JsonResponseSerializer(),
                WhenConditions = new List<Parameter>(),
                TamperRules = new List<Parameter>(),
                RequestContentType = RequestContentTypeEnum.Json,
            };

            _methodDefinitions = new List<IApiMethodDefinition>();
        }

        public IApiBuilder RequestContentType(RequestContentTypeEnum contentType)
        {
            _sharedState.RequestContentType = contentType;

            return this;
        }

        public IEnumerable<Action<Session>> Build()
        {
            return _methodDefinitions.Select<IApiMethodDefinition, Action<Session>>(x => x.BeforeRequest);
        }

        public IApiBuilder WithScheme(string scheme)
        {
            _sharedState.WhenConditions.Add(new SchemeParameter(scheme));

            return this;
        }

        public IApiBuilder WithHost(string host)
        {
            _sharedState.WhenConditions.Add(new HostParameter(host));

            return this;
        }

        public IApiBuilder WithPort(int port)
        {
            _sharedState.WhenConditions.Add(new PortParameter(port));

            return this;
        }

        public IApiObfuscatorBuilder<TRequest> ForRequest<TRequest>() where TRequest : class
        {
            return ForRequest<TRequest>(condition: null);
        }

        public IApiObfuscatorBuilder<TRequest> ForRequest<TRequest>(Func<TRequest, bool> condition) where TRequest : class
        {
            var sharedStare = new ApiSharedState<TRequest>(_sharedState)
            {
                WhenCondition = condition,
                RequestType = typeof(TRequest)
            };

            IApiObfuscatorBuilder<TRequest> methodDefinition = new ApiObfuscatorBuilder<TRequest>(new ApiSharedState<TRequest>(sharedStare));
            _methodDefinitions.Add(methodDefinition as IApiMethodDefinition);

            return methodDefinition;
        }

        public IApiBuilder WithPath(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new PathSegmentParameter(isMetPathTransformer));

            return this;
        }

        public IApiBuilder WithQuery(Func<string, bool> isMetPathTransformer)
        {
            _sharedState.WhenConditions.Add(new QuerySegmentParameter(isMetPathTransformer));

            return this;
        }

        public IApiBuilder WithFragment(string fragment)
        {
            _sharedState.WhenConditions.Add(new FragmentSegmentParameter(fragment));

            return this;
        }

        public IApiBuilder TamperHeader(string key, string value)
        {
            _sharedState.WhenConditions.Add(new HeaderParameter(key, value));

            return this;
        }

        // Quick workaround for not providing the type of request body
        public IApiObfuscatorBuilder ForRequest()
        {
            var sharedStare = new ApiSharedState<string>(_sharedState);

            IApiObfuscatorBuilder methodDefinition = new ApiObfuscatorBuilder(new ApiSharedState<string>(sharedStare) as IApiSharedState<string>);
            _methodDefinitions.Add(methodDefinition as IApiMethodDefinition);

            return methodDefinition;
        }

        public IApiObfuscatorBuilder ForRequest(Func<string, bool> condition)
        {
            var sharedStare = new ApiSharedState(_sharedState)
            {
                StrWhenCondition = condition,
            };

            IApiObfuscatorBuilder methodDefinition = new ApiObfuscatorBuilder(new ApiSharedState<string>(sharedStare) as IApiSharedState<string>);
            _methodDefinitions.Add(methodDefinition as IApiMethodDefinition);

            return methodDefinition;
        }
    }
}
