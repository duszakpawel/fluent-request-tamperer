using RequestObfuscator.Api;

namespace RequestObfuscator.Client
{
    public interface IApiClient
    {
        IApiBuilder Configure(IApiBuilder api);
    }
}
