using System;
using Fiddler;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    internal class PathSegmentParameter : KeyValueParameter<string, string>
    {
        private Func<string, string> _pathTransformer;
        private Func<string, bool> _isMetPathTransformer;

        internal PathSegmentParameter(Func<string, bool> isMetPathTransformer = null)
        {
            _isMetPathTransformer = isMetPathTransformer;
        }

        internal PathSegmentParameter(Func<string, string> pathTransformer = null)
        {
            _pathTransformer = pathTransformer;
        }

        internal override bool IsMet(Session session)
        {
            var uri = new UriBuilder(session.fullUrl);

            return _isMetPathTransformer == null ? true : _isMetPathTransformer(uri.Path);
        }

        internal override Session Transform(Session session)
        {
            var uri = new UriBuilder(session.fullUrl);

            if (_pathTransformer != null)
            {
                uri.Path = _pathTransformer(uri.Path);
            }

            session.fullUrl = uri.Uri.AbsoluteUri;

            return session;
        }
    }
}
