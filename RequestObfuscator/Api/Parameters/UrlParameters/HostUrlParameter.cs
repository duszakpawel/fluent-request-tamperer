using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    internal class HostParameter : KeyParameter<string>
    {
        internal HostParameter(string host)
        {
            Key = host;
        }

        internal override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Host == Key;
        }

        internal override Session Transform(Session session)
        {
            var uri = new UriBuilder(session.fullUrl)
            {
                Host = Key
            };

            session.fullUrl = uri.Uri.AbsoluteUri;

            return session;
        }
    }
}
