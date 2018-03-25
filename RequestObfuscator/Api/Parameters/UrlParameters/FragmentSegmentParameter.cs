using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    internal class FragmentSegmentParameter : KeyParameter<string>
    {
        internal FragmentSegmentParameter(string fragment)
        {
            Key = fragment;
        }

        internal override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Fragment == Key;
        }

        internal override Session Transform(Session session)
        {
            var uri = new UriBuilder(session.fullUrl)
            {
                Fragment = Key
            };

            session.fullUrl = uri.Uri.AbsoluteUri;

            return session;
        }
    }
}
