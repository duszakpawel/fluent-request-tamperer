using System;
using Fiddler;

namespace RequestObfuscator.Api.Parameters.Parameters
{
    public class PathSegmentParameter : KeyValueParameter<string, string>
    {
        private Func<string, string> _pathTransformer;
        private Func<string, bool> _isMetPathTransformer;

        public PathSegmentParameter(Func<string, bool> isMetPathTransformer = null)
        {
            _isMetPathTransformer = isMetPathTransformer;
        }

        public PathSegmentParameter(Func<string, string> pathTransformer = null)
        {
            _pathTransformer = pathTransformer;
        }

        public override bool IsMet(Session session)
        {
            var uri = new UriBuilder(session.fullUrl);

            return _isMetPathTransformer == null ? true : _isMetPathTransformer(uri.Path);
        }

        public override Session Transform(Session session)
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
