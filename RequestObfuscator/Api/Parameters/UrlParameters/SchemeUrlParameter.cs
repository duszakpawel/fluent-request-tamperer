using System;
using Fiddler;

namespace RequestObfuscator.Api.Parameters
{
    internal class SchemeParameter : KeyParameter<string>
    {
        internal SchemeParameter(string scheme)
        {
            Key = scheme;
        }

        internal override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Scheme == Key;
        }

        internal override Session Transform(Session session)
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
