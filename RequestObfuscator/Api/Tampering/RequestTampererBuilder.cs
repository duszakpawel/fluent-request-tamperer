using RequestObfuscator.Api.Parameters;
using RequestObfuscator.Api.Parameters.Parameters;
using RequestObfuscator.Api.Parameters.RequestParameters;
using RequestObfuscator.Enums;
using System;
using Fiddler;

namespace RequestObfuscator.Api.Tampering
{
    public class RequestTampererBuilder<TRequest> : IRequestTampererBuilder<TRequest> where TRequest : class
    {

        private readonly IApiSharedState<TRequest> _sharedState;
        public RequestTampererBuilder(IApiSharedState<TRequest> sharedState)
        {
            _sharedState = sharedState;
        }

        public IRequestTamperer Build()
        {
            return new RequestTamperer<TRequest>(_sharedState);
        }

        public IRequestTampererBuilder<TRequest> TamperHeader(string key, string value)
        {
            _sharedState.TamperRules.Add(new HeaderParameter(key, value));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperHost(string host)
        {
            _sharedState.TamperRules.Add(new HostParameter(host));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperMethodType(HttpMethodEnum methodType)
        {
            _sharedState.TamperRules.Add(new MethodTypeParameter(methodType));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperPort(int port)
        {
            _sharedState.TamperRules.Add(new PortParameter(port));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperScheme(string scheme)
        {
            _sharedState.TamperRules.Add(new SchemeParameter(scheme));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperPath(Func<string, string> pathTransformer = null)
        {
            _sharedState.TamperRules.Add(new PathSegmentParameter(pathTransformer));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperQuery(Func<string, string> pathTransformer = null)
        {
            _sharedState.TamperRules.Add(new QuerySegmentParameter(pathTransformer));

            return this;
        }

        public IRequestTampererBuilder<TRequest> TamperFragment(string fragment)
        {
            _sharedState.TamperRules.Add(new FragmentSegmentParameter(fragment));

            return this;
        }
    }

    public class RequestTampererBuilder : IRequestTampererBuilder
    {

        private readonly IApiSharedState<string> _sharedState;
        public RequestTampererBuilder(IApiSharedState<string> sharedState)
        {
            _sharedState = sharedState;
        }

        public IRequestTamperer Build()
        {
            return new RequestTamperer(_sharedState);
        }

        public IRequestTampererBuilder TamperHeader(string key, string value)
        {
            _sharedState.TamperRules.Add(new HeaderParameter(key, value));

            return this;
        }

        public IRequestTampererBuilder TamperHost(string host)
        {
            _sharedState.TamperRules.Add(new HostParameter(host));

            return this;
        }

        public IRequestTampererBuilder TamperMethodType(HttpMethodEnum methodType)
        {
            _sharedState.TamperRules.Add(new MethodTypeParameter(methodType));

            return this;
        }

        public IRequestTampererBuilder TamperPort(int port)
        {
            _sharedState.TamperRules.Add(new PortParameter(port));

            return this;
        }

        public IRequestTampererBuilder TamperScheme(string scheme)
        {
            _sharedState.TamperRules.Add(new SchemeParameter(scheme));

            return this;
        }

        public IRequestTampererBuilder TamperPath(Func<string, string> pathTransformer = null)
        {
            _sharedState.TamperRules.Add(new PathSegmentParameter(pathTransformer));

            return this;
        }

        public IRequestTampererBuilder TamperQuery(Func<string, string> pathTransformer = null)
        {
            _sharedState.TamperRules.Add(new QuerySegmentParameter(pathTransformer));

            return this;
        }

        public IRequestTampererBuilder TamperFragment(string fragment)
        {
            _sharedState.TamperRules.Add(new FragmentSegmentParameter(fragment));

            return this;
        }
    }
}
