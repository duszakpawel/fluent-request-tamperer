using Fiddler;

namespace RequestObfuscator.Api
{
    public interface IApiMethodDefinition
    {
        void BeforeRequest(Session session);
    }
}
