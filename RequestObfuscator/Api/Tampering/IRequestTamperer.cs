using Fiddler;

namespace RequestObfuscator.Api.Tampering
{
    public interface IRequestTamperer
    {
        void BeforeRequest(Session request);
    }
}
