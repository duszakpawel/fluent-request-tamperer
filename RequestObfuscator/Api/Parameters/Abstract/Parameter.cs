using Fiddler;

namespace RequestObfuscator.Api.Parameters
{
    public abstract class Parameter
    {
        public abstract Session Transform(Session session);
        public abstract bool IsMet(Session session);
    }
}
