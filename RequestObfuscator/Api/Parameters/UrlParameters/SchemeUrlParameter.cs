using System;
using Fiddler;

namespace RequestObfuscator.Api.Parameters
{
    public class SchemeParameter : KeyParameter<string>
    {
        public SchemeParameter(string scheme)
        {
            Key = scheme;
        }

        public override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Scheme == Key;
        }

        public override Session Transform(Session session)
        {
            var uri = new UriBuilder(session.fullUrl)
            {
                Scheme = Key
            };

            session.fullUrl = uri.Uri.AbsoluteUri;

            return session;
        }
    }
}
