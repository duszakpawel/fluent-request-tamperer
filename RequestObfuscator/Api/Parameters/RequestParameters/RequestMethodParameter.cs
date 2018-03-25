using RequestObfuscator.Enums;
using Fiddler;

namespace RequestObfuscator.Api.Parameters.RequestParameters
{
    public class RequestMethodParameter : KeyParameter<HttpMethodEnum>
    {
        public override bool IsMet(Session session)
        {
            return session.RequestMethod == Key.ToString();
        }

        public override Session Transform(Session session)
        {
            session.RequestMethod = Key.ToString();

            return session;
        }
    }
}
