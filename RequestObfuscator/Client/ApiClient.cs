using RequestObfuscator.Api;

namespace RequestObfuscator.Client
{
    public abstract class ApiClient : IApiClient
    {
        public abstract IApiBuilder Configure(IApiBuilder api);
    }
}
