using Fiddler;
using System;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    public class FragmentSegmentParameter : KeyParameter<string>
    {
        public FragmentSegmentParameter(string fragment)
        {
            Key = fragment;
        }

        public override bool IsMet(Session session)
        {
            var uri = new Uri(session.fullUrl);

            return uri.Fragment == Key;
        }

        public override Session Transform(Session session)
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
