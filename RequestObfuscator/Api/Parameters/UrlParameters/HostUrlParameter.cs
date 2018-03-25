using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    public class HostParameter : KeyParameter<string>
    {
        public HostParameter(string host)
        {
            Key = host;
        }

        public override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Host == Key;
        }

        public override Session Transform(Session session)
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
