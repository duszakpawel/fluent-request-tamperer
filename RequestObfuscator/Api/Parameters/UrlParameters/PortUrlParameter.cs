using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    internal class PortParameter : KeyParameter<int>
    {
        internal PortParameter(int port)
        {
            Key = port;
        }

        internal override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Port == Key;
        }
        internal override Session Transform(Session session)
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
