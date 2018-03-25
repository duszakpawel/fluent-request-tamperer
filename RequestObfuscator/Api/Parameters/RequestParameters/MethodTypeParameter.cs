using Fiddler;
using RequestObfuscator.Enums;

namespace RequestObfuscator.Api.Parameters.RequestParameters
{
    internal class MethodTypeParameter : KeyParameter<HttpMethodEnum>
    {
        public MethodTypeParameter(HttpMethodEnum key)
        {
            Key = key;
        }

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
