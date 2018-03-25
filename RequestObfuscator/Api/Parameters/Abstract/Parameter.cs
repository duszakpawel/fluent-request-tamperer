using Fiddler;

namespace RequestObfuscator.Api.Parameters
{
    internal abstract class Parameter
    {
        internal abstract Session Transform(Session session);
        internal abstract bool IsMet(Session session);
    }
}
