using Fiddler;

namespace RequestObfuscator.Api
{
    internal interface IApiMethodDefinition
    {
        void BeforeRequest(Session session);
    }
}
