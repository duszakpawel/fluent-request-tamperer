using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    public class PortParameter : KeyParameter<int>
    {
        public PortParameter(int port)
        {
            Key = port;
        }

        public override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Port == Key;
        }

        public override Session Transform(Session session)
        {
            var uri = new UriBuilder(session.fullUrl)
            {
                Port = Key
            };

            session.fullUrl = uri.Uri.AbsoluteUri;

            return session;
        }
    }
}
