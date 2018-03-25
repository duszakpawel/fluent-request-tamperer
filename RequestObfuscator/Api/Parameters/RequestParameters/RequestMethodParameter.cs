using RequestObfuscator.Enums;
using Fiddler;

namespace RequestObfuscator.Api.Parameters.RequestParameters
{
    internal class RequestMethodParameter : KeyParameter<HttpMethodEnum>
    {
        internal override bool IsMet(Session session)
        {
            return session.RequestMethod == Key.ToString();
        }

        internal override Session Transform(Session session)
        {
            session.RequestMethod = Key.ToString();

            return session;
        }
    }
}
