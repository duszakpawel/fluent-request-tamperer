using Fiddler;
using RequestObfuscator.Enums;

namespace RequestObfuscator.Api.Parameters.RequestParameters
{
    public class MethodTypeParameter : KeyParameter<HttpMethodEnum>
    {
        public MethodTypeParameter(HttpMethodEnum key)
        {
            Key = key;
        }

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
